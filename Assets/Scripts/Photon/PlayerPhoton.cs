using Photon.Pun;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class PlayerPhoton : MonoBehaviour
{
    public GameObject playerCamera;
    public Transform immune;
    public TMP_Text userTag;
    public float movementSpeed;
    
    private PhotonView _view;
    private bool _shouldWork, _isLookingLeft;
    
    private void Awake()
    {
        _view = GetComponent<PhotonView>();
        _shouldWork = _view.IsMine;
        
        if (_shouldWork)
        {
            playerCamera.SetActive(true);
        }
        
        userTag.text = _view.Owner.NickName;
    }

    private void Update()
    {
        if(!_shouldWork) return;
        
        Movement();
    }
    
    private void Movement()
    {
        var horiz = Input.GetAxis("Horizontal");
        var move = new Vector3(horiz, 0, 0);

        transform.position += move * (movementSpeed * Time.deltaTime);

        if (horiz < 0)
        {
            if (!_isLookingLeft)
            {
                _view.RPC("FlipPlayer", RpcTarget.AllBuffered);
            }
        }
        else if (horiz > 0)
        {
            if (_isLookingLeft)
            {
                _view.RPC("FlipPlayer", RpcTarget.AllBuffered);
            }
        }
    }

    /// <summary>
    /// Rotates the player while maintaining the camera in the same place
    /// his is a bad function i wrote just to get the tutorial over with and just understand RPCs
    /// </summary>
    [PunRPC] private void FlipPlayer()
    {
        _isLookingLeft = !_isLookingLeft;
        
        immune.parent = null;

        transform.Rotate(0, 180, 0);

        immune.parent = transform;
    }
}
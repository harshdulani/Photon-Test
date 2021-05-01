using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserCanvasController : MonoBehaviour
{
    [SerializeField] private Canvas roomCanvas;
    private Canvas _userCanvas;
    
    [SerializeField] private TMP_InputField usernameText;
    [SerializeField] private Button submitButton;

    private void Start()
    {
        _userCanvas = GetComponent<Canvas>();
        
        roomCanvas.enabled = false;
    }

    public void UsernameUpdate(string text)
    {
        submitButton.interactable = text.Length >= 3;
    }

    public void OnSubmit()
    {
        PhotonNetwork.NickName = usernameText.text;
        
        _userCanvas.enabled = false;
        roomCanvas.enabled = true;
    }
}
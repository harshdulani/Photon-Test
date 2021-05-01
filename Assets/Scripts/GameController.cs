using System.IO;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviourPunCallbacks
{
    public GameObject mainCamera, gameCanvas, playerPrefab, disconnectUI, roomFeedGrid, roomFeedItem;
    public Text ping;

    private bool _isDisconnectUIVisible;

    private void Start()
    {
        gameCanvas.SetActive(true);
    }

    private void Update()
    {
        ping.text = "PING: " + PhotonNetwork.GetPing();

        if (Input.GetButtonDown("Cancel"))
        {
            if (_isDisconnectUIVisible)
            {
                disconnectUI.SetActive(false);
                _isDisconnectUIVisible = false;
            }
            else
            {
                disconnectUI.SetActive(true);
                _isDisconnectUIVisible = true;
            }
        }
    }

    public void StartButton()
    {
        var spawnLoc = new Vector3(Random.Range(-2f, 2f), 1,0);

        PhotonNetwork.Instantiate("Prefabs" + Path.DirectorySeparatorChar + playerPrefab.name, spawnLoc, Quaternion.identity);
        
        mainCamera.SetActive(false);
        gameCanvas.SetActive(false);
        
        Debug.Log(PhotonNetwork.CloudRegion);
    }

    public void OnLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("DS_MenuScene");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogError("entered " + PhotonNetwork.CloudRegion);
        var instance = Instantiate(roomFeedItem, Vector3.zero, Quaternion.identity);
        instance.transform.SetParent(roomFeedGrid.transform, false);
        var text = instance.GetComponent<Text>();
        text.text = newPlayer.NickName + " joined the game";
        text.color = Color.green;
        Destroy(instance, 4f);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogError("exit" + PhotonNetwork.CloudRegion);
        var instance = Instantiate(roomFeedItem, Vector3.zero, Quaternion.identity);
        instance.transform.SetParent(roomFeedGrid.transform, false);
        var text = instance.GetComponent<Text>();
        text.text = otherPlayer.NickName + " left the game";
        text.color = Color.red;
        Destroy(instance, 4f);
    }
}
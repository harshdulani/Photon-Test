using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomController : MonoBehaviourPunCallbacks
{
    public static RoomController me;

    [SerializeField] private string version = "0.1"; 

    private void Awake()
    {
        if (me == null)
            me = this;
        else
            Destroy(gameObject);

        PhotonNetwork.GameVersion = version;
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Cancel"))
            Application.Quit();
    }

    public void JoinRoom(string id)
    {
        //may want to do clean up here
        // if room is full/nonexistent
        PhotonNetwork.JoinRoom(id);
    }

    public void CreateRoom(string id)
    {
        PhotonNetwork.CreateRoom(id, new RoomOptions() {MaxPlayers = 5}, TypedLobby.Default);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        print("Connected to PUN");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomCanvasController : MonoBehaviour
{
    [SerializeField] private TMP_InputField joinID, createID;
    [SerializeField] private Button joinBtn, createBtn;

    public void OnJoinEdit(string text)
    {
        joinBtn.interactable = text.Length >= 3;
    }
    
    public void OnCreateEdit(string text)
    {
        createBtn.interactable = text.Length >= 3;
    }

    public void ClickJoin()
    {
        RoomController.me.JoinRoom(joinID.text);
    }

    public void ClickCreate()
    {
        RoomController.me.CreateRoom(createID.text);
    }
}

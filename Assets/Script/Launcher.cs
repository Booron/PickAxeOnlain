using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public TMP_InputField roomNameInputField;
    [SerializeField]
    public TMP_Text errorText;
    [SerializeField]
    public TMP_Text roomNameText;
    void Start()
    {
        Debug.Log("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        MenuMeneger.Instance.OpenMenu("title");
        Debug.Log("Joinde Lobby");
    }
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
            PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuMeneger.Instance.OpenMenu("loading");
    }
    public override void OnJoinedRoom()
    {
        MenuMeneger.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Faild: " + message;
        MenuMeneger.Instance.OpenMenu("error");
    }
     public void leaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuMeneger.Instance.OpenMenu("title");
    }
}

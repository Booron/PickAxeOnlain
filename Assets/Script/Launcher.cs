using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;

public class Launcher : MonoBehaviourPunCallbacks
{

    public static Launcher Instance;

    [SerializeField]
     TMP_InputField roomNameInputField;
    [SerializeField]
     TMP_Text errorText;
    [SerializeField]
     TMP_Text roomNameText;
    [SerializeField]
    Transform roomListContent;
    [SerializeField]
    GameObject roomListPrefab;
    [SerializeField]
    Transform playerListContent;
    [SerializeField]
    GameObject playerListPrefab;
    [SerializeField]
    GameObject StartGameButton;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Debug.Log("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnJoinedLobby()
    {
        MenuMeneger.Instance.OpenMenu("title");
        Debug.Log("Joinde Lobby");
        PhotonNetwork.NickName="Player"+ Random.Range(1,100).ToString("0000");
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
    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnJoinedRoom()
    {
        MenuMeneger.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        Player[] players = PhotonNetwork.PlayerList;
        
        for (int i = 0; i < players.Count();  i++)
        {
            Instantiate(playerListPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        StartGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        StartGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Faild: " + message;
        MenuMeneger.Instance.OpenMenu("error");
    }
     public void leaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuMeneger.Instance.OpenMenu("loading");
    }
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuMeneger.Instance.OpenMenu("loading");
    }
    public override void OnLeftRoom()
    {
        MenuMeneger.Instance.OpenMenu("title");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans  in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            Instantiate(roomListPrefab, roomListContent).GetComponent<RroomListItem>().SetUp(roomList[i]);
        }

    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

}

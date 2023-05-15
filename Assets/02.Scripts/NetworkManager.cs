using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public Text StatusText;
    public InputField roomInput, NickNameInput;


    void Awake() => Screen.SetResolution(960, 540, false);

    void Update() => StatusText.text = PhotonNetwork.NetworkClientState.ToString();



    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        print("서버접속완료");
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
    }


    //연결끊기
    public void Disconnect() => PhotonNetwork.Disconnect();

    //연결끊기완료시
    public override void OnDisconnected(DisconnectCause cause) => print("연결끊김");


    //로비로 접속
    public void JoinLobby() => PhotonNetwork.JoinLobby();

    //로비접속완료시
    public override void OnJoinedLobby() => print("로비접속완료");



    //방만들기
    public void CreateRoom() => PhotonNetwork.CreateRoom(roomInput.text, new RoomOptions { MaxPlayers = 2 });

    //방들어가기
    public void JoinRoom() => PhotonNetwork.JoinRoom(roomInput.text);
    
    //방들어가기시 방이없으면 만들기
    public void JoinOrCreateRoom() => PhotonNetwork.JoinOrCreateRoom(roomInput.text, new RoomOptions { MaxPlayers = 2 }, null);

    //랜덤방들어가기
    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();

    //방떠나기
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    //방만들기완료시
    public override void OnCreatedRoom() => print("방만들기완료");

    //방들어가기 완료시
    public override void OnJoinedRoom() => print("방참가완료");

    //방만들기 실패시
    public override void OnCreateRoomFailed(short returnCode, string message) => print("방만들기실패");

    //방들어가기 실패시
    public override void OnJoinRoomFailed(short returnCode, string message) => print("방참가실패");

    //방랜덤참가 실패시
    public override void OnJoinRandomFailed(short returnCode, string message) => print("방랜덤참가실패");



    [ContextMenu("정보")]
    void Info()
    {
        if (PhotonNetwork.InRoom)
        {
            print("현재 방 이름 : " + PhotonNetwork.CurrentRoom.Name);
            print("현재 방 인원수 : " + PhotonNetwork.CurrentRoom.PlayerCount);
            print("현재 방 최대인원수 : " + PhotonNetwork.CurrentRoom.MaxPlayers);

            string playerStr = "방에 있는 플레이어 목록 : ";
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++) playerStr += PhotonNetwork.PlayerList[i].NickName + ", ";
            print(playerStr);
        }
        else
        {
            print("접속한 인원 수 : " + PhotonNetwork.CountOfPlayers);
            print("방 개수 : " + PhotonNetwork.CountOfRooms);
            print("모든 방에 있는 인원 수 : " + PhotonNetwork.CountOfPlayersInRooms);
            print("로비에 있는지? : " + PhotonNetwork.InLobby);
            print("연결됐는지? : " + PhotonNetwork.IsConnected);
        }
    }
}
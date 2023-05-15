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
        print("�������ӿϷ�");
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
    }


    //�������
    public void Disconnect() => PhotonNetwork.Disconnect();

    //�������Ϸ��
    public override void OnDisconnected(DisconnectCause cause) => print("�������");


    //�κ�� ����
    public void JoinLobby() => PhotonNetwork.JoinLobby();

    //�κ����ӿϷ��
    public override void OnJoinedLobby() => print("�κ����ӿϷ�");



    //�游���
    public void CreateRoom() => PhotonNetwork.CreateRoom(roomInput.text, new RoomOptions { MaxPlayers = 2 });

    //�����
    public void JoinRoom() => PhotonNetwork.JoinRoom(roomInput.text);
    
    //������ ���̾����� �����
    public void JoinOrCreateRoom() => PhotonNetwork.JoinOrCreateRoom(roomInput.text, new RoomOptions { MaxPlayers = 2 }, null);

    //���������
    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();

    //�涰����
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    //�游���Ϸ��
    public override void OnCreatedRoom() => print("�游���Ϸ�");

    //����� �Ϸ��
    public override void OnJoinedRoom() => print("�������Ϸ�");

    //�游��� ���н�
    public override void OnCreateRoomFailed(short returnCode, string message) => print("�游������");

    //����� ���н�
    public override void OnJoinRoomFailed(short returnCode, string message) => print("����������");

    //�淣������ ���н�
    public override void OnJoinRandomFailed(short returnCode, string message) => print("�淣����������");



    [ContextMenu("����")]
    void Info()
    {
        if (PhotonNetwork.InRoom)
        {
            print("���� �� �̸� : " + PhotonNetwork.CurrentRoom.Name);
            print("���� �� �ο��� : " + PhotonNetwork.CurrentRoom.PlayerCount);
            print("���� �� �ִ��ο��� : " + PhotonNetwork.CurrentRoom.MaxPlayers);

            string playerStr = "�濡 �ִ� �÷��̾� ��� : ";
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++) playerStr += PhotonNetwork.PlayerList[i].NickName + ", ";
            print(playerStr);
        }
        else
        {
            print("������ �ο� �� : " + PhotonNetwork.CountOfPlayers);
            print("�� ���� : " + PhotonNetwork.CountOfRooms);
            print("��� �濡 �ִ� �ο� �� : " + PhotonNetwork.CountOfPlayersInRooms);
            print("�κ� �ִ���? : " + PhotonNetwork.InLobby);
            print("����ƴ���? : " + PhotonNetwork.IsConnected);
        }
    }
}
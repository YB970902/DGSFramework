using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using DGS.NetworkModule;
using System;

public class PUNLobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] string _name;
    [SerializeField] int _level;

    private void Start()
    {
        PhotonNetwork.GameVersion = "0.0.0";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");
        PhotonNetwork.CreateRoom("TestRoom", new RoomOptions() { MaxPlayers = 10 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        // 방에 들어온 뒤 부터 활성화
        var info = new ServerData.PlayerInfo();
        var span = DateTime.Now.ToLocalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
        long timestamp = (long)span.TotalSeconds;
        info.Set(_name, _level, timestamp);

        DGSServerManager.Instance.Request(DGS.Define.DefineServerData.REQ_REGISTER_INFO, info, ReceiverGroup.MasterClient);
        DGSServerManager.Instance.AddListener(DGS.Define.DefineServerData.RES_REGISTER_INFO, OnResponseRegister);
        DGSServerManager.Instance.AddListener(DGS.Define.DefineServerData.RES_PLAYER_INFO, OnResponsePlayerInfo);
    }

    void OnResponseRegister(ServerData.IServerData serverData)
    {
        var info = serverData as ServerData.RegisterInfo;
        Debug.Log($"Register Message : {info.Message}");
    }

    void OnResponsePlayerInfo(ServerData.IServerData serverData)
    {
        var local = new LocalData.PlayerInfo();
        local.Set(serverData);
        UIManager.Instance.CreateUserInfo(local);
    }
}

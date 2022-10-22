using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using DGS.Define;
using DGS.NetworkModule;
using Photon.Realtime;

public class ServerManager : MonoBehaviourPunCallbacks
{
    [SerializeField] int _id;
    [SerializeField] int _level;
    [SerializeField] string _name;

    private void Start()
    {
        PhotonNetwork.GameVersion = "0.0.0";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        // 연결 여기서 부터 시작 !

        var userInfo = new ServerData.UserInfo();
        userInfo.Set(_id, _level, _name);
        DGSServerManager.Instance.Send(DefineServerData.REQ_REGISTER_MY_INFO, userInfo, ReceiverGroup.MasterClient);
        DGSServerManager.Instance.AddListener(DefineServerData.RES_REGISTER_MY_INFO, OnResponseRegisterMyInfo);
    }

    void OnResponseRegisterMyInfo(ServerData.IServerData data)
    {
        var result = data as ServerData.RegisterResult;
        Debug.Log(result.Message);
    }
}

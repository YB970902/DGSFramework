using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using DGS.NetworkModule;
using DGS.Define;

public class DGSServerManager : MonoBehaviourPun, IOnEventCallback
{
    #region SINGLETON
    private static DGSServerManager _instance = null;

    public static DGSServerManager Instance
    {
        get
        {
            if(null == _instance)
            {
                _instance = FindObjectOfType<DGSServerManager>();
            }

            return _instance;
        }
    }
    #endregion

    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    private Dictionary<byte, List<System.Action<ServerData.IServerData>>> _dictCallback = new Dictionary<byte, List<System.Action<ServerData.IServerData>>>();

    public void Request(byte key, ServerData.IServerData serverData, ReceiverGroup group, EventCaching caching = EventCaching.DoNotCache)
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = group, CachingOption = caching };
        SendOptions sendOptions = new SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(key, serverData.ToCustomData(), raiseEventOptions, sendOptions);
    }

    public void AddListener(byte key, System.Action<ServerData.IServerData> callback)
    {
        if(null == callback)
        {
            return;
        }

        if (false == _dictCallback.ContainsKey(key))
        {
            _dictCallback[key] = new List<System.Action<ServerData.IServerData>>();
        }

        _dictCallback[key].Add(callback);
    }

    public void RemoveListener(byte key, System.Action<ServerData.IServerData> callback)
    {
        if (false == _dictCallback.ContainsKey(key))
        {
            return;
        }

        _dictCallback[key].Remove(callback);
    }

    public void RemoveAllListener(byte key)
    {
        if (false == _dictCallback.ContainsKey(key))
        {
            return;
        }

        _dictCallback[key].Clear();
    }

    public void OnEvent(EventData photonEvent)
    {
        byte key = photonEvent.Code;

        if(PhotonNetwork.IsMasterClient)
        {
            OnGetRequest(photonEvent);
        }

        if(key < DefineServerData.GAP_OF_KEY || key >= DefineServerData.GAP_OF_KEY * 2)
        {
            // REQ는 모조리 무시한다
            return;
        }

        ServerData.IServerData data = null;

        // 여기에 조건문 추가
        
        data.SetData((object[])photonEvent.CustomData);

        if(_dictCallback.ContainsKey(key) == false)
        {
            return;
        }

        foreach (var callback in _dictCallback[key])
        {
            callback.Invoke(data);
        }
    }

    void OnGetRequest(EventData photonEvent)
    {
        byte key = photonEvent.Code;

        // RES 메시지는 모조리 무시
        if(key >= DefineServerData.GAP_OF_KEY || key < 0)
        {
            return;
        }

        int sender = photonEvent.Sender;

        // 여기에 조건문 추가
    }

    void SendEventToSender(byte key, int sender, ServerData.IServerData data, EventCaching caching = EventCaching.DoNotCache)
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { TargetActors = new int[] { sender }, CachingOption = caching };
        SendOptions sendOptions = new SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(key, data.ToCustomData(), raiseEventOptions, sendOptions);
    }

    void SendEventToGroup(byte key, ReceiverGroup group, ServerData.IServerData data, EventCaching caching = EventCaching.DoNotCache)
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = group, CachingOption = caching };
        SendOptions sendOptions = new SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(key, data.ToCustomData(), raiseEventOptions, sendOptions);
    }
}

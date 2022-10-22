using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DGS.NetworkModule
{
    public class ServerData
    {
        public interface IServerData
        {
            public object[] ToCustomData();
            public void SetData(object[] customData);
        }
    }

    public class LocalData
    {
        public interface ILocalData
        {
            public void Set(ServerData.IServerData data);
        }
    }
}
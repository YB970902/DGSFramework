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

        public class UserInfo : IServerData
        {
            private int _id;
            public int ID { get => _id; }
            
            private int _level;
            public int Level { get => _level; }

            private string _name;
            public string Name { get => _name; }

            public void Set(int id, int level, string name)
            {
                _id = id;
                _level = level;
                _name = name;
            }

            public object[] ToCustomData()
            {
                return new object[] { _id, _level, _name };
            }

            public void SetData(object[] customData)
            {
                _id = (int)customData[0];
                _level = (int)customData[1];
                _name = (string)customData[2];
            }
        }

        public class RegisterResult : IServerData
        {
            private string _msg;
            public string Message { get => _msg; }

            public void Set(string msg)
            {
                _msg = msg;
            }

            public void SetData(object[] customData)
            {
                _msg = (string)customData[0];
            }

            public object[] ToCustomData()
            {
                return new object[] { _msg };
            }
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
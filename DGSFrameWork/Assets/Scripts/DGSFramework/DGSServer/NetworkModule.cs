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

        public class PlayerInfo : IServerData
        {
            private string _name = string.Empty;
            public string Name { get => _name; }

            private int _level = default(int);
            public int Level { get => _level; }

            private long _timeStamp = default(long);
            public long TimeStamp { get => _timeStamp; }

            public void Set(string name, int level, long timeStamp)
            {
                _name = name;
                _level = level;
                _timeStamp = timeStamp;
            }

            public void SetData(object[] customData)
            {
                _name = (string)customData[0];
                _level = (int)customData[1];
                _timeStamp = (long)customData[2];
            }

            public object[] ToCustomData()
            {
                return new object[] { _name, _level, _timeStamp };
            }
        }

        public class RegisterInfo : IServerData
        {
            string _message;
            public string Message { get => _message; }

            public void Set(string message)
            {
                _message = message;
            }

            public void SetData(object[] customData)
            {
                _message = (string)customData[0];
            }

            public object[] ToCustomData()
            {
                return new object[] { _message };
            }
        }
    }

    public class LocalData
    {
        public interface ILocalData
        {
            public void Set(ServerData.IServerData data);
        }

        public class PlayerInfo : ILocalData
        {
            private string _name;
            public string Name { get => _name; }

            private int _level;
            public int Level { get => _level; }

            public void Set(ServerData.IServerData data)
            {
                var info = (ServerData.PlayerInfo)data;
                _name = info.Name;
                _level = info.Level;
            }
        }
    }
}
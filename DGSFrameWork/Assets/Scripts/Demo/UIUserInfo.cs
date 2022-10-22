using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUserInfo : MonoBehaviour
{
    [SerializeField] Text _name;
    [SerializeField] Text _level;

    private void Awake()
    {
        _name.text = string.Empty;
        _level.text = string.Empty;
    }

    public void Set(DGS.NetworkModule.LocalData.PlayerInfo info)
    {
        _name.text = $"Name : {info.Name}";
        _level.text = $"Level : {info.Level}";
    }
}

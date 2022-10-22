using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region SINGLETON
    private static UIManager _instance = null;

    public static UIManager Instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = FindObjectOfType<UIManager>();
            }

            return _instance;
        }
    }
    #endregion

    [SerializeField] RectTransform _rcLayout;

    [SerializeField] UIUserInfo _userInfoPrefab;

    public void CreateUserInfo(DGS.NetworkModule.LocalData.PlayerInfo info)
    {
        var userInfo = Instantiate(_userInfoPrefab, _rcLayout);
        userInfo.Set(info);
    }
}

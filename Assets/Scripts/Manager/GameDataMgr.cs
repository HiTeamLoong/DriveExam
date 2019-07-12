using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr : XSingleton<GameDataMgr>
{

    public bool NeedLogin
    {
        get
        {
            return (ResponseLogin != null && responseCarType != null);
        }
    }

    /// <summary>
    /// 缓存登录信息
    /// </summary>
    private ResponseLogin responseLogin;
    public ResponseLogin ResponseLogin
    {
        get
        {
            if (responseLogin == null)
            {
                string json = PlayerPrefs.GetString("ResponseLogin");
                if (json != null)
                {
                    responseLogin = LitJson.JsonMapper.ToObject<ResponseLogin>(json);
                }
            }
            return responseLogin;
        }
        set
        {
            if (value != null)
            {
                responseLogin = value;
                string json = LitJson.JsonMapper.ToJson(responseLogin);
                PlayerPrefs.SetString("ResponseLogin",json);
            }
            else
            {
                PlayerPrefs.DeleteKey("ResponseLogin");
                responseLogin = value;
            }
        }
    }
    /// <summary>
    /// 缓存车型信息
    /// </summary>
    private ResponseCarType responseCarType;
    public ResponseCarType ResponseCarType
    {
        get
        {
            if(responseCarType == null)
            {
                string json = PlayerPrefs.GetString("ResponseCarType");
                if (json != null)
                {
                    responseCarType = LitJson.JsonMapper.ToObject<ResponseCarType>(json);
                }
            }
            return responseCarType;
        }
        set
        {
            if (value != null)
            {
                responseCarType = value;
                string json = LitJson.JsonMapper.ToJson(responseCarType);
                PlayerPrefs.SetString("ResponseCarType", json);
            }
            else
            {
                PlayerPrefs.DeleteKey("ResponseCarType");
                responseCarType = value;
            }
        }
    }

    private string accessToken;
    public string AcceccToken
    {
        get
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                accessToken = PlayerPrefs.GetString("AccessToken", null);
            }
            return accessToken;
        }
        set {
            PlayerPrefs.SetString("AccessToken", value);
            accessToken = value; 
        }
    }

    public CarType carType;
    public CarVersion carVersion;

    public CarUID carUID;
    public CarTypeData carTypeData = null;
    public ResponseCarInfo carInfo = null;
    public TypeModel typeModel = null;
}

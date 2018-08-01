using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr : XSingleton<GameDataMgr>
{
    private ResponseLogin responseLogin;
    public ResponseLogin ResponseLogin
    {
        get
        {
            //PlayerPrefs.DeleteAll();
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
    
    public CarType carType = CarType.DAZHONG;
}

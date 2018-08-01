using System;
using System.Collections;
using System.Collections.Generic;
using cn.sharesdk.unity3d;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GlobalManager : XMonoSingleton<GlobalManager>
{
    public ShareSDK shareSDK;

    private void Awake()
    {
        UIManager.Instance.CloseAllWindow();

        //Input.multiTouchEnabled = false;
    }

    private void Start()
    {
        if (shareSDK == null)
        {
            shareSDK = FindObjectOfType<ShareSDK>();
        }
        shareSDK.authHandler = OnAuthResultHandler;
        shareSDK.shareHandler = OnShareResultHandler;
        shareSDK.showUserHandler = OnGetUserInfoResultHandler;
        shareSDK.getFriendsHandler = OnGetFriendsResultHandler;
        shareSDK.followFriendHandler = OnFollowFriendResultHandler;

    }

    /// <summary>
    /// 授权回调
    /// </summary>
    /// <param name="reqID">Req identifier.</param>
    /// <param name="state">State.</param>
    /// <param name="type">Type.</param>
    /// <param name="result">Result.</param>
    void OnAuthResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            if (type == PlatformType.WeChat)
            {
                RequestOther requestOther = new RequestOther();
                requestOther.uid = result["openid"].ToString();
                requestOther.loginType = 1;
                requestOther.headImg = result["icon"].ToString();
                requestOther.userName = result["nickname"].ToString();
                requestOther.equitment = SystemInfo.deviceUniqueIdentifier;
                if (authWechat != null)
                {
                    authWechat(requestOther);
                    authWechat = null;
                }
            }
            else if (type == PlatformType.QQ)
            {
                RequestOther requestOther = new RequestOther();
                requestOther.uid = result["openid"].ToString();
                requestOther.loginType = 1;
                requestOther.headImg = result["headimgurl"].ToString();
                requestOther.userName = result["nickname"].ToString();
                requestOther.equitment = SystemInfo.deviceUniqueIdentifier;
                if (authWechat != null)
                {
                    authWechat(requestOther);
                    authWechat = null;
                }
            }
            //if (result != null && result.Count > 0)
            //{
            //    print("authorize success !" + "Platform :" + type + "result:" + MiniJSON.jsonEncode(result));
            //}
            //else
            //{
            //    print("authorize success !" + "Platform :" + type);
            //}
        }
        else if (state == ResponseState.Fail)
        {
#if UNITY_ANDROID
            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
#elif UNITY_IPHONE
            print("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
#endif
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
        }
    }
    /// <summary>
    /// 分享回调
    /// </summary>
    /// <param name="reqID">Req identifier.</param>
    /// <param name="state">State.</param>
    /// <param name="type">Type.</param>
    /// <param name="result">Result.</param>
    void OnShareResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            print("share successfully - share result :");
            print(MiniJSON.jsonEncode(result));
        }
        else if (state == ResponseState.Fail)
        {
#if UNITY_ANDROID
            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
#elif UNITY_IPHONE
            print("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
#endif
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
        }
    }
    /// <summary>
    /// 获取信息回调
    /// </summary>
    /// <param name="reqID">Req identifier.</param>
    /// <param name="state">State.</param>
    /// <param name="type">Type.</param>
    /// <param name="result">Result.</param>
    void OnGetUserInfoResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            print("get user info result :");
            print(MiniJSON.jsonEncode(result));
            print("AuthInfo:" + MiniJSON.jsonEncode(shareSDK.GetAuthInfo(PlatformType.QQ)));
            print("Get userInfo success !Platform :" + type);
        }
        else if (state == ResponseState.Fail)
        {
#if UNITY_ANDROID
            print ("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
#elif UNITY_IPHONE
            print("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
#endif
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
        }
    }
    /// <summary>
    /// 获取好友回调
    /// </summary>
    /// <param name="reqID">Req identifier.</param>
    /// <param name="state">State.</param>
    /// <param name="type">Type.</param>
    /// <param name="result">Result.</param>
    void OnGetFriendsResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            print("get friend list result :");
            print(MiniJSON.jsonEncode(result));
        }
        else if (state == ResponseState.Fail)
        {
#if UNITY_ANDROID
            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
#elif UNITY_IPHONE
            print("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
#endif
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="reqID"></param>
    /// <param name="state"></param>
    /// <param name="type"></param>
    /// <param name="result"></param>
    void OnFollowFriendResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            print("Follow friend successfully !");
        }
        else if (state == ResponseState.Fail)
        {
#if UNITY_ANDROID
            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
#elif UNITY_IPHONE
            print("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
#endif
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
        }
    }


    private Callback<RequestOther> authWechat;
    public void AuthWechat(Callback<RequestOther> callback)
    {
#if UNITY_EDITOR
        RequestOther requestOther = new RequestOther();
        requestOther.uid = "o0ray0iVuIB4CDxi_l_BgerD5Kw8";
        requestOther.loginType = 1;
        requestOther.headImg = "http://thirdwx.qlogo.cn/mmopen/vi_32/9RU5gXnwDosiaTn2VgyviajNpYalYoOEuUs3JJbtFjYbJWaT4Nq2j9Qw193FSwFVwePyuLWBGSSxfLf1E2HjSOdQ/132";
        requestOther.userName = "薛龙";
        requestOther.equitment = SystemInfo.deviceUniqueIdentifier;
        if (callback != null)
        {
            callback(requestOther);
        }
#else
        authWechat = callback;
        shareSDK.Authorize(PlatformType.WeChat);
#endif
    }

    //看需求写个MONO单例
    public void RequestNetworkFile(string url, Action<bool, string, byte[]> callback)
    {
        StartCoroutine(IRequestNetworkFile(url, callback));
    }
    /// <summary>
    /// Requests the network file.
    /// </summary>
    /// <returns>The network file.</returns>
    /// <param name="url">URL.</param>
    /// <param name="callback">Callback.</param>
    IEnumerator IRequestNetworkFile(string url, Action<bool, string, byte[]> callback)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        bool isError = false;

        if (request.isNetworkError)
        {
            Debug.LogErrorFormat("isNetworkError [{0}] [{1}]", url, request.error);
            isError = true;
        }
        else if (request.isHttpError)
        {
            Debug.LogErrorFormat("isHttpError [{0}] [{1}]", url, request.responseCode);
            isError = true;
        }
        else
        {
            if (request.responseCode == 200 || request.responseCode == 0)
            {
                Debug.Log(request.downloadHandler.text);
                callback(true, request.downloadHandler.text, request.downloadHandler.data);
            }
            else
            {
                Debug.LogErrorFormat("response code error [{0}]", request.responseCode);
                isError = true;
            }
        }

        if (isError)
        {
            callback(false, null, null);
        }
    }
}

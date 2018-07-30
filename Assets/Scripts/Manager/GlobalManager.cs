using System.Collections;
using System.Collections.Generic;
using cn.sharesdk.unity3d;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManager : XMonoSingleton<GlobalManager>
{
    public ShareSDK shareSDK;

    private void Awake()
    {
        UIManager.Instance.CloseAllWindow();

        Input.multiTouchEnabled = false;
    }

    private void Start()
    {
        if (shareSDK==null)
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
            if (result != null && result.Count > 0)
            {
                print("authorize success !" + "Platform :" + type + "result:" + MiniJSON.jsonEncode(result));
            }
            else
            {
                print("authorize success !" + "Platform :" + type);
            }
        }
        else if (state == ResponseState.Fail)
        {
#if UNITY_ANDROID
            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
#elif UNITY_IPHONE
            print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
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
            print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
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
    void OnGetUserInfoResultHandler (int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            print ("get user info result :");
            print (MiniJSON.jsonEncode(result));
            print ("AuthInfo:" + MiniJSON.jsonEncode (shareSDK.GetAuthInfo (PlatformType.QQ)));
            print ("Get userInfo success !Platform :" + type );
        }
        else if (state == ResponseState.Fail)
        {
            #if UNITY_ANDROID
            print ("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
            #elif UNITY_IPHONE
            print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
            #endif
        }
        else if (state == ResponseState.Cancel) 
        {
            print ("cancel !");
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
            print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
#endif
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
        }
    }

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
            print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
#endif
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
        }
    }

}

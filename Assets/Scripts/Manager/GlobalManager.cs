using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        //设置不开启多点触控
        Input.multiTouchEnabled = false;
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
            RequestOther requestOther = new RequestOther();
#if UNITY_ANDROID
                requestOther.uid = result["openid"].ToString();
                requestOther.loginType = 1;
                requestOther.headImg = result["icon"].ToString();
                requestOther.userName = result["nickname"].ToString();
                requestOther.equitment = SystemInfo.deviceUniqueIdentifier;
#elif UNITY_IOS
            requestOther.uid = result["openid"].ToString();
            requestOther.loginType = 1;
            requestOther.headImg = result["headimgurl"].ToString();
            requestOther.userName = result["nickname"].ToString();
            requestOther.equitment = SystemInfo.deviceUniqueIdentifier;
#endif


            if (authWechat != null)
            {
                authWechat(requestOther);
                authWechat = null;
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
            UITipsDialog.ShowTips("分享成功");
            print("share successfully - share result :");
            print(MiniJSON.jsonEncode(result));
        }
        else if (state == ResponseState.Fail)
        {
            UITipsDialog.ShowTips("分享失败");
#if UNITY_ANDROID
            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
#elif UNITY_IPHONE
            print("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
#endif
        }
        else if (state == ResponseState.Cancel)
        {
            UITipsDialog.ShowTips("取消分享");
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
    public string loongAuthUrl = "http://loongx.gz01.bdysite.com/authorize.json";
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

    public static string SHARE_DIR
    {
        get
        {
            return Path.Combine(Application.persistentDataPath, "ShareImage");
        }
    }
    private string WriteShareImage(Texture2D texture)
    {
        if (Directory.Exists(SHARE_DIR))
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(SHARE_DIR);
            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                File.Delete(fileInfo.FullName);
            }
        }

        byte[] imgData = texture.EncodeToPNG();
        string imgName = System.Guid.NewGuid().ToString("N") + ".png";
        try
        {
            string imagePath = Path.Combine(SHARE_DIR, imgName);
            using (FileStream fs = new FileStream(Path.Combine(SHARE_DIR, imgName), FileMode.Create, FileAccess.ReadWrite))
            {
                fs.Write(imgData, 0, imgData.Length);
            }
            return imagePath;
        }
        catch (IOException)
        {
            Debug.LogFormat("[{0}]:分享失败，请检查磁盘空间是否已满", "ShareSystem");
            return null;
        }
    }
    public void ShareImage(Texture2D texture)
    {
        string imagePath = WriteShareImage(texture);
        if (!string.IsNullOrEmpty(imagePath))
        {
            ShareContent content = new ShareContent();
            content.SetImagePath(imagePath);
            content.SetShareType(ContentType.Image);
            shareSDK.ShowPlatformList(null, content, 100, 100);
        }
    }
    public void ShareWebpage(string title, string text, string url, string imageUrl)
    {

        //    ShareContent content = new ShareContent();
        //content.SetText("this is a test string.");
        //content.SetImageUrl("http://ww3.sinaimg.cn/mw690/be159dedgw1evgxdt9h3fj218g0xctod.jpg");
        //content.SetTitle("test title");
        ////          content.SetTitleUrl("http://www.mob.com");
        ////          content.SetSite("Mob-ShareSDK");
        //// content.SetSiteUrl("http://www.mob.com");
        //content.SetUrl("http://qjsj.youzu.com/jycs/");
        ////          content.SetComment("test description");
        ////          content.SetMusicUrl("http://mp3.mwap8.com/destdir/Music/2009/20090601/ZuiXuanMinZuFeng20090601119.mp3");
        //content.SetShareType(ContentType.Webpage);
        //ssdk.ShareContent(PlatformType.WeChat, content);

        //string imagePath = WriteShareImage(texture);
        //if (!string.IsNullOrEmpty(imagePath))
        //{
        ShareContent content = new ShareContent();
        content.SetTitle(title);
        content.SetText(text);
        content.SetUrl(url);
        content.SetImageUrl(imageUrl);
        content.SetTitleUrl(url);
        //content.SetImagePath(imagePath);
        content.SetShareType(ContentType.Webpage);
        shareSDK.ShowPlatformList(null, content, 100, 100);
        //}
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

    private bool IsTiming;  //是否开始计时
    private float CountDown; //倒计时

    void Update()
    {
        EixtDetection(); //调用 退出检测函数
    }

    /// <summary>
    /// 退出检测
    /// </summary>
    private void EixtDetection()
    {
        if (Input.GetKeyDown(KeyCode.Escape))            //如果按下退出键
        {
            if (CountDown <= 0)                          //当倒计时时间等于0的时候
            {
                CountDown = Time.time;                   //把游戏开始时间，赋值给 CountDown
                IsTiming = true;                        //开始计时
                UITipsDialog.ShowTips("再次点击退出应用");
            }
            else
            {
                Application.Quit();                      //退出游戏
            }
        }

        if (IsTiming) //如果 IsTiming 为 true 
        {
            if ((Time.time - CountDown) > 2.0)           //如果 两次点击时间间隔大于2秒
            {
                CountDown = 0;                           //倒计时时间归零
                IsTiming = false;                       //关闭倒计时
            }
        }
    }

}

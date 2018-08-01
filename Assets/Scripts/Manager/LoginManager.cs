using BestHTTP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class LoginManager : XSingleton<LoginManager>
{
    public override void OnInit()
    {
        base.OnInit();
    }

    public readonly string serverURL = "http://app.jiakaojingling.com/jkjl/api";

    /// <summary>
    /// 请求账号登录
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="request"></param>
    /// <param name="callback"></param>
    public void SendLoginMessage<T>(RequestData_Base request, Callback<ResponseData<T>> callback) where T : ResponseData_Base
    {
        string requestURL = serverURL + "/member/userLogin";
        Request(requestURL, request, callback);
    }
    /// <summary>
    /// 第三方登录
    /// </summary>
    /// <param name="request">Request.</param>
    /// <param name="callback">Callback.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public void SendOtherMessage<T>(RequestData_Base request, Callback<ResponseData<T>> callback) where T : ResponseData_Base
    {
        string requestURL = serverURL + "/member/otherLogin";
        Request(requestURL, request, callback);
    }
    /// <summary>
    /// 请求验证码
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="request"></param>
    /// <param name="callback"></param>
    public void SendAuthCode<T>(RequestData_Base request, Callback<ResponseData<T>> callback) where T : ResponseData_Base
    {
        string requestURL = serverURL + "/member/getIdentifyingCode";
        Request(requestURL, request, callback);
    }
    /// <summary>
    /// 请求忘记密码
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="request"></param>
    /// <param name="callback"></param>
    public void SendForgetPwd<T>(RequestData_Base request, Callback<ResponseData<T>> callback) where T : ResponseData_Base
    {
        string requestURL = serverURL + "/member/resetPwd";
        Request(requestURL, request, callback);
    }
    /// <summary>
    /// 请求免费注册
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="request"></param>
    /// <param name="callback"></param>
    public void SendFreeSingup<T>(RequestData_Base request, Callback<ResponseData<T>> callback) where T : ResponseData_Base
    {
        string requestURL = serverURL + "/member/register";
        Request(requestURL, request, callback);
    }

    /// <summary>
    /// 获取验证图片
    /// </summary>
    /// <param name="udid"></param>
    /// <param name="callback"></param>
    public void GetValidateCodeImage(string udid, Callback<bool, Texture2D> callback)
    {
        string fileUrl = serverURL + "/member/ValidateCodeUtil?equitmentTime=" + udid;
        Debug.Log(fileUrl);
        ResourcesMgr.Instance.LoadNetworkFile(fileUrl, callback);
    }
    
    public void Request<T>(string url, RequestData_Base request, Callback<ResponseData<T>> callback) where T : ResponseData_Base
    {
        UIWaitDialog uIWait = UIManager.Instance.OpenUI<UIWaitDialog>();
        HTTPRequest httpRequest = new HTTPRequest(new Uri(url), HTTPMethods.Post, (originalRequest, response) =>
             {
                 string errStr = string.Empty;
                 ResponseData<T> retObject = new ResponseData<T>();
                 if (response == null || response.StatusCode >= 400)
                 {
                     if (response == null)
                     {
                         retObject.status = "-1";
                         retObject.msg = "服务器链接错误状态码：" + "response==null";
                     }
                     else
                     {
                         retObject.status = response.StatusCode.ToString();
                         retObject.msg = "服务器链接错误状态码：" + response.StatusCode.ToString();
                     }
                 }
                 else
                 {
                     switch (originalRequest.State)
                     {
                         case HTTPRequestStates.Processing:
                             retObject = null;
                             break;
                         case HTTPRequestStates.Finished:
                             if (response.IsSuccess)
                             {
                                 try
                                 {
                                     retObject = LitJson.JsonMapper.ToObject<ResponseData<T>>(response.DataAsText);
                                 }
                                 catch (Exception e)
                                 {
                                     retObject.status = "-1";
                                     retObject.msg = e.Message;
                                 }
                             }
                             else
                             {
                                 errStr = string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                           response.StatusCode, response.Message, response.DataAsText);
                             }
                             break;
                         case HTTPRequestStates.Error:
                             retObject.status = ((int)HTTPRequestStates.Error).ToString();
                             retObject.msg = originalRequest.Exception != null ? (originalRequest.Exception.Message + "\n" + originalRequest.Exception.StackTrace) : "No Exception";
                             break;
                         case HTTPRequestStates.Aborted:
                             retObject.status = ((int)HTTPRequestStates.Aborted).ToString();
                             retObject.msg = "Request Aborted!";
                             break;
                         case HTTPRequestStates.ConnectionTimedOut:
                             retObject.status = ((int)HTTPRequestStates.ConnectionTimedOut).ToString();
                             retObject.msg = "Connection Timed Out!";
                             break;
                         case HTTPRequestStates.TimedOut:
                             retObject.status = ((int)HTTPRequestStates.TimedOut).ToString();
                             retObject.msg = "Processing the request Timed Out!";
                             break;
                     }
                 }
                 if (retObject != null)
                 {
                     UIManager.Instance.CloseUI(uIWait);
                     callback(retObject);
                 }
             });

        System.Reflection.FieldInfo[] fieldInfos = request.GetType().GetFields();
        string message = url;
        for (int i = 0; i < fieldInfos.Length; i++)
        {
            System.Reflection.FieldInfo fieldInfo = fieldInfos[i];
            httpRequest.AddField(fieldInfo.Name, fieldInfo.GetValue(request).ToString());
            message += (i == 0) ? "?" : "&";
            message += (fieldInfo.Name + "=" + fieldInfo.GetValue(request).ToString());
        }
        Debug.Log(message);
        httpRequest.Send();
    }
}

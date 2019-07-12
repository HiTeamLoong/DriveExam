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
    /// 灯光考试登录
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="request"></param>
    /// <param name="callback"></param>
    public void SendLightLoginMessage<T>(RequestData_Base request, Callback<ResponseData<T>> callback) where T : ResponseData_Base
    {
        //http://app.jiakaojingling.com/jkjl/api/dengguang/userLogin  //10000001/782109 
        string requestURL = serverURL + "/dengguang/userLogin";
        Request(requestURL, request, callback);
    }

    /// <summary>
    /// 获取拥有车型
    /// </summary>
    /// <param name="request">Request.</param>
    /// <param name="callback">Callback.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public void SendGetCarType<T>(RequestData_Base request, Callback<ResponseData<T>> callback) where T : ResponseData_Base
    {
        //string requestURL = "http://192.168.31.195:8029/jkjl/api/light/carttype";
        //RequestTest(requestURL, request, callback);
        string requestURL = "http://139.129.255.20/jkjl/api/light/carttype";
        Request(requestURL, request, callback);
    }

    /// <summary>
    /// 获取车型信息
    /// </summary>
    /// <param name="request">Request.</param>
    /// <param name="callback">Callback.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public void SendGetCarInfo<T>(RequestData_Base request, Callback<ResponseData<T>> callback) where T : ResponseData_Base
    {
        //string requestURL = "http://192.168.31.195:8029/jkjl/api/light/examinationQuestions";
        //RequestTest(requestURL, request, callback);
        string requestURL = "http://139.129.255.20/jkjl/api/light/examinationQuestions";
        Request(requestURL, request, callback);
    }


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
                 Debug.Log("请求返回:"+response.DataAsText.ToString());
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

    /// <summary>
    /// 测试请求返回数据
    /// </summary>
    /// <param name="url">URL.</param>
    /// <param name="request">Request.</param>
    /// <param name="callback">Callback.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public void RequestTest<T>(string url, RequestData_Base request, Callback<ResponseData<T>> callback) where T : ResponseData_Base
    {
        UIWaitDialog uIWait = UIManager.Instance.OpenUI<UIWaitDialog>();

        ResponseData<T> retObject = new ResponseData<T>();
        string response = "";
        if (typeof(T).IsAssignableFrom(typeof(ResponseCarInfo)))
        {
            //response = "{\n    \"msg\": \"请求成功\",\n    \"data\": {\n        \"exam\": {\n            \"0\": [\n                {\n                    \"timuid\": 1\n                },\n                {\n                    \"timuid\": 2\n                },\n                {\n                    \"timuid\": 11\n                },\n                {\n                    \"timuid\": 5\n                },\n                {\n                    \"timuid\": 0\n                }\n            ],\n            \"1\": [\n                {\n                    \"timuid\": 1\n                },\n                {\n                    \"timuid\": 3\n                },\n                {\n                    \"timuid\": 4\n                },\n                {\n                    \"timuid\": 15\n                },\n                {\n                    \"timuid\": 0\n                }\n            ]\n        },\n        \"listvideo\": [\n            {\n                \"imgurl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight002.jpg\",\n                \"chexingId\": 1,\n                \"videourl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight002.mp4\",\n                \"videoname\": \"null\"\n            },\n            {\n                \"imgurl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight001.jpg\",\n                \"chexingId\": 1,\n                \"videourl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight001.mp4\",\n                \"videoname\": \"2001709210000032101.mp4\"\n            }\n        ],\n        \"listnewold\": [\n            {\n                \"examaudio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/start_new.mp3\",\n                \"uid\": 2,\n                \"broadcastend\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/ding.mp3\",\n                \"addTime\": \"2019-06-15 12:03:19\",\n                \"examtip\": \"下面将进行模拟夜间灯光的考试，请在语音播报叮一声结束后5秒内完成操作\",\n                \"type\": \"1\"\n            },\n            {\n                \"examaudio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/start_old.mp3\",\n                \"uid\": 1,\n                \"broadcastend\": \"http://jiakaojingling.oss-cn-qingdao.aliyuncs.com/static/upload/driver/90041560756849403.png\",\n                \"addTime\": \"2019-06-15 12:03:03\",\n                \"examtip\": \"下面将进行模拟夜间行驶场景灯光使用的考试，请按语音指令在5秒内做出相应的灯光操作\",\n                \"type\": \"2\"\n            }\n        ],\n        \"questions\": [\n            {\n                \"uid\": 1,\n                \"question\": \"请开启前照灯\",\n                \"answer\": \"答案：开启大灯，注意是近光\",\n                \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/1.mp3\",\n                \"DoubleJumpLamp\": 0,\n                \"ClearAnceLamp\": 1,\n                \"LowBeamLight\": 1,\n                \"HigBeamLight\": 0,\n                \"FrontFogLamp\": 0,\n                \"RearFogLamp\": 0,\n                \"LeftIndicator\": 0,\n                \"RightIndicator\": 0,\n                \"LowToHigLight\": 0\n            },\n            {\n                \"uid\": 2,\n                \"question\": \"夜间超越前方车辆\",\n                \"answer\": \"答案：变换远近光灯，完成操作后仍为近光\",\n                \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/2.mp3\",\n                \"DoubleJumpLamp\": 0,\n                \"ClearAnceLamp\": 1,\n                \"LowBeamLight\": 1,\n                \"HigBeamLight\": 0,\n                \"FrontFogLamp\": 0,\n                \"RearFogLamp\": 0,\n                \"LeftIndicator\": 0,\n                \"RightIndicator\": 0,\n                \"LowToHigLight\": 2\n            },\n            {\n                \"uid\": 3,\n                \"question\": \"夜间通过急弯\",\n                \"answer\": \"答案：远近光交替\",\n                \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/3.mp3\",\n                \"DoubleJumpLamp\": 0,\n                \"ClearAnceLamp\": 1,\n                \"LowBeamLight\": 1,\n                \"HigBeamLight\": 0,\n                \"FrontFogLamp\": 0,\n                \"RearFogLamp\": 0,\n                \"LeftIndicator\": 0,\n                \"RightIndicator\": 0,\n                \"LowToHigLight\": 2\n            },\n            {\n                \"uid\": 4,\n                \"question\": \"夜间通过拱桥\",\n                \"answer\": \"答案：变换远近光灯，完成操作后仍为近光\",\n                \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/4.mp3\",\n                \"DoubleJumpLamp\": 0,\n                \"ClearAnceLamp\": 1,\n                \"LowBeamLight\": 1,\n                \"HigBeamLight\": 0,\n                \"FrontFogLamp\": 0,\n                \"RearFogLamp\": 0,\n                \"LeftIndicator\": 0,\n                \"RightIndicator\": 0,\n                \"LowToHigLight\": 2\n            },\n            {\n                \"uid\": 9,\n                \"question\": \"夜间同方向近距离跟车行驶\",\n                \"answer\": \"答案：开启近光灯\",\n                \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/9.mp3\",\n                \"DoubleJumpLamp\": 0,\n                \"ClearAnceLamp\": 1,\n                \"LowBeamLight\": 1,\n                \"HigBeamLight\": 0,\n                \"FrontFogLamp\": 0,\n                \"RearFogLamp\": 0,\n                \"LeftIndicator\": 0,\n                \"RightIndicator\": 0,\n                \"LowToHigLight\": 0\n            },\n            {\n                \"uid\": 10,\n                \"question\": \"夜间在窄路、窄桥与机动车会车\",\n                \"answer\": \"答案：开启近光灯\",\n                \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/10.mp3\",\n                \"DoubleJumpLamp\": 0,\n                \"ClearAnceLamp\": 1,\n                \"LowBeamLight\": 1,\n                \"HigBeamLight\": 0,\n                \"FrontFogLamp\": 0,\n                \"RearFogLamp\": 0,\n                \"LeftIndicator\": 0,\n                \"RightIndicator\": 0,\n                \"LowToHigLight\": 0\n            },\n            {\n                \"uid\": 11,\n                \"question\": \"夜间机动车会车\",\n                \"answer\": \"答案：开启近光灯\",\n                \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/11.mp3\",\n                \"DoubleJumpLamp\": 0,\n                \"ClearAnceLamp\": 1,\n                \"LowBeamLight\": 1,\n                \"HigBeamLight\": 0,\n                \"FrontFogLamp\": 0,\n                \"RearFogLamp\": 0,\n                \"LeftIndicator\": 0,\n                \"RightIndicator\": 0,\n                \"LowToHigLight\": 0\n            },\n            {\n                \"uid\": 15,\n                \"question\": \"夜间在道路上发生事故，妨碍交通又难以移动\",\n                \"answer\": \"答案：开启示宽灯和危险警示灯\",\n                \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/15.mp3\",\n                \"DoubleJumpLamp\": 1,\n                \"ClearAnceLamp\": 1,\n                \"LowBeamLight\": 0,\n                \"HigBeamLight\": 0,\n                \"FrontFogLamp\": 0,\n                \"RearFogLamp\": 0,\n                \"LeftIndicator\": 0,\n                \"RightIndicator\": 0,\n                \"LowToHigLight\": 0\n            },\n            {\n                \"uid\": 0,\n                \"question\": \"模拟夜间考试完成请关闭所有灯光\",\n                \"answer\": \"答案：关闭所有灯光\",\n                \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/end.mp3\",\n                \"DoubleJumpLamp\": 0,\n                \"ClearAnceLamp\": 0,\n                \"LowBeamLight\": 0,\n                \"HigBeamLight\": 0,\n                \"FrontFogLamp\": 0,\n                \"RearFogLamp\": 0,\n                \"LeftIndicator\": 0,\n                \"RightIndicator\": 0,\n                \"LowToHigLight\": 0\n            }\n        ],\n        \"suiji\": [\n            {\n                \"uid\": 2\n            },\n            {\n                \"uid\": 3\n            },\n            {\n                \"uid\": 4\n            },\n            {\n                \"uid\": 11\n            },\n            {\n                \"uid\": 15\n            }\n        ]\n    },\n    \"status\": \"200\"\n}";
            //response = "{\n\"msg\": \"请求成功\",\n\"data\": {\n\"exam\": {\n\"0\": [\n{\n\"timuid\": 1\n},\n{\n\"timuid\": 1\n},\n{\n                    \"timuid\": 2\n                },\n                {\n                    \"timuid\": 1\n                }\n            ],\n            \"1\": [\n                {\n                    \"timuid\": 1\n                },\n                {\n                    \"timuid\": 1\n                },\n                {\n                    \"timuid\": 1\n                },\n                {\n                    \"timuid\": 1\n                }\n            ]\n        },\n        \"listvideo\": [\n            {\n                \"imgurl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight002.jpg\",\n                \"chexingId\": 1,\n                \"videourl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight002.mp4\",\n                \"videoname\": \"null\"\n            },\n            {\n                \"imgurl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight001.jpg\",\n                \"chexingId\": 1,\n                \"videourl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight001.mp4\",\n                \"videoname\": \"2001709210000032101.mp4\"\n            }\n        ],\n        \"listnewold\": [\n            {\n                \"examaudio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/start_new.mp3\",\n                \"uid\": 2,\n                \"broadcastend\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/ding.mp3\",\n                \"addTime\": \"2019-06-15 12:03:19\",\n                \"examtip\": \"下面将进行模拟夜间灯光的考试，请在语音播报叮一声结束后5秒内完成操作\",\n                \"type\": \"1\"\n            },\n            {\n                \"examaudio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/start_old.mp3\",\n                \"uid\": 1,\n                \"broadcastend\": \"http://jiakaojingling.oss-cn-qingdao.aliyuncs.com/static/upload/driver/90041560756849403.png\",\n                \"addTime\": \"2019-06-15 12:03:03\",\n                \"examtip\": \"下面将进行模拟夜间行驶场景灯光使用的考试，请按语音指令在5秒内做出相应的灯光操作\",\n                \"type\": \"2\"\n            }\n        ],\n        \"questions\": [\n            {\n                \"RightIndicator\": 0,\n                \"question\": \"请开启前照灯\",\n                \"DoubleJumpLamp\": 0,\n                \"HigBeamLight\": 0,\n                \"LeftIndicator\": 0,\n                \"FrontFogLamp\": 0,\n                \"RearFogLamp\": 0,\n                \"uid\": 1,\n                \"LowBeamLight\": 1,\n                \"answer\": \"答案：开启大灯，注意是近光\",\n                \"ClearAnceLamp\": 1,\n                \"LowToHigLight\": 0,\n                \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/1.mp3\"\n            },\n            {\n                \"RightIndicator\": 0,\n                \"question\": \"夜间超越前方车辆\",\n                \"DoubleJumpLamp\": 0,\n                \"HigBeamLight\": 0,\n                \"LeftIndicator\": 0,\n                \"FrontFogLamp\": 0,\n                \"RearFogLamp\": 0,\n                \"uid\": 2,\n                \"LowBeamLight\": 1,\n                \"answer\": \"答案：变换远近光灯，完成操作后仍为近光\",\n                \"ClearAnceLamp\": 1,\n                \"LowToHigLight\": 1,\n                \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/2.mp3\"\n            }\n        ],\n        \"suiji\": [\n            {\n                \"uid\": 1\n            },\n            {\n                \"uid\": 2\n            }\n        ]\n    },\n    \"status\": \"200\"\n}";
            //response = "{\n  \"msg\": \"请求成功\",\n  \"data\": {\n    \"exam\": [\n      {\n        \"subid\": \"1,\"\n      },\n      {\n        \"subid\": \"1,2,\"\n      }\n    ],\n    \"listvideo\": [\n      {\n        \"imgurl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight001.jpg\",\n        \"chexingId\": 1,\n        \"videourl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight001.mp4\",\n        \"videoname\": \"2001709210000032101.mp4\"\n      }\n    ],\n    \"listnewold\": [\n      {\n        \"examaudio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/start_new.mp3\",\n        \"uid\": 2,\n        \"broadcastend\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/ding.mp3\",\n        \"addTime\": \"2019-06-15 12:03:19\",\n        \"examtip\": \"下面将进行模拟夜间灯光的考试，请在语音播报叮一声结束后5秒内完成操作\",\n        \"type\": 1\n      },\n      {\n        \"examaudio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/start_old.mp3\",\n        \"uid\": 1,\n        \"broadcastend\": \"http://jiakaojingling.oss-cn-qingdao.aliyuncs.com/static/upload/driver/90041560756849403.png\",\n        \"addTime\": \"2019-06-15 12:03:03\",\n        \"examtip\": \"下面将进行模拟夜间行驶场景灯光使用的考试，请按语音指令在5秒内做出相应的灯光操作\",\n        \"type\": 2\n      }\n    ],\n    \"questions\": [\n      {\n        \"RightIndicator\": 0,\n        \"question\": \"请开启前照灯\",\n        \"DoubleJumpLamp\": 1,\n        \"HigBeamLight\": 0,\n        \"LeftIndicator\": 0,\n        \"FrontFogLamp\": 0,\n        \"RearFogLamp\": 0,\n        \"uid\": 1,\n        \"LowBeamLight\": 1,\n        \"score\": 5,\n        \"answer\": \"答案：开启大灯，注意是近光\",\n        \"ClearAnceLamp\": 1,\n        \"LowToHigLight\": 0,\n        \"audio\": \"http://jiakaojingling.oss-cn-qingdao.aliyuncs.com/static/upload/driver/24111562411730757.mp3\"\n      },\n      {\n        \"RightIndicator\": 0,\n        \"question\": \"夜间超越前方车辆\",\n        \"DoubleJumpLamp\": 0,\n        \"HigBeamLight\": 0,\n        \"LeftIndicator\": 0,\n        \"FrontFogLamp\": 0,\n        \"RearFogLamp\": 0,\n        \"uid\": 2,\n        \"LowBeamLight\": 1,\n        \"score\": 5,\n        \"answer\": \"答案：变换远近光灯，完成操作后仍为近光\",\n        \"ClearAnceLamp\": 1,\n        \"LowToHigLight\": 1,\n        \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/2.mp3\"\n      },\n      {\n        \"RightIndicator\": 0,\n        \"question\": \"测试\",\n        \"DoubleJumpLamp\": 0,\n        \"HigBeamLight\": 0,\n        \"LeftIndicator\": 0,\n        \"FrontFogLamp\": 0,\n        \"RearFogLamp\": 0,\n        \"uid\": 3,\n        \"LowBeamLight\": 0,\n        \"score\": 5,\n        \"answer\": \"测试\",\n        \"ClearAnceLamp\": 0,\n        \"LowToHigLight\": 0,\n        \"audio\": \"http://jiakaojingling.oss-cn-qingdao.aliyuncs.com/static/upload/driver/52841561961663818.jpg\"\n      },\n      {\n        \"RightIndicator\": 0,\n        \"question\": \"测试2\",\n        \"DoubleJumpLamp\": 0,\n        \"HigBeamLight\": 0,\n        \"LeftIndicator\": 0,\n        \"FrontFogLamp\": 0,\n        \"RearFogLamp\": 0,\n        \"uid\": 4,\n        \"LowBeamLight\": 0,\n        \"score\": 5,\n        \"answer\": \"测试2\",\n        \"ClearAnceLamp\": 0,\n        \"LowToHigLight\": 1,\n        \"audio\": \"http://jiakaojingling.oss-cn-qingdao.aliyuncs.com/static/upload/driver/54751561961907063.jpg\"\n      },\n      {\n        \"RightIndicator\": 0,\n        \"question\": \"同方向近距离跟车行驶\",\n        \"DoubleJumpLamp\": 0,\n        \"HigBeamLight\": 0,\n        \"LeftIndicator\": 0,\n        \"FrontFogLamp\": 0,\n        \"RearFogLamp\": 0,\n        \"uid\": 6,\n        \"LowBeamLight\": 0,\n        \"score\": 5,\n        \"answer\": \"进光灯\",\n        \"ClearAnceLamp\": 0,\n        \"LowToHigLight\": 1,\n        \"audio\": \"http://jiakaojingling.oss-cn-qingdao.aliyuncs.com/static/upload/driver/7591562412129413.wav\"\n      }\n    ],\n    \"suiji\": [\n      1,\n      2\n    ]\n  },\n  \"status\": \"200\"\n}";

            response = UIManager.Instance.testContent;

        }
        else if (typeof(T).IsAssignableFrom(typeof(ResponseCarType)))
        {
            response = "{\n    \"msg\": \"请求成功\",\n    \"data\": {\n        \"carType\": [\n            {\n                \"image\": \"sangtana\",\n                \"uid\": 1,\n                \"chexingname\": \"桑塔纳捷达-旧灯光\",\n                \"chexingcode\": \"sangtana\",\n                \"type\": 2\n            },\n            {\n                \"image\": \"sangtana\",\n                \"uid\": 2,\n                \"chexingname\": \"桑塔纳捷达-新灯光\",\n                \"chexingcode\": \"sangtana\",\n                \"type\": 1\n            },\n            {\n                \"image\": \"ailishe\",\n                \"uid\": 3,\n                \"chexingname\": \"老爱丽舍-旧灯光\",\n                \"chexingcode\": \"ailishe\",\n                \"type\": 2\n            },\n            {\n                \"image\": \"ailishe\",\n                \"uid\": 4,\n                \"chexingname\": \"老爱丽舍-新灯光\",\n                \"chexingcode\": \"ailishe\",\n                \"type\": 1\n            },\n            {\n                \"image\": \"bentengb30\",\n                \"uid\": 5,\n                \"chexingname\": \"奔腾B30-旧灯光\",\n                \"chexingcode\": \"1\",\n                \"type\": 2\n            },\n            {\n                \"image\": \"bentengb30\",\n                \"uid\": 6,\n                \"chexingname\": \"奔腾B30-新灯光\",\n                \"chexingcode\": \"1\",\n                \"type\": 1\n            },\n            {\n                \"image\": \"ailishe2\",\n                \"uid\": 7,\n                \"chexingname\": \"新爱丽舍-旧灯光\",\n                \"chexingcode\": \"1\",\n                \"type\": 2\n            },\n            {\n                \"image\": \"ailishe2\",\n                \"uid\": 8,\n                \"chexingname\": \"新爱丽舍-新灯光\",\n                \"chexingcode\": \"1\",\n                \"type\": 1\n            }\n        ]\n    },\n    \"status\": \"200\"\n}";
            //response = "{\n\"msg\": \"请求成功\",\n    \"data\": {\n        \"carType\": [\n            {\n                \"image\": \"http://jiakaojingling.oss-cn-qingdao.aliyuncs.com/static/upload/driver/90041560756849403.png\",\n                \"uid\": 1,\n                \"chexingname\": \"桑塔纳捷达\",\n                \"chexingcode\": \"sangtana\",\n                \"type\": 2\n            },\n            {\n                \"image\": \"http://jiakaojingling.oss-cn-qingdao.aliyuncs.com/static/upload/driver/90041560756849403.png\",\n                \"uid\": 2,\n                \"chexingname\": \"桑塔纳捷达\",\n                \"chexingcode\": \"sangtana\",\n                \"type\": 1\n            },\n            {\n                \"image\": \"http://jiakaojingling.oss-cn-qingdao.aliyuncs.com/static/upload/driver/90041560756849403.png\",\n                \"uid\": 3,\n                \"chexingname\": \"爱丽舍\",\n                \"chexingcode\": \"ailishe\",\n                \"type\": 2\n            },\n            {\n                \"image\": \"http://jiakaojingling.oss-cn-qingdao.aliyuncs.com/static/upload/driver/90041560756849403.png\",\n                \"uid\": 4,\n                \"chexingname\": \"爱丽舍\",\n                \"chexingcode\": \"ailishe\",\n                \"type\": 1\n            },\n            {\n                \"image\": \"http://jiakaojingling.oss-cn-qingdao.aliyuncs.com/static/upload/driver/90041560756849403.png\",\n                \"uid\": 5,\n                \"chexingname\": \"奔腾B30\",\n                \"chexingcode\": \"1\",\n                \"type\": 2\n            },\n            {\n                \"image\": \"http://jiakaojingling.oss-cn-qingdao.aliyuncs.com/static/upload/driver/90041560756849403.png\",\n                \"uid\": 6,\n                \"chexingname\": \"奔腾B30\",\n                \"chexingcode\": \"1\",\n                \"type\": 1\n            },\n            {\n                \"image\": \"http://jiakaojingling.oss-cn-qingdao.aliyuncs.com/static/upload/driver/90041560756849403.png\",\n                \"uid\": 7,\n                \"chexingname\": \"新爱丽舍\",\n                \"chexingcode\": \"1\",\n                \"type\": 2\n            },\n            {\n                \"image\": \"http://jiakaojingling.oss-cn-qingdao.aliyuncs.com/static/upload/driver/90041560756849403.png\",\n                \"uid\": 8,\n                \"chexingname\": \"新爱丽舍\",\n                \"chexingcode\": \"1\",\n                \"type\": 1\n            }\n        ]\n    },\n    \"status\": \"200\"\n}";
        }

        Debug.Log(response.ToString());

        retObject = LitJson.JsonMapper.ToObject<ResponseData<T>>(response);

        UIManager.Instance.CloseUI(uIWait);
        callback(retObject);
    }
}

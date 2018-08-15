using System;
using System.Collections.Generic;

public enum CarType
{
    DAZHONG = 1,
    AILISHE = 2
}
public enum CarVersion
{
    OLD = 1,
    NEW = 2
}

public class Data_Base { }

/// <summary>
/// 游戏配置文件
/// </summary>
public class GameConfig : Data_Base
{
    /// <summary>
    /// 配置版本
    /// </summary>
    public string version = "0";
    /// <summary>
    /// 考试开始语音
    /// </summary>
    public string exam_audio;
    public ExamTipData examtip_old;
    public ExamTipData examtip_new;
    /// <summary>
    /// 试题列表
    /// </summary>
    public Dictionary<string, QuestionData> questions = new Dictionary<string, QuestionData>();
    public Dictionary<string, ExamData> examList = new Dictionary<string, ExamData>();
    /// <summary>
    /// 视频教学
    /// </summary>
    public Dictionary<string, List<VideoData>> video = new Dictionary<string, List<VideoData>>();
    public bool showShare = false;
    /// <summary>
    /// 分享的内容
    /// </summary>
    public ShareData share;
}

public class ShareData : Data_Base
{
    public string title = "驾考精灵为驾考保驾护航";
    public string content = "驾考精灵百分百保过，您驾考的最优选择...";
    public string url = "http://www.ly502.com/";
    public string image = "http://app.jiakaojingling.com/jkjl/static/dengguang/shareImage2.png";
}

public class ExamTipData :Data_Base{
    public string exam_tip;
    public string exam_audio;
}

/// <summary>
/// 試題的數據結構
/// </summary>
public class QuestionData : Data_Base
{
    /// <summary>
    /// 轉語音的文字描述
    /// </summary>
    public string question;
    /// <summary>
    /// 试题答案文案.
    /// </summary>
    public string answer;
    /// <summary>
    /// 试题语音文件
    /// </summary>
    public string audio;

    /// <summary>
    /// 双闪灯
    /// </summary>
    public bool DoubleJumpLamp;
    /// <summary>
    /// 示廓灯
    /// </summary>
    public bool ClearAnceLamp;
    /// <summary>
    /// 近光灯
    /// </summary>
    public bool LowBeamLight;
    /// <summary>
    /// 远光灯
    /// </summary>
    public bool HigBeamLight;
    /// <summary>
    /// 前雾灯
    /// </summary>
    public bool FrontFogLamp;
    /// <summary>
    /// 后雾灯
    /// </summary>
    public bool RearFogLamp;
    /// <summary>
    /// 左指示器
    /// </summary>
    public bool LeftIndicator;
    /// <summary>
    /// 右指示器
    /// </summary>
    public bool RightIndicator;
    /// <summary>
    /// 远近灯光切换--两次
    /// </summary>
    public bool LowToHigLight;
}

/// <summary>
/// 车型试题列表顺序
/// </summary>
public class ExamData:Data_Base{
    /// <summary>
    /// 考试随机列表--多个表随机
    /// </summary>
    public List<List<string>> exam = new List<List<string>>();
    /// <summary>
    /// 随机练习列表--从此列表中随机
    /// </summary>
    public List<string> random = new List<string>();
}

/// <summary>
/// 车型视频数据结构
/// </summary>
public class VideoData : Data_Base
{
    public string imgurl;
    public string videourl;
    public string videoname;
}
public class AuthorizeData
{
    public string author = "loong";
    public bool authorize = true;
    public string authTime = "2068-8-1 0:0:0";
    public bool authExpire
    {
        get
        {
            DateTime dateTime = DateTime.Parse(authTime);
            return (DateTime.Now > dateTime);
        }
    }
}
////e.g.
//public class pendingChest : Data_Base
//{
//    public int index;
//    public int type;
//    public int value;
//    public int status;
//
//    public pendingCard card;
//
//
//    public static void Parse(LitJson.JsonData json, out pendingChest data)
//    {
//        data = new pendingChest();
//        try
//        {
//            data.index = int.Parse(json["index"].ToString());
//            data.type = int.Parse(json["type"].ToString());
//            data.value = int.Parse(json["value"].ToString());
//            data.status = int.Parse(json["status"].ToString());
//            if (json.Keys.Contains("pendingCard") && data.type != 1 && data.type != 2)
//            {
//                pendingCard.Parse(json["pendingCard"], out data.card, (pendingCard.EChestType)(data.type - 3));
//            }
//            else
//            {
//                data.card = null;
//            }
//        }
//        catch (System.Exception)
//        {
//            data = null;
//            throw;
//        }
//    }
//
//}

public class ProtocalData_Base { }
public class RequestData_Base : ProtocalData_Base { }
public class ResponseData_Base : ProtocalData_Base { }
public class ResponseData<T> where T : ResponseData_Base
{
    public string msg;
    public string status;
    public T data;
}

//登录
public class RequestOther : RequestData_Base
{
    public string uid;
    public int loginType;
    public string headImg;
    public string userName;
    public string equitment;
}
public class RequestLogin : RequestData_Base
{
    public string phone;
    public string password;
    public string equitment;
}
public class ResponseLogin : ResponseData_Base
{
    public string headImage;
    public string equitmentIdentity;
    public string userName;
    public int uid;
    public string phone;
    public int countyId;
    public string countyName;
    public int schoolId;
    public string schoolName;
    public int cityId;
    public string cityName;
    public int provinceId;
    public string provinceName;
    public int isOtherLogin;
    public int isNeedBind;
}
//忘记密码
public class RequestForgetPwd: RequestData_Base
{
    public string phone;
    public string password;
    public string code;
    public string equitment;
}
public class ResponseForgetPwd : ResponseData_Base
{
}
//验证码
public class RequestAuthCode:RequestData_Base
{
    public string phone;
    public string type;
    public string zuoti;
    public string equitmentTime;
}
public class ResponseAuthCode : ResponseData_Base
{
    public string code;
}
//免费注册
public class RequestFreeSignup : RequestData_Base
{
    public string phone;
    public string password;
    public string code;
    public string equitment;
}
public class ResponseFreeSignup : ResponseData_Base
{

}


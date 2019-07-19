using System;
using System.Collections.Generic;

/// <summary>
/// 选择车型的枚举--【枚举类型不要随便更改】题库会使用字符串作为键来获取数据
/// </summary>
public enum CarType     //当前把字符串获取方式改为了大写--等服务器返回数据后可以修改
{
    DaZhong = 1,
    AiLiShe = 2,
    BenTengB30 = 3,
    AiLiShe2015 = 4
}
public enum CarUID
{
    SangTaNa_Old = 1,
    SangTaNa_New = 2,
    AiLiShe_Old = 3,
    AiLiShe_New = 4,
    BenTengB30_Old = 5,
    BenTengB30_New = 6,
    AiLiShe2_Old = 7,
    AiLiShe2_New = 8
}
public enum CarVersion
{
    NEW = 1,
    OLD = 2
}

public class Data_Base { }

public class GameVersion : Data_Base
{
    /// <summary>
    /// 配置版本
    /// </summary>
    public string version = "0.0";
    public int AppVersion
    {
        get
        {
            return int.Parse(version.Split('.')[0]);
        }
    }
    public int ResVersion
    {
        get
        {
            return int.Parse(version.Split('.')[1]);
        }
    }
}
/// <summary>
/// 游戏配置文件
/// </summary>
public class GameConfig : Data_Base
{
    /// <summary>
    /// 配置版本
    /// </summary>
    public string version = "0.0";
    /// <summary>
    /// 考试开始语音
    /// </summary>
    public ExamTipData examtip_old;
    public ExamTipData examtip_new;
    /// <summary>
    /// 试题列表
    /// </summary>
    public Dictionary<string, cQuestionData> questions = new Dictionary<string, cQuestionData>();
    public Dictionary<string, ExamData> examList = new Dictionary<string, ExamData>();
    /// <summary>
    /// 视频教学
    /// </summary>
    public Dictionary<string, List<VideoData>> video = new Dictionary<string, List<VideoData>>();
    public bool ios_audit = false;
    public bool showShare = false;
    /// <summary>
    /// 分享的内容
    /// </summary>
    public ShareData share;

    public int AppVersion
    {
        get
        {
            return int.Parse(version.Split('.')[0]);
        }
    }
    public int ResVersion
    {
        get
        {
            return int.Parse(version.Split('.')[1]);
        }
    }
}

public class ShareData : Data_Base
{
    public string title = "驾考精灵为驾考保驾护航";
    public string content = "驾考精灵百分百保过，您驾考的最优选择...";
    public string url = "http://www.ly502.com/";
    public string image = "http://app.jiakaojingling.com/jkjl/static/dengguang/shareImage2.png";
}

public class ExamTipData : Data_Base
{
    public string exam_tip;
    public string exam_audio;
    public string broadcast_end;
}

/// <summary>
/// 試題的數據結構
/// </summary>
public class cQuestionData : Data_Base
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
public class ExamData : Data_Base
{
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
//灯光账号登录
public class RequestLightLogin : RequestData_Base
{
    public string loginAccount;
    public string password;
}
public class ResponseLightLogin : ResponseData_Base
{

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
    public int uid;
    public string userName;
    public string headImage;
    public string equitmentIdentity;
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
    public string loginAccount;
}

//获取驾校车型
public class RequestCarType : RequestData_Base
{
    public string loginAccount;
}
public class ResponseCarType : ResponseData_Base
{
    public CarTypeData[] carType;
}

/// <summary>
/// 车型数据
/// </summary>
public class CarTypeData
{
    public int uid;
    public int type;
    public string image;
    public string chexingname;
    public string chexingcode;
}

/// <summary>
/// 获取车型信息
/// </summary>
public class RequestCarInfo : RequestData_Base
{
    public string cart_type;
    public string loginAccount;
}
public class ResponseCarInfo : ResponseData_Base
{
    public class examClass
    {
        public string subid;
    }
    //public Dictionary<string, List<examQuestion>> exam = new Dictionary<string, List<examQuestion>>();
    //private List<examClass> _exam;
    public List<examClass> exam;
    public List<VideoData> listvideo;
    public List<TypeModel> listnewold;
    public List<sQuestionData> questions;
    //public List<suijiQuestion> suiji;
    public List<int> suiji;
    private List<List<int>> exams;

    private TypeModel typeModel;
    public TypeModel TypeModel
    {
        get
        {
            if (typeModel == null)
            {
                for (int i = 0; i < listnewold.Count; i++)
                {
                    if (listnewold[i].type == GameDataMgr.Instance.carTypeData.type)
                    {
                        typeModel = listnewold[i];
                        break;
                    }
                }
            }
            return typeModel;
        }
    }
    public TypeModel GetTypeModel()
    {
        TypeModel model = null;
        for (int i = 0; i < listnewold.Count; i++)
        {
            if (listnewold[i].type == GameDataMgr.Instance.carTypeData.type)
            {
                model = listnewold[i];
                break;
            }
        }
        return model;
    }

    /// <summary>
    /// 获取所有试题
    /// </summary>
    /// <returns></returns>
    public List<List<int>> GetExams()
    {
        if (exams == null)
        {
            exams = new List<List<int>>();
            for (int i = 0; i < exam.Count; i++)
            {
                string[] qids = exam[i].subid.Split(',');
                exams.Add(new List<int>());
                for (int j = 0; j < qids.Length; j++)
                {
                    if (!string.IsNullOrEmpty(qids[j]))
                    {
                        exams[i].Add(int.Parse(qids[j]));
                    }
                }
            }
        }

        List<List<int>> examList = new List<List<int>>(exams);
        return examList;
    }
    /// <summary>
    /// 获取一套考试题Id列表
    /// </summary>
    /// <returns>The exam list.</returns>
    public List<int> GetExamList()
    {
        List<List<int>> examList = GetExams();
        Random rd = new Random();
        int index = rd.Next(0, examList.Count);
        return examList[index];
    }
    /// <summary>
    /// 获取某套试题的列表
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public List<int> GetExamList(int index)
    {
        List<List<int>> examList = GetExams();
        if (index >= 0 && index < examList.Count)
        {
            return examList[index];
        }
        else
        {
            return examList[0];
        }
    }

    /// <summary>
    /// 通过试题Id获取试题
    /// </summary>
    /// <returns>The question with identifier.</returns>
    /// <param name="qid">Qid.</param>
    public sQuestionData GetQuestionWithId(int qid)
    {
        for (int i = 0; i < questions.Count; i++)
        {
            if (questions[i].uid == qid)
            {
                return questions[i];
            }
        }
        Random rd = new Random();
        int index = rd.Next(0, questions.Count);
        return questions[index];
    }
}
/// <summary>
/// 考试题列表
/// </summary>
public class examData
{
    public Dictionary<string, List<examQuestion>> exam;
}

/// <summary>
/// 考试题ID
/// </summary>
public class examQuestion
{
    public string timuid;
}
/// <summary>
/// 随机题ID
/// </summary>
//public class suijiQuestion
//{
//    public int uid;
//}
///// <summary>
///// 教程视频
///// </summary>
//public class examVideo: VideoData
//{
//    //public string imgurl;
//    public int chexingId;
//    //public string videourl;
//    //public string videoname;
//}
/// <summary>
/// 车型模式
/// </summary>
public class TypeModel
{
    public int uid;
    public int type;
    public string examaudio;
    public string broadcastend;
    public string addTime;
    public string examtip;
}
public class sQuestionData
{
    public int uid;
    public string question;
    public string answer;
    public string audio;
    public int score;
    public int RightIndicator;
    public int DoubleJumpLamp;
    public int HigBeamLight;
    public int LeftIndicator;
    public int FrontFogLamp;
    public int RearFogLamp;
    public int LowBeamLight;
    public int ClearAnceLamp;
    public int LowToHigLight;


    public sQuestionData() { }
    public sQuestionData(cQuestionData data)
    {
        this.uid = -1;
        this.question = data.question;
        this.answer = data.answer;
        this.audio = data.audio;

        this.RightIndicator = (data.RightIndicator ? 1 : 0);
        this.DoubleJumpLamp = (data.DoubleJumpLamp ? 2 : 0);
        this.HigBeamLight = (data.HigBeamLight ? 1 : 0);
        this.LeftIndicator = (data.LeftIndicator ? 1 : 0);
        this.FrontFogLamp = (data.FrontFogLamp ? 1 : 0);
        this.RearFogLamp = (data.RearFogLamp ? 1 : 0);
        this.LowBeamLight = (data.LowBeamLight ? 1 : 0);
        this.ClearAnceLamp = (data.ClearAnceLamp ? 1 : 0);
        this.LowToHigLight = (data.LowToHigLight ? 1 : 0);
    }
}


//忘记密码
public class RequestForgetPwd : RequestData_Base
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
public class RequestAuthCode : RequestData_Base
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


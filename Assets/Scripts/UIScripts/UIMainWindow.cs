using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainWindow : UIWindow
{
    [System.Serializable]
    public class BtnCarType
    {
        public int uid;
        public CarType carType;
        public CarVersion carVersion;
        public Button btnEnter;
    }

    public List<BtnCarType> btnList = new List<BtnCarType>();

    public Button btnJump;
    public Button btnShare;
    public Button btnVow;
    public Button btnFlow;
    public Button btnPolicy;

    public ScrollRect scrollView;
    public Transform typeList;


    public override void OnCreate()
    {
        base.OnCreate();

        btnJump.onClick.AddListener(OnClickJump);
        btnShare.onClick.AddListener(OnClickShare);
        btnVow.onClick.AddListener(OnClickVow);
        btnFlow.onClick.AddListener(OnClickFlow);
        btnPolicy.onClick.AddListener(OnClickPolicy);
        //#if CHAPTER_ONE
        //List<string> types = new List<string> { CarType.DaZhong.ToString(), CarType.AiLiShe.ToString(), CarType.BenTengB30.ToString(),CarType.AiLiShe2015.ToString() };
        //for (int i = 0; i < btnList.Count; i++)
        //{
        //    var btnEnter = btnList[i];

        //    if (!types.Contains(btnEnter.carType.ToString()))
        //    {
        //        btnEnter.btnEnter.gameObject.SetActive(false);
        //    }
        //    else
        //    {
        //        btnEnter.btnEnter.gameObject.SetActive(true);
        //        btnEnter.btnEnter.onClick.AddListener(() =>
        //        {
        //            GameDataMgr.Instance.carType = btnEnter.carType;
        //            GameDataMgr.Instance.carVersion = btnEnter.carVersion;
        //            ShowDetailWindow();
        //        });
        //    }
        //}
        //#elif CHAPTER_TWO
        CarTypeData[] cartypes = GameDataMgr.Instance.ResponseCarType.carType;
        GameObject prefab = ResourcesMgr.Instance.LoadUIPrefab("CarTypeItem");
        for (int i = 0; i < cartypes.Length; i++)
        {
            GameObject go = Instantiate(prefab, typeList);
            CarTypeItem carType = go.GetComponent<CarTypeItem>();
            carType.InitWith(cartypes[i], OnClickCarType);
        }


        bool isScroll = (cartypes.Length > 4);// (typeList.transform as RectTransform).sizeDelta.x > (scrollView.transform as RectTransform).sizeDelta.x;

        scrollView.enabled = isScroll;

//#endif
    }

    void OnClickCarType(CarTypeData typeData)
    {
        RequestCarInfo param = new RequestCarInfo
        {
            cart_type = typeData.uid.ToString(),
            loginAccount = GameDataMgr.Instance.ResponseLogin.loginAccount
        };
        LoginManager.Instance.SendGetCarInfo<ResponseCarInfo>(param, (ret) =>
        {
            GameDataMgr.Instance.carTypeData = typeData;
            GameDataMgr.Instance.carInfo = ret.data;
            ShowDetailWindow();
        });
    }


    /// <summary>
    /// 检测是是否进入视频界面
    /// </summary>
    void ShowDetailWindow()
    {
//#if CHAPTER_ONE
//        List<VideoData> videoDatas = ConfigDataMgr.Instance.GetVideoList(GameDataMgr.Instance.carType);
//        if (videoDatas.Count > 0)
//        {
//            UIDetailWindow uiDetailWindow = UIManager.Instance.OpenUI<UIDetailWindow>();
//        }
//        else
//        {
//            SwitchSceneMgr.Instance.SwitchToExam();
//        }
//#elif CHAPTER_TWO
        if (GameDataMgr.Instance.carInfo.listvideo.Count>0)
        {
            /*UIDetailWindow uiDetailWindow = */UIManager.Instance.OpenUI<UIDetailWindow>();
        }
        else
        {
            SwitchSceneMgr.Instance.SwitchToExam();
        }
//#endif
    }

    /// <summary>
    /// 跳转驾考精灵
    /// </summary>
    void OnClickJump()
    {
        GlobalManager.JumpToAPP();
    }
    /// <summary>
    /// 开启分享界面
    /// </summary>
    void OnClickShare()
    {
        UIManager.Instance.OpenUI<UIShareWindow>();
    }
    /// <summary>
    /// 查看驾考宣言
    /// </summary>
    void OnClickVow()
    {
        UIManager.Instance.OpenUI<UIVowDialog>();
    }
    /// <summary>
    /// 查看驾考流程
    /// </summary>
    void OnClickFlow()
    {
        UIManager.Instance.OpenUI<UIFlowDialog>();
    }
     /// <summary>
     /// 查看隐私政策
     /// </summary>
    void OnClickPolicy()
    {
        UIManager.Instance.OpenUI<UIPolicyDetialDialog>();
    }
}

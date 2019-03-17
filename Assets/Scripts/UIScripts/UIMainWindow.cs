using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainWindow : UIWindow
{
    [System.Serializable]
    public class BtnCarType{
        public CarType carType;
        public CarVersion carVersion;
        public Button btnEnter;
    }

    public List<BtnCarType> btnList = new List<BtnCarType>();

    public Button btnJump;
    public Button btnShare;
    public Button btnVow;
    public Button btnFlow;




    public override void OnCreate()
    {
        base.OnCreate();

        btnJump.onClick.AddListener(OnClickJump);
        btnShare.onClick.AddListener(OnClickShare);
        btnVow.onClick.AddListener(OnClickVow);
        btnFlow.onClick.AddListener(OnClickFlow);


        List<string> types = new List<string> { CarType.DaZhong.ToString(), CarType.AiLiShe.ToString(), CarType.BenTengB30.ToString(),CarType.AiLiShe2015.ToString() };
        for (int i = 0; i < btnList.Count; i++)
        {
            var btnEnter = btnList[i];

            if (!types.Contains(btnEnter.carType.ToString()))
            {
                btnEnter.btnEnter.gameObject.SetActive(false);
            }
            else
            {
                btnEnter.btnEnter.onClick.AddListener(() =>
                {
                    GameDataMgr.Instance.carType = btnEnter.carType;
                    GameDataMgr.Instance.carVersion = btnEnter.carVersion;
                    ShowDetailWindow();
                });
            }
        }

    }

    /// <summary>
    /// 检测是是否进入视频界面
    /// </summary>
    void ShowDetailWindow()
    {
        List<VideoData> videoDatas;
        if (ConfigDataMgr.Instance.gameConfig.video.ContainsKey(GameDataMgr.Instance.carType.ToString().ToUpper()))
        {
            videoDatas = ConfigDataMgr.Instance.gameConfig.video[GameDataMgr.Instance.carType.ToString().ToUpper()];
        }
        else
        {
            videoDatas = new List<VideoData>();
        }

        if (videoDatas.Count > 0)
        {
            UIDetailWindow uiDetailWindow = UIManager.Instance.OpenUI<UIDetailWindow>();
        }
        else
        {
            SwitchSceneMgr.Instance.SwitchToExam();
        }
    }

    /// <summary>
    /// 跳转驾考精灵
    /// </summary>
    void OnClickJump(){
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainWindow : UIWindow
{
    public Button btnJieda_Old;
    public Button btnAilishe_Old;

    public Button btnJieda_New;
    public Button btnAilishe_New;

    public Button btnJump;
    public Button btnShare;
    public Button btnVow;
    public Button btnFlow;


    public override void OnCreate()
    {
        base.OnCreate();
        btnJieda_Old.onClick.AddListener(OnClickJiedaOld);
        btnAilishe_Old.onClick.AddListener(OnClickAilisheOld);

        btnJieda_New.onClick.AddListener(OnClickJiedaNew);
        btnAilishe_New.onClick.AddListener(OnClickAilisheNew);

        btnJump.onClick.AddListener(OnClickJump);
        btnShare.onClick.AddListener(OnClickShare);
        btnVow.onClick.AddListener(OnClickVow);
        btnFlow.onClick.AddListener(OnClickFlow);
    }

    /// <summary>
    /// 检测是是否进入视频界面
    /// </summary>
    void ShowDetailWindow()
    {
        List<VideoData> videoDatas;
        if (ConfigDataMgr.Instance.gameConfig.video.ContainsKey(GameDataMgr.Instance.carType.ToString()))
        {
            videoDatas = ConfigDataMgr.Instance.gameConfig.video[GameDataMgr.Instance.carType.ToString()];
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

    void OnClickJiedaOld(){
        GameDataMgr.Instance.carType = CarType.DAZHONG;
        GameDataMgr.Instance.carVersion = CarVersion.OLD;
        ShowDetailWindow();
    }
    void OnClickAilisheOld(){
        GameDataMgr.Instance.carType = CarType.AILISHE;
        GameDataMgr.Instance.carVersion = CarVersion.OLD;
        ShowDetailWindow();
    }
    /// <summary>
    /// Ons the click jieda.
    /// </summary>
    void OnClickJiedaNew()
    {
        GameDataMgr.Instance.carType = CarType.DAZHONG;
        GameDataMgr.Instance.carVersion = CarVersion.NEW;
        ShowDetailWindow();
    }
    /// <summary>
    /// Ons the click ailishe.
    /// </summary>
    void OnClickAilisheNew()
    {
        GameDataMgr.Instance.carType = CarType.AILISHE;
        GameDataMgr.Instance.carVersion = CarVersion.NEW;
        ShowDetailWindow();
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

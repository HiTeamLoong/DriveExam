﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIDetailWindow : UIWindow
{
    public Button btnReturn;
    public Button btnStart;

    public PlayVideoController playVideoController;

    public override void OnCreate()
    {
        base.OnCreate();
        btnReturn.onClick.AddListener(OnClickReturn);
        btnStart.onClick.AddListener(OnClickStart);

//#if CHAPTER_ONE
//        playVideoController.InitWith(ConfigDataMgr.Instance.gameConfig.video[GameDataMgr.Instance.carType.ToString().ToUpper()]);
//#elif CHAPTER_TWO
        playVideoController.InitWith(GameDataMgr.Instance.carInfo.listvideo);
//#endif

    }

    /// <summary>
    /// Ons the click return.
    /// </summary>
    void OnClickReturn()
    {
        UIManager.Instance.CloseUI(this);
    }
    /// <summary>
    /// Ons the click start.
    /// </summary>
    void OnClickStart()
    {
        //AsyncOperation async = SceneManager.LoadSceneAsync("ExamScene");
        //UILoadingWindow uiLoadingWindow = UIManager.Instance.OpenUI<UILoadingWindow>();
        //uiLoadingWindow.InitWith(async);
        playVideoController.IsPlay = false;
        SwitchSceneMgr.Instance.SwitchToExam();
    }
}

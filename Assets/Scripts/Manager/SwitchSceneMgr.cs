﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneMgr : XSingleton<SwitchSceneMgr>
{
    public readonly string StartScene = "StartScene";
    public readonly string MainScene = "MainScene";
    public readonly string ExamScene = "ExamScene";
    /// <summary>
    /// 进入考试场景
    /// </summary>
    public void SwitchToExam(Callback callback = null)
    {
        AuthorizeData auth = ConfigDataMgr.instance.authorizeData;
        if (!auth.authorize||auth.authExpire)
        {
            UITipsDialog.ShowTips("软件未授权或授权到期");
            return;
        }

        Callback LoadFinish = () =>
        {
            switch (GameDataMgr.instance.carType)
            {
                case CarType.DaZhong:
                    UIManager.Instance.OpenUI<UIExamWindowDaZhong>();
                    break;
                case CarType.AiLiShe:
                    UIManager.Instance.OpenUI<UIExamWindowAiLiShe>();
                    break;
                case CarType.BenTengB30:
                    UIManager.Instance.OpenUI<UIExamWindowBenTengB30>();
                    break;
                case CarType.AiLiShe2015:
                    //TODO --添加新款爱丽舍车型
                    break;
            }
            if (callback != null)
            {
                callback();
            }
        };

        if (!ConfigDataMgr.instance.gameConfig.ios_audit)
        {
            UILoadingWindow uiLoadingWindow = UIManager.Instance.OpenUI<UILoadingWindow>();
            AsyncOperation async = SceneManager.LoadSceneAsync(ExamScene);

            uiLoadingWindow.InitWith(async, LoadFinish, true);
        }
        else
        {
            UIWaitDialog uIWait = UIManager.Instance.OpenUI<UIWaitDialog>();
            ResourcesMgr.Instance.DownLoadAudioResource(ConfigDataMgr.Instance.gameConfig, (result, prog) =>
            {
                if (result && prog >= 1.0f)
                {
                    UIManager.Instance.CloseUI(uIWait);
                    SceneManager.LoadScene(ExamScene);
                    LoadFinish();
                }
            });
        }
    }
    /// <summary>
    /// 进入主场景
    /// </summary>
    public void SwitchToMain(bool loading = true, Callback callback = null)
    {
        if (loading && !ConfigDataMgr.instance.gameConfig.ios_audit)
        {
            UILoadingWindow uiLoadingWindow = UIManager.Instance.OpenUI<UILoadingWindow>();
            AsyncOperation async = SceneManager.LoadSceneAsync(MainScene);
            uiLoadingWindow.InitWith(async, () =>
            {
                UIManager.Instance.OpenUI<UIMainWindow>();

                if (callback != null)
                {
                    callback();
                }
            });
        }
        else
        {
            SceneManager.LoadScene(MainScene);
            if (callback != null)
            {
                callback();
            }
        }
    }
}

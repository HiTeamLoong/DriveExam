using System.Collections;
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
        UILoadingWindow uiLoadingWindow = UIManager.Instance.OpenUI<UILoadingWindow>();
        AsyncOperation async = SceneManager.LoadSceneAsync(ExamScene);
        uiLoadingWindow.InitWith(async, () =>
        {
            //大众
            if (GameDataMgr.Instance.carType == CarType.DAZHONG)
            {
                UIManager.Instance.OpenUI<UIExamWindowDaZhong>();
            }
            //爱丽舍
            else
            {
                UIManager.Instance.OpenUI<UIExamWindowAiLiShe>();
            }
            if (callback != null)
            {
                callback();
            }
        });
    }
    /// <summary>
    /// 进入主场景
    /// </summary>
    public void SwitchToMain(bool loading = true, Callback callback = null)
    {
        if (loading)
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

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
        AuthorizeData auth = ConfigDataMgr.Instance.authorizeData;
        if (!auth.authorize || auth.authExpire)
        {
            UITipsDialog.ShowTips("软件未授权或授权到期");
            return;
        }
//#if CHAPTER_ONE
//        Callback LoadFinish = () =>
//        {
//            switch (GameDataMgr.instance.carType)
//            {
//                case CarType.DaZhong:
//                    UIManager.Instance.OpenUI<UIExamWindowDaZhong>();
//                    break;
//                case CarType.AiLiShe:
//                    UIManager.Instance.OpenUI<UIExamWindowAiLiShe>();
//                    break;
//                case CarType.BenTengB30:
//                    UIManager.Instance.OpenUI<UIExamWindowBenTengB30>();
//                    break;
//                case CarType.AiLiShe2015:
//                    UIManager.Instance.OpenUI<UIExamWindowAiLiShe2015>();
//                    break;
//            }
//            if (callback != null)
//            {
//                callback();
//            }
//        };

//        if (!ConfigDataMgr.instance.gameConfig.ios_audit)
//        {
//            UILoadingWindow uiLoadingWindow = UIManager.Instance.OpenUI<UILoadingWindow>();
//            AsyncOperation async = SceneManager.LoadSceneAsync(ExamScene);
//            uiLoadingWindow.InitWith(async, LoadFinish, true);
//        }
//        else
//        {
//            UILoadingDialog uiLoadingDialog = UIManager.Instance.OpenUI<UILoadingDialog>();
//            AsyncOperation async = SceneManager.LoadSceneAsync(ExamScene);
//            uiLoadingDialog.InitWith(async, LoadFinish, true);
//        }
//#elif CHAPTER_TWO
        Callback LoadFinish = () =>
        {
            switch ((CarUID)GameDataMgr.Instance.carTypeData.uid)
            {
                case CarUID.SangTaNa_Old:
                case CarUID.SangTaNa_New:
                    UIManager.Instance.OpenUI<UIExamWindowDaZhong>();
                    break;
                case CarUID.AiLiShe_Old:
                case CarUID.AiLiShe_New:
                    UIManager.Instance.OpenUI<UIExamWindowAiLiShe>();
                    break;
                case CarUID.BenTengB30_Old:
                case CarUID.BenTengB30_New:
                    UIManager.Instance.OpenUI<UIExamWindowBenTengB30>();
                    break;
                case CarUID.AiLiShe2_Old:
                case CarUID.AiLiShe2_New:
                    UIManager.Instance.OpenUI<UIExamWindowAiLiShe2015>();
                    break;
            }
            if (callback != null)
            {
                callback();
            }
        };

        UILoadingDialog uiLoadingDialog = UIManager.Instance.OpenUI<UILoadingDialog>();
        AsyncOperation async = SceneManager.LoadSceneAsync(ExamScene);
        uiLoadingDialog.InitWith(async, LoadFinish, true);
//#endif
    }

    /// <summary>
    /// 进入主场景
    /// </summary>
    public void SwitchToMain(bool loading = true, Callback callback = null)
    {
//#if CHAPTER_ONE
//        if (loading && !ConfigDataMgr.instance.gameConfig.ios_audit)
//#elif CHAPTER_TWO
        if (loading)
//#endif
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

            UIManager.Instance.OpenUI<UIMainWindow>();
            if (callback != null)
            {
                callback();
            }
        }
    }
}

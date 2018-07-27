using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneMgr : XSingleton<SwitchSceneMgr>
{
    public void SwitchToExam(){
        UILoadingWindow uiLoadingWindow = UIManager.Instance.OpenUI<UILoadingWindow>();
        AsyncOperation async = SceneManager.LoadSceneAsync("ExamSceneTest");
        uiLoadingWindow.InitWith(async);
    }
}

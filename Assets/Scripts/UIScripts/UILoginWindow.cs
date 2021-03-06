﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoginWindow : UIWindow
{
    public Text textState;
	public Text textPolicy;

    public LoginController loginController;

    [System.Serializable]
    public class ProgressGroup
    {
        public GameObject root;
        public Text textProg;
        public ProgressBar progressBar;
    }
    public ProgressGroup progressGroup;

    public enum State
    {
        None,
        Login,
        Update
    }
    private State uiState = State.None;

    public State UiState
    {
        get { return uiState; }
        set
        {
            if (uiState != value)
            {
                uiState = value;
                loginController.gameObject.SetActive(uiState == State.Login);
                progressGroup.root.SetActive(uiState == State.Update);
                if (uiState == State.Login)
                {
                    loginController.InitWith(OnLoginCallback);
                }
            }
        }
    }

    protected override void BindListener()
    {
        base.BindListener();
    }
    protected override void UnBindListener()
    {
        base.UnBindListener();
    }

    public override void OnCreate()
    {
        base.OnCreate();
        if (!PlayerPrefs.HasKey("POLICY"))
        {
            UIManager.Instance.OpenUI<UIPolicyDialog>();
        }
        textPolicy.GetComponent<Button>().onClick.AddListener(() =>
        {
            UIManager.Instance.OpenUI<UIPolicyDetialDialog>();
        });

	}


    public void SetLoginList()
    {
        UiState = State.Login;
        SetState("");
    }
    public void SetProgress(float prog)
    {
        UiState = State.Update;
        progressGroup.textProg.text = prog.ToString("P");
        progressGroup.progressBar.Value = prog;
    }
    public void SetState(string strState)
    {
        textState.text = strState;
    }

    void OnLoginCallback(ResponseLogin result)
    {
        if (result != null)
        {

            GameDataMgr.Instance.ResponseLogin = result;
//#if CHAPTER_ONE
//            SwitchSceneMgr.Instance.SwitchToMain(false, () =>
//            {
//                UIManager.Instance.OpenUI<UIMainWindow>();
//            });
//#elif CHAPTER_TWO
            RequestCarType param = new RequestCarType
            {
                loginAccount = result.loginAccount
            };
            LoginManager.Instance.SendGetCarType<ResponseCarType>(param, (ret) =>
            {
                GameDataMgr.Instance.ResponseCarType = ret.data;
                SwitchSceneMgr.Instance.SwitchToMain(false, () =>
                {
                    UIManager.Instance.OpenUI<UIMainWindow>();
                });
            });
//#endif
        }
    }
    //void OnClickLogin()
    //{
    //    //AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.GetAudioWithStr(ConfigDataMgr.ExamStart));
    //    UIManager.Instance.OpenUI<UIMainWindow>();
    //}

    //AudioObject audioObject;
    //void OnClickWechat()
    //{
    //    //UITipsDialog.ShowTips("此接口当前未开放");

    //    if (audioObject != null)
    //    {
    //        AudioSystemMgr.Instance.StopSoundByAudio(audioObject);
    //        audioObject = null;
    //    }
    //    else
    //    {
    //        audioObject = AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("right"), true);
    //    }
    //}
}

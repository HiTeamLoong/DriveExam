using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoadingWindow : UIDialog
{
    public ProgressBar progressBar;

    private AsyncOperation async;
    private Callback finishCallback;
    private bool isDownloadFail = false;
    private float downloadProg = 1f;
    private UIWaitDialog uIWait = null;

    public void InitWith(AsyncOperation async, Callback callback, bool checkRes = false)
    {
        this.async = async;
        async.allowSceneActivation = false;
        finishCallback = callback;

        if (checkRes)
        {
            downloadProg = 0f;
            ResourcesMgr.Instance.DownLoadAudioResource(ConfigDataMgr.Instance.gameConfig, DownLoadCallback);
        }
        if (ConfigDataMgr.Instance.gameConfig.ios_audit)
        {
            progressBar.gameObject.SetActive(false);
            uIWait = UIManager.Instance.OpenUI<UIWaitDialog>();
        }
    }

    public void DownLoadCallback(bool result, float prog)
    {
        if (!isDownloadFail)
        {
            if (result)
            {
                downloadProg = prog;
            }
            else
            {
                isDownloadFail = true;
                UIPrompDialog.ShowPromp(UIPrompDialog.PrompType.Confirm, "资源加载失败", "请退出游戏后检查网络设置？", confirm =>
                {
                    if (confirm)
                    {
                        Application.Quit();
                    }
                });
            }
        }
    }

    private void Update()
    {
        if (async != null)
        {

            if (!async.isDone)
            {

                if (progressBar.Value <= .9f)
                {
                    if (progressBar.Value < async.progress && progressBar.Value < downloadProg)
                    {
                        progressBar.Value += Time.deltaTime * Random.Range(0.2f, 1f);
                    }
                }
                else
                {
                    if (progressBar.Value < 1f && progressBar.Value < downloadProg)
                    {
                        progressBar.Value += Time.deltaTime * Random.Range(0.2f, 1f);
                    }
                    else
                    {
                        async.allowSceneActivation = true;
                    }

                }
            }
            else
            {
                if (ConfigDataMgr.Instance.gameConfig.ios_audit)
                {
                    UIManager.Instance.CloseUI(uIWait);
                }
                if (finishCallback != null)
                {
                    finishCallback();
                }
                UIManager.Instance.CloseUI(this);
            }

        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoadingDialog : UIWaitDialog
{
    private AsyncOperation async;
    private Callback finishCallback;
    private bool isDownloadFail = false;
    private float downloadProg = 1f;

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
                if (downloadProg >= 1f && async.progress >= 0.9f)
                {
                    async.allowSceneActivation = true;
                }
            }
            else
            {
                if (finishCallback != null)
                {
                    finishCallback();
                }
                UIManager.Instance.CloseUI(this);
            }

        }

    }
}

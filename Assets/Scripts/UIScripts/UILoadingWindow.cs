using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoadingWindow : UIDialog
{
    public ProgressBar progressBar;

    private AsyncOperation async;
    private Callback finishCallback;

    public void InitWith(AsyncOperation async, Callback callback)
    {
        this.async = async;
        async.allowSceneActivation = false;
        finishCallback = callback;
    }

    private void Update()
    {
        if (async != null)
        {

            if (!async.isDone)
            {

                if (progressBar.Value <= .9f)
                {
                    if (progressBar.Value < async.progress)
                    {
                        progressBar.Value += Time.deltaTime * Random.Range(0.2f, 1f);
                    }
                }
                else
                {
                    if (progressBar.Value < 1f)
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
                if (finishCallback != null)
                {
                    finishCallback();
                }
                UIManager.Instance.CloseUI(this);
            }

        }

    }

}

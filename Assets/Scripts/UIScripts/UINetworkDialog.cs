using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UINetworkDialog
{
    private const string title = "网络连接失败";
    private const string content = "网络连接失败，请连接网络后点击确定重试！";
    private static Callback successCallback;

    public static void CheckNetwork(Callback callback)
    {
        successCallback = callback;

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            successCallback();
        }
        else
        {
            UIPrompDialog.ShowPromp(UIPrompDialog.PrompType.Confirm, title, content, confirm =>
            {
                if (confirm)
                {
                    CheckNetwork(successCallback);
                }
            });
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShareWindow : UIWindow
{
    public Button btnLogout;
    public Button btnReturn;
    public Button btnShare;
    public override void OnCreate()
    {
        base.OnCreate();
        btnLogout.onClick.AddListener(OnClickLogout);
        btnReturn.onClick.AddListener(OnClickReturn);
        btnShare.onClick.AddListener(OnClickShare);
    }

    void OnClickLogout(){
        GameDataMgr.Instance.ResponseLogin = null;
        UILoginWindow uILoginWindow = UIManager.Instance.OpenUI<UILoginWindow>();
        uILoginWindow.SetLoginList();
    }
    void OnClickReturn(){
        UIManager.Instance.CloseUI(this);
    }
    void OnClickShare()
    {
        if (ConfigDataMgr.Instance.gameConfig.showShare)
        {
            ShareData shareData = ConfigDataMgr.Instance.shareData;
            GlobalManager.Instance.ShareWebpage(shareData.title, shareData.content, shareData.url, shareData.image);
        }else
        {
            UITipsDialog.ShowTips("此功能暂未开放");
        }
    }
}

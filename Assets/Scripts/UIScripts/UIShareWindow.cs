using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShareWindow : UIWindow
{
    public Button btnLogout;
    public Button btnReturn;

    public override void OnCreate()
    {
        base.OnCreate();
        btnLogout.onClick.AddListener(OnClickLogout);
        btnReturn.onClick.AddListener(OnClickReturn);
    }

    void OnClickLogout(){
        GameDataMgr.Instance.ResponseLogin = null;
        UILoginWindow uILoginWindow = UIManager.Instance.OpenUI<UILoginWindow>();
        uILoginWindow.SetLoginList();
    }
    void OnClickReturn(){
        UIManager.Instance.CloseUI(this);
    }
}

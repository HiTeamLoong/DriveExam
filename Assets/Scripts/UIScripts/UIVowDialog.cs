using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIVowDialog : UIDialog
{
    public Button btnClose;
    public Text textName;

    public override void OnCreate()
    {
        base.OnCreate();
        btnClose.onClick.AddListener(OnClickClose);
        textName.text = GameDataMgr.Instance.ResponseLogin.userName;
    }

    void OnClickClose()
    {
        UIManager.Instance.CloseUI(this);
    }
}

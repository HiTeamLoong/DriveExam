using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFlowDialog : UIDialog
{
    public Button btnClose;

    public override void OnCreate()
    {
        base.OnCreate();
        btnClose.onClick.AddListener(OnClickClose);
    }

    void OnClickClose()
    {
        UIManager.Instance.CloseUI(this);
    }
}

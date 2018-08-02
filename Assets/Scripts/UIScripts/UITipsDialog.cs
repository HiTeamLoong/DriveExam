using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITipsDialog : UIDialog
{
    public static void ShowTips(string tips, bool isKeep = false)
    {
        UITipsDialog uITipsDialog = UIManager.Instance.OpenUI<UITipsDialog>();
        uITipsDialog.InitWith(tips,isKeep);
    }
    public Text textTips;

    public void InitWith(string tips, bool isKeep = false)
    {
        textTips.text = tips;
        if (!isKeep)
        {
            XTime.Instance.AddTimer(2f, 1, () =>
            {
                UIManager.Instance.CloseUI(this);
            });
        }
    }
}

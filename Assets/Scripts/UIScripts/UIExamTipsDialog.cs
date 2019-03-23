using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIExamTipsDialog : UIDialog
{
    public Button btnClose;

    public GameObject help_Ailishe;
    public GameObject help_Dazhong;
    public GameObject help_Ailishe2015;
    public GameObject help_BentengB30;
    public override void OnCreate()
    {
        base.OnCreate();

        GameObject helpPrefab = null;
        switch (GameDataMgr.Instance.carType)
        {
            case CarType.DaZhong:
                helpPrefab = help_Dazhong;
                break;
            case CarType.AiLiShe:
                helpPrefab = help_Ailishe;
                break;
            case CarType.AiLiShe2015:
                helpPrefab = help_Ailishe2015;
                break;
            case CarType.BenTengB30:
                helpPrefab = help_BentengB30;
                break;
            default:
                break;
        }
        if (helpPrefab != null)
        {
            GameObject help = Instantiate(helpPrefab, transform);

        }
        btnClose.onClick.AddListener(OnClickClose);
    }

    void OnClickClose(){
        UIManager.Instance.CloseUI(this);
    }
}

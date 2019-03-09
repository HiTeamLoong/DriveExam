using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIExamTipsDialog : UIDialog
{
    public Button btnClose;

    public GameObject tipsAilishe;
    public GameObject tipsDazhong;
    public override void OnCreate()
    {
        base.OnCreate();
        tipsAilishe.SetActive(GameDataMgr.Instance.carType == CarType.AiLiShe);
        tipsDazhong.SetActive(GameDataMgr.Instance.carType == CarType.DaZhong);

        btnClose.onClick.AddListener(OnClickClose);
    }

    void OnClickClose(){
        UIManager.Instance.CloseUI(this);
    }
}

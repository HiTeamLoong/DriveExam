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
        tipsAilishe.SetActive(GameDataMgr.Instance.carType == CarType.AILISHE);
        tipsDazhong.SetActive(GameDataMgr.Instance.carType == CarType.DAZHONG);

        btnClose.onClick.AddListener(OnClickClose);
    }

    void OnClickClose(){
        UIManager.Instance.CloseUI(this);
    }
}

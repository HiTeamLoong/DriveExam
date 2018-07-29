using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIVideoDialog : UIDialog
{
    public PlayVideoController PlayVideoController;

    public override void OnCreate()
    {
        base.OnCreate();
        PlayVideoController.InitWith(ConfigDataMgr.Instance.gameConfig.video[GameDataMgr.Instance.carType.ToString()]);
    }
}

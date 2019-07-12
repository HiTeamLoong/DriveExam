using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIVideoDialog : UIDialog
{
    public PlayVideoController PlayVideoController;

    public override void OnCreate()
    {
        base.OnCreate();
//#if CHAPTER_ONE
//        PlayVideoController.InitWith(ConfigDataMgr.Instance.gameConfig.video[GameDataMgr.Instance.carType.ToString().ToUpper()]);
//#elif CHAPTER_TWO
        PlayVideoController.InitWith(GameDataMgr.Instance.carInfo.listvideo);
//#endif
    }
}

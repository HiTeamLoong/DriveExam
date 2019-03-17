using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIExamWindowAiLiShe2015 : UIExamWindowBase
{
    [Space(20)]
    public Image imgOpenLight;      //示廓灯标识
    public Image imgHeadNear;       //近光灯标识
    public Image imgHeadFar;        //远光灯标志
    public Image imgFrontFog;       //前雾灯标识
    public Image imgRearFog;        //后雾灯标志

    public Button btnControlRight;  //右转向
    public Button btnControlClose;  //关闭转向
    public Button btnControlLeft;   //左转向

    public ButtonState btsCantrolState;   //--灯光状态显示
    public ButtonState btsControlBackward1;  //远近切换
    public ButtonState btsControlBackward2;  //切换灯光

    public ButtonState btsDoubleJump;

    public Transform transControlRod;


    [System.Serializable]
    public class ControlRod
    {
        public GameObject objRoot;

        public Image imgHeadLight;
        //[HideInInspector]
        public Sprite sprHeadLight0 = null;
        public Sprite sprHeadLight1;
        public Sprite sprHeadLight2;

        public Image imgFogLight;
        //[HideInInspector]
        public Sprite sprFogLoghtNormal = null;
        public Sprite sprFogLightOpen;
        public Sprite sprFogLightClose;
    }


    public ControlRod controlRodForward;
    public ControlRod controlRodBackward1;
    public ControlRod controlRodBackward2;

    public override bool FrontFogSwitch
    {
        set
        {
            if (FrontFogSwitch != value)
            {
                base.FrontFogSwitch = value;
                //controlRodNormal.imgFogLight.sprite = RearFogSwitch ? controlRodNormal.sprFoglight2 : value ? controlRodNormal.sprFogLight1 : controlRodNormal.sprFogLight0;
                //controlRodBackward.imgFogLight.sprite = RearFogSwitch ? controlRodBackward.sprFoglight2 : value ? controlRodBackward.sprFogLight1 : controlRodBackward.sprFogLight0;

                imgFrontFog.DOFade(FrontFogLamp ? 1f : 0f, 0);
            }
        }
    }
    public override bool LeftIndicatorSwitch
    {
        set
        {
            if (LeftIndicatorSwitch != value)
            {
                base.LeftIndicatorSwitch = value;
                if (value)
                {
                    transControlRod.localEulerAngles = new Vector3(0, 0, 5);
                }
                else if (!RightIndicatorSwitch)
                {
                    transControlRod.localEulerAngles = new Vector3(0, 0, 0);
                }
            }
        }
    }
    public override bool RightIndicatorSwitch
    {
        set
        {
            if (RightIndicatorSwitch != value)
            {
                base.RightIndicatorSwitch = value;
                if (value)
                {
                    transControlRod.localEulerAngles = new Vector3(0, 0, -5);
                }
                else if (!LeftIndicatorSwitch)
                {
                    transControlRod.localEulerAngles = new Vector3(0, 0, 0);
                }
            }
        }
    }
    public override bool DoubleJumpSwitch
    {
        set
        {
            if (DoubleJumpSwitch != value)
            {
                base.DoubleJumpSwitch = value;
                (btsDoubleJump.button.targetGraphic as Image).sprite = value ? btsDoubleJump.sprSelect : btsDoubleJump.sprNormal;
            }
        }
    }

    public override bool FarHeadlightSwitch
    {
        set
        {
            if (FarHeadlightSwitch != value)
            {
                base.FarHeadlightSwitch = value;
                //TODO：UI标识
                //(btsControlState.button.targetGraphic as Image).sprite = value ? btsCantrolState.sprSelect : btsCantrolState.sprNormal;

                //imgHeadNear.DOFade(LowBeamLight ? 1f : 0f, 0);
                imgHeadFar.DOFade(HigBeamLight ? 1f : 0f, 0);
            }
        }
    }

    public override void OnCreate()
    {
        base.OnCreate();

    }

}

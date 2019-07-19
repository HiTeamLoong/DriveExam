using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIExamWindowAiLiShe : UIExamWindowBase
{
    [Space(20)]
    public Image imgOpenLight;
    public Image imgHeadNear;
    public Image imgHeadFar;
    public Image imgFrontFog;
    public Image imgRearFog;

    public Button btnControlRigth;      //右转向
    public Button btnControlClose;      //关闭转向
    public Button btnControlLeft;       //左转向

    public ButtonState btsCantrolState;   //--灯光状态显示
    public ButtonState btsControlBackward1;  //远近切换
    public ButtonState btsControlBackward2;  //切换灯光

    public ButtonState btsDoubleJump;       //双闪灯

    public Transform transControlRod;       //角度控制杆

    [System.Serializable]
    public class ControlRod
    {
        public GameObject objRoot;

        public Image imgHeadLight;
        public Sprite sprHeadLight0;
        public Sprite sprHeadLight1;
        public Sprite sprHeadLight2;

        public Image imgFogLight;
        public Sprite sprFogLight0;
        public Sprite sprFogLight1;
        public Sprite sprFoglight2;
    }
    public ControlRod controlRodNormal;    //默认状态
    public ControlRod controlRodBackward;   //后掰状态

    #region SwitchModule

    public override bool ClearanceSwitch
    {
        set
        {
            if (ClearanceSwitch != value)
            {
                base.ClearanceSwitch = value;
                controlRodNormal.imgHeadLight.sprite = value ? controlRodNormal.sprHeadLight1 : HeadlightSwitch ? controlRodNormal.sprHeadLight2 : controlRodNormal.sprHeadLight0;
                controlRodBackward.imgHeadLight.sprite = value ? controlRodBackward.sprHeadLight1 : HeadlightSwitch ? controlRodBackward.sprHeadLight2 : controlRodBackward.sprHeadLight0;

                imgOpenLight.DOFade((ClearanceSwitch || HeadlightSwitch) ? 1f : 0f, 0);

                imgFrontFog.DOFade(FrontFogLamp ? 1f : 0f, 0);
                imgRearFog.DOFade(RearFogLamp ? 1f : 0f, 0);
            }
        }
    }
    public override bool HeadlightSwitch
    {
        set
        {
            if (HeadlightSwitch != value)
            {
                base.HeadlightSwitch = value;
                controlRodNormal.imgHeadLight.sprite = value ? controlRodNormal.sprHeadLight2 : ClearanceSwitch ? controlRodNormal.sprHeadLight1 : controlRodNormal.sprHeadLight0;
                controlRodBackward.imgHeadLight.sprite = value ? controlRodBackward.sprHeadLight2 : ClearanceSwitch ? controlRodBackward.sprHeadLight1 : controlRodBackward.sprHeadLight0;

                imgOpenLight.DOFade((ClearanceSwitch || HeadlightSwitch) ? 1f : 0f, 0);
                imgHeadNear.DOFade(LowBeamLight ? 1f : 0f, 0);
                imgHeadFar.DOFade(HigBeamLight ? 1f : 0f, 0);

                imgFrontFog.DOFade(FrontFogLamp ? 1f : 0f, 0);
                imgRearFog.DOFade(RearFogLamp ? 1f : 0f, 0);
            }
        }
    }
    public override bool FrontFogSwitch
    {
        set
        {
            if (FrontFogSwitch != value)
            {
                base.FrontFogSwitch = value;
                controlRodNormal.imgFogLight.sprite = RearFogSwitch ? controlRodNormal.sprFoglight2 : value ? controlRodNormal.sprFogLight1 : controlRodNormal.sprFogLight0;
                controlRodBackward.imgFogLight.sprite = RearFogSwitch ? controlRodBackward.sprFoglight2 : value ? controlRodBackward.sprFogLight1 : controlRodBackward.sprFogLight0;

                imgFrontFog.DOFade(FrontFogLamp ? 1f : 0f, 0);
            }
        }
    }
    public override bool RearFogSwitch
    {
        set
        {
            if (RearFogSwitch != value)
            {
                base.RearFogSwitch = value;
                controlRodNormal.imgFogLight.sprite = value ? controlRodNormal.sprFoglight2 : FrontFogSwitch ? controlRodNormal.sprFogLight1 : controlRodNormal.sprFogLight0;
                controlRodBackward.imgFogLight.sprite = value ? controlRodBackward.sprFoglight2 : FrontFogSwitch ? controlRodBackward.sprFogLight1 : controlRodBackward.sprFogLight0;

                imgRearFog.DOFade(RearFogLamp ? 1f : 0f, 0);
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
                (btsCantrolState.button.targetGraphic as Image).sprite = value ? btsCantrolState.sprSelect : btsCantrolState.sprNormal;

                imgHeadNear.DOFade(LowBeamLight ? 1f : 0f, 0);
                imgHeadFar.DOFade(HigBeamLight ? 1f : 0f, 0);
            }
        }
    }
    public override bool ToggleHeadlightSwitch
    {
        set
        {
            if (ToggleHeadlightSwitch != value)
            {
                base.ToggleHeadlightSwitch = value;
            }
        }
    }
    #endregion

    public override void OnCreate()
    {
        base.OnCreate();

        btnControlLeft.onClick.AddListener(() =>
        {
            if (!LeftIndicatorSwitch)
            {
                LeftIndicatorSwitch = true;
                AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect jin"));
            }
            OnSwitchChange();
        });
        btnControlClose.onClick.AddListener(() =>
        {
            if (LeftIndicatorSwitch || RightIndicatorSwitch)
            {
                AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect jin"));
                LeftIndicatorSwitch = false;
                RightIndicatorSwitch = false;
            }
            OnSwitchChange();
        });
        btnControlRigth.onClick.AddListener(() =>
        {
            if (!RightIndicatorSwitch)
            {
                RightIndicatorSwitch = true;
                AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect jin"));
            }
            OnSwitchChange();
        });

        btsDoubleJump.button.onClick.AddListener(() =>
        {
            DoubleJumpSwitch = !DoubleJumpSwitch;
            if (DoubleJumpSwitch)
            {
                AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect WX01"));
            }
            else
            {
                AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect WX02"));
            }
            OnSwitchChange();
        });

        UIEventListener.Get(btsControlBackward1.button.gameObject).onDown += (go) =>
        {
            if (ClearanceSwitch || HeadlightSwitch)
            {
                ToggleHeadlightSwitch = true;
                FarHeadlightSwitch = !FarHeadlightSwitch;
            }
            controlRodBackward.objRoot.SetActive(true);
            controlRodNormal.objRoot.SetActive(false);
            (btsControlBackward1.button.targetGraphic as Image).sprite = btsControlBackward1.sprSelect;
            AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect yuan"));
            OnSwitchChange();
        };
        UIEventListener.Get(btsControlBackward1.button.gameObject).onUp += (go) =>
        {
            if (ClearanceSwitch || HeadlightSwitch)
            {
                ToggleHeadlightSwitch = false;
                FarHeadlightSwitch = !FarHeadlightSwitch;
            }
            controlRodBackward.objRoot.SetActive(false);
            controlRodNormal.objRoot.SetActive(true);
            (btsControlBackward1.button.targetGraphic as Image).sprite = btsControlBackward1.sprNormal;
            AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect jin"));
            OnSwitchChange();
        };

        UIEventListener.Get(btsControlBackward2.button.gameObject).onDown += (go) =>
        {
            if (ClearanceSwitch|| HeadlightSwitch)
            {
                FarHeadlightSwitch = !FarHeadlightSwitch;
            }
            controlRodBackward.objRoot.SetActive(true);
            controlRodNormal.objRoot.SetActive(false);
            (btsControlBackward2.button.targetGraphic as Image).sprite = btsControlBackward2.sprSelect;

            AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect yuan"));
            OnSwitchChange();
        };
        UIEventListener.Get(btsControlBackward2.button.gameObject).onUp += (go) =>
        {
            /*处理效果*/
            controlRodBackward.objRoot.SetActive(false);
            controlRodNormal.objRoot.SetActive(true);
            (btsControlBackward2.button.targetGraphic as Image).sprite = btsControlBackward2.sprNormal;
            AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect jin"));
            OnSwitchChange();
        };

        UIEventListener.Get(controlRodNormal.imgHeadLight.gameObject).onDragEnd += OnHeadLightDragEnd;
        UIEventListener.Get(controlRodBackward.imgHeadLight.gameObject).onDragEnd += OnHeadLightDragEnd;

        UIEventListener.Get(controlRodNormal.imgFogLight.gameObject).onDragEnd += OnFogLightDragEnd;
        UIEventListener.Get(controlRodBackward.imgFogLight.gameObject).onDragEnd += OnFogLightDragEnd;
    }


    private void OnHeadLightDragEnd(GameObject go, PointerEventData eventData)
    {
        Vector2 passPos = eventData.pressPosition;
        Vector2 currPos = eventData.position;
        if (Vector2.Distance(passPos, currPos) > 10f)//滑动小了不触发
        {
            if (currPos.x > passPos.x && currPos.y > passPos.y)//右上
            {
                if (!ClearanceSwitch && !HeadlightSwitch)
                {
                    ClearanceSwitch = true;
                    AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect01"));
                }
                else if (ClearanceSwitch)
                {
                    ClearanceSwitch = false;
                    HeadlightSwitch = true;
                    AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect01"));
                }
            }
            else if (currPos.x < passPos.x && currPos.y < passPos.y)
            {
                if (HeadlightSwitch)
                {
                    HeadlightSwitch = false;
                    ClearanceSwitch = true;
                    RearFogSwitch = false;
                    AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect01"));
                }
                else if (ClearanceSwitch)
                {
                    ClearanceSwitch = false;
                    FarHeadlightSwitch = false;
                    AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect01"));
                }
            }
        }
        OnSwitchChange();
    }

    private void OnFogLightDragEnd(GameObject go, PointerEventData eventData)
    {
        Vector2 passPos = eventData.pressPosition;
        Vector2 currPos = eventData.position;
        if (Vector2.Distance(passPos, currPos) > 10f)//滑动小了不触发
        {
            if (currPos.x > passPos.x && currPos.y > passPos.y)//右上
            {
                if (!FrontFogSwitch && !RearFogSwitch)
                {
                    FrontFogSwitch = true;
                    AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect01"));
                }
                else if (FrontFogSwitch && HeadlightSwitch)
                {
                    RearFogSwitch = true;
                    AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect01"));
                }
            }
            else if (currPos.x < passPos.x && currPos.y < passPos.y)
            {
                if (RearFogSwitch)
                {
                    RearFogSwitch = false;
                    AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect01"));
                }
                else if (FrontFogSwitch && !RearFogSwitch)
                {
                    FrontFogSwitch = false;
                    AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect01"));
                }
            }
        }
        OnSwitchChange();
    }
}

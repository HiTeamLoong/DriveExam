using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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


    public ControlRod controlRodNormal;
    public ControlRod controlRodBackward1;
    public ControlRod controlRodBackward2;

    #region SwitchModule
    public override bool ClearanceSwitch
    {
        set
        {
            if (ClearanceSwitch != value)
            {
                base.ClearanceSwitch = value;

                controlRodNormal.imgHeadLight.sprite = value ? controlRodNormal.sprHeadLight1 : HeadlightSwitch ? controlRodNormal.sprHeadLight2 : controlRodNormal.sprHeadLight0;
                controlRodBackward1.imgHeadLight.sprite = value ? controlRodBackward1.sprHeadLight1 : HeadlightSwitch ? controlRodBackward1.sprHeadLight2 : controlRodBackward1.sprHeadLight0;
                controlRodBackward2.imgHeadLight.sprite = value ? controlRodBackward2.sprHeadLight1 : HeadlightSwitch ? controlRodBackward2.sprHeadLight2 : controlRodBackward2.sprHeadLight0;

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

                controlRodNormal.imgHeadLight.sprite = value ? controlRodNormal.sprHeadLight2 : ClearanceLamp ? controlRodNormal.sprHeadLight1 : controlRodNormal.sprHeadLight0;
                controlRodBackward1.imgHeadLight.sprite = value ? controlRodBackward1.sprHeadLight2 : ClearanceLamp ? controlRodBackward1.sprHeadLight1 : controlRodBackward1.sprHeadLight0;
                controlRodBackward2.imgHeadLight.sprite = value ? controlRodBackward2.sprHeadLight2 : ClearanceLamp ? controlRodBackward2.sprHeadLight1 : controlRodBackward2.sprHeadLight0;

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
        btnControlRight.onClick.AddListener(() =>
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
            ToggleHeadlightSwitch = true;
            FarHeadlightSwitch = !FarHeadlightSwitch;
            controlRodNormal.objRoot.SetActive(false);
            controlRodBackward1.objRoot.SetActive(true);
            (btsControlBackward1.button.targetGraphic as Image).sprite = btsControlBackward1.sprSelect;
            AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect yuan"));
            OnSwitchChange();
        };
        UIEventListener.Get(btsControlBackward1.button.gameObject).onUp += (go) =>
        {
            ToggleHeadlightSwitch = false;
            FarHeadlightSwitch = !FarHeadlightSwitch;
            controlRodNormal.objRoot.SetActive(true);
            controlRodBackward1.objRoot.SetActive(false);
            (btsControlBackward1.button.targetGraphic as Image).sprite = btsControlBackward1.sprNormal;
            AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect jin"));
            OnSwitchChange();
        };

        UIEventListener.Get(btsControlBackward2.button.gameObject).onDown += (go) =>
        {
            FarHeadlightSwitch = !FarHeadlightSwitch;
            controlRodNormal.objRoot.SetActive(false);
            controlRodBackward2.objRoot.SetActive(true);
            (btsControlBackward2.button.targetGraphic as Image).sprite = btsControlBackward2.sprSelect;
            AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect yuan"));
            OnSwitchChange();
        };
        UIEventListener.Get(btsControlBackward2.button.gameObject).onUp += (go) =>
        {
            /*处理效果*/
            controlRodNormal.objRoot.SetActive(true);
            controlRodBackward2.objRoot.SetActive(false);
            (btsControlBackward2.button.targetGraphic as Image).sprite = btsControlBackward2.sprNormal;
            AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect jin"));
            OnSwitchChange();
        };


        UIEventListener.Get(controlRodNormal.imgHeadLight.gameObject).onDragEnd += OnHeadLightDragEnd;

        UIEventListener.Get(controlRodNormal.imgFogLight.gameObject).onDragBegin += OnFogLightDragBegin;
        UIEventListener.Get(controlRodNormal.imgFogLight.gameObject).onDrag += OnFogLightDrag;
        UIEventListener.Get(controlRodNormal.imgFogLight.gameObject).onDragEnd += OnFogLightDragEnd;
    }
    private void OnHeadLightDragEnd(GameObject go, PointerEventData eventData)
    {
        Vector2 passPos = eventData.pressPosition;
        Vector2 currPos = eventData.position;

        if (Vector2.Distance(passPos, currPos) > 10f &&
           (Mathf.Abs(passPos.x - currPos.x) < Mathf.Abs(passPos.y - currPos.y)))//滑动小了不触发
        {
            if (currPos.y > passPos.y)//右上
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
            else if (currPos.y < passPos.y)
            {
                if (HeadlightSwitch)
                {
                    HeadlightSwitch = false;
                    ClearanceSwitch = true;
                    AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect01"));
                }
                else if (ClearanceSwitch)
                {
                    ClearanceSwitch = false;
                    AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect01"));
                }
            }
        }
        OnSwitchChange();
    }


    private enum FogSwitchState
    {
        Open,
        Normal,
        Close,
    }
    private FogSwitchState fogSwitchState;
    private void OnFogLightDragBegin(GameObject go, PointerEventData eventData)
    {
        fogSwitchState = FogSwitchState.Normal;
    }
    private void OnFogLightDrag(GameObject go, PointerEventData eventData)
    {
        Vector2 passPos = eventData.pressPosition;
        Vector2 currPos = eventData.position;

        if (Vector2.Distance(passPos, currPos) > 10f &&
           (Mathf.Abs(passPos.x - currPos.x) < Mathf.Abs(passPos.y - currPos.y)))//滑动小了不触发
        {
            if (currPos.y > passPos.y && fogSwitchState != FogSwitchState.Open)//右上
            {
                fogSwitchState = FogSwitchState.Open;
                controlRodNormal.imgFogLight.sprite = controlRodNormal.sprFogLightOpen;
                //controlRodBackward1.imgFogLight.sprite = controlRodBackward1.sprFogLightOpen;
                //controlRodBackward2.imgFogLight.sprite = controlRodBackward1.sprFogLightOpen;
                if (!FrontFogSwitch)
                {
                    FrontFogSwitch = true;
                    AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect01"));
                }
                else if (!RearFogSwitch)
                {
                    RearFogSwitch = true;
                    AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect01"));
                }
            }
            else if (currPos.y < passPos.y && fogSwitchState != FogSwitchState.Close)
            {
                fogSwitchState = FogSwitchState.Close;
                controlRodNormal.imgFogLight.sprite = controlRodNormal.sprFogLightClose;
                if (RearFogLamp)
                {
                    RearFogSwitch = false;
                    AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect01"));
                }
                else if (FrontFogSwitch)
                {
                    FrontFogSwitch = false;
                    AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect01"));
                }
            }
        }
        else if (fogSwitchState != FogSwitchState.Normal)
        {
            fogSwitchState = FogSwitchState.Normal;
            controlRodNormal.imgFogLight.sprite = controlRodNormal.sprFogLoghtNormal;
        }
        //OnSwitchChange();
    }
    private void OnFogLightDragEnd(GameObject go, PointerEventData eventData)
    {
        fogSwitchState = FogSwitchState.Normal;
        controlRodNormal.imgFogLight.sprite = controlRodNormal.sprFogLoghtNormal;
        OnSwitchChange();
    }
}

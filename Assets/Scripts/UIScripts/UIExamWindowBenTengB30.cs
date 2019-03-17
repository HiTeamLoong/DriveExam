using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIExamWindowBenTengB30 : UIExamWindowBase
{
    [Space(20)]
    public Image imgOpenLight;      //示廓灯标识
    public Image imgHeadFar;        //远光灯标志
    public Image imgFrontFog;       //雾灯标识

    public Button btnControlRight;
    public Button btnControlClose;
    public Button btnControlLeft;

    public ButtonState btsControlForward;
    public ButtonState btsControlNormal;
    public ButtonState btsControlBackward;

    public ButtonState btsDoubleJump;

    public Transform transControlRod;

    
    [System.Serializable]
    public class ControlRod
    {
        public GameObject objRoot;

        public Image imgHeadLight;
        //[HideInInspector]
        public Sprite sprHeadLight0;
        public Sprite sprHeadLight1;
        public Sprite sprHeadLight2;

        public Image imgFogLight;
        //[HideInInspector]
        public Sprite sprFogLightNormal;
        public Sprite sprFogLightOpen;
    }

    public ControlRod controlRodForward;
    public ControlRod controlRodNormal;
    public ControlRod controlRodBackward;

    public override bool ClearanceSwitch
    {
        set
        {
            if (ClearanceSwitch != value)
            {
                base.ClearanceSwitch = value;
                controlRodForward.imgHeadLight.sprite = value ? controlRodForward.sprHeadLight1 : HeadlightSwitch ? controlRodForward.sprHeadLight2 : controlRodForward.sprHeadLight0;
                controlRodNormal.imgHeadLight.sprite = value ? controlRodNormal.sprHeadLight1 : HeadlightSwitch ? controlRodNormal.sprHeadLight2 : controlRodNormal.sprHeadLight0;
                controlRodBackward.imgHeadLight.sprite = value ? controlRodBackward.sprHeadLight1 : HeadlightSwitch ? controlRodBackward.sprHeadLight2 : controlRodBackward.sprHeadLight0;

                imgOpenLight.DOFade((ClearanceSwitch || HeadlightSwitch) ? 1f : 0f, 0);

                imgFrontFog.DOFade(FrontFogLamp ? 1f : 0f, 0);
                //imgRearFog.DOFade(RearFogLamp ? 1f : 0f, 0);
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
                controlRodForward.imgHeadLight.sprite = value ? controlRodForward.sprHeadLight2 : ClearanceLamp ? controlRodForward.sprHeadLight1 : controlRodForward.sprHeadLight0;
                controlRodNormal.imgHeadLight.sprite = value ? controlRodNormal.sprHeadLight2 : ClearanceSwitch ? controlRodNormal.sprHeadLight1 : controlRodNormal.sprHeadLight0;
                controlRodBackward.imgHeadLight.sprite = value ? controlRodBackward.sprHeadLight2 : ClearanceSwitch ? controlRodBackward.sprHeadLight1 : controlRodBackward.sprHeadLight0;

                imgOpenLight.DOFade((ClearanceSwitch || HeadlightSwitch) ? 1f : 0f, 0);
                //imgHeadNear.DOFade(LowBeamLight ? 1f : 0f, 0);
                imgHeadFar.DOFade(HigBeamLight ? 1f : 0f, 0);

                imgFrontFog.DOFade(FrontFogLamp ? 1f : 0f, 0);
                //imgRearFog.DOFade(RearFogLamp ? 1f : 0f, 0);
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

                controlRodForward.imgFogLight.sprite = value ? controlRodForward.sprFogLightOpen :  controlRodForward.sprFogLightNormal;
                controlRodNormal.imgFogLight.sprite = value ? controlRodNormal.sprFogLightOpen :  controlRodNormal.sprFogLightNormal;
                controlRodBackward.imgFogLight.sprite = value ? controlRodBackward.sprFogLightOpen : controlRodBackward.sprFogLightNormal;
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

                bool isSetFar = (value && !ToggleHeadlightSwitch);
                //更新按键
                (btsControlForward.button.targetGraphic as Image).sprite = (value && !ToggleHeadlightSwitch) ? btsControlForward.sprSelect : btsControlForward.sprNormal;
                (btsControlNormal.button.targetGraphic as Image).sprite = (!value && !ToggleHeadlightSwitch) ? btsControlNormal.sprSelect : btsControlNormal.sprNormal;
                //更新摇杆
                controlRodForward.objRoot.SetActive(value && !ToggleHeadlightSwitch);
                //更新仪表
                imgHeadFar.DOFade(HigBeamLight ? 1f : 0f, 0);//此车型没有近光标志

                //控制显示摇杆
                controlRodForward.objRoot.SetActive(value&&!ToggleHeadlightSwitch);
                controlRodNormal.objRoot.SetActive(!value&&!ToggleHeadlightSwitch);
                //controlRodBackward.objRoot.SetActive(!value);
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

                (btsControlForward.button.targetGraphic as Image).sprite = value ? btsControlForward.sprNormal : FarHeadlightSwitch ? btsControlForward.sprSelect : btsControlForward.sprNormal;
                (btsControlNormal.button.targetGraphic as Image).sprite = value ? btsControlNormal.sprNormal : FarHeadlightSwitch ? btsControlNormal.sprNormal : btsControlNormal.sprSelect;
                (btsControlBackward.button.targetGraphic as Image).sprite = value ? btsControlBackward.sprSelect : btsControlBackward.sprNormal;


                controlRodForward.objRoot.SetActive(!value);
                controlRodNormal.objRoot.SetActive(!value);
                controlRodBackward.objRoot.SetActive(value);
            }
        }
    }
    private bool isSwitching = false;
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
        });
        btnControlClose.onClick.AddListener(() =>
        {
            if (LeftIndicatorSwitch || RightIndicatorSwitch)
            {
                AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect jin"));
                LeftIndicatorSwitch = false;
                RightIndicatorSwitch = false;
            }
        });
        btnControlRight.onClick.AddListener(() =>
        {
            if (!RightIndicatorSwitch)
            {
                RightIndicatorSwitch = true;
                AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect jin"));
            }
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
        });

        btsControlForward.button.onClick.AddListener(() =>
        {
            if (!FarHeadlightSwitch && !ToggleHeadlightSwitch)
            {
                FarHeadlightSwitch = true;
                //controlRodForward.objRoot.SetActive(true);
                //controlRodNormal.objRoot.SetActive(false);
                //controlRodBackward.objRoot.SetActive(false);
                //TODO：UI标识
                AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect yuan"));
            }
        });
        btsControlNormal.button.onClick.AddListener(() =>
        {
            if (FarHeadlightSwitch && !ToggleHeadlightSwitch)
            {
                FarHeadlightSwitch = false;
                //controlRodForward.objRoot.SetActive(false);
                //controlRodNormal.objRoot.SetActive(true);
                //controlRodBackward.objRoot.SetActive(false);
                //TODO：UI标识
                AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect jin"));
            }
        });
        UIEventListener.Get(btsControlBackward.button.gameObject).onDown += (go) =>
        {
            ToggleHeadlightSwitch = true;
            FarHeadlightSwitch = true;
            //controlRodForward.objRoot.SetActive(false);
            //controlRodNormal.objRoot.SetActive(false);
            //controlRodBackward.objRoot.SetActive(true);
            //TODO：UI标识
            AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect yuan"));
        };
        UIEventListener.Get(btsControlBackward.button.gameObject).onUp += (go) =>
        {
            ToggleHeadlightSwitch = false;
            FarHeadlightSwitch = false;
            //controlRodForward.objRoot.SetActive(false);
            //controlRodNormal.objRoot.SetActive(true);
            //controlRodBackward.objRoot.SetActive(false);
            //TODO：UI标识
            AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect jin"));
        };

        UIEventListener.Get(controlRodForward.imgHeadLight.gameObject).onDragEnd += OnHeadLightDragEnd;
        UIEventListener.Get(controlRodNormal.imgHeadLight.gameObject).onDragEnd += OnHeadLightDragEnd;
        UIEventListener.Get(controlRodBackward.imgHeadLight.gameObject).onDragEnd += OnHeadLightDragEnd;

        UIEventListener.Get(controlRodForward.imgFogLight.gameObject).onDragEnd += OnFogLightDragEnd;
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
                //示廓灯是否开启/前照灯是否开启
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
            else if (/*currPos.x < passPos.x &&*/ currPos.y < passPos.y)//左下
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
                    FrontFogSwitch = false;
                    AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect01"));
                }
            }
        }
    }

    private void OnFogLightDragEnd(GameObject go, PointerEventData eventData)
    {
        Vector2 passPos = eventData.pressPosition;
        Vector2 currPos = eventData.position;
        if (Vector2.Distance(passPos, currPos) > 10f)//滑动小了不触发
        {
            if (/*currPos.x > passPos.x &&*/ currPos.y > passPos.y)//右上
            {
                if (ClearanceSwitch || HeadlightSwitch)
                {
                    if (!FrontFogSwitch)
                    {
                        FrontFogSwitch = true;
                        AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect01"));
                    }
                }
            }
            else if (/*currPos.x < passPos.x &&*/ currPos.y < passPos.y)
            {
                if (FrontFogSwitch)
                {
                    FrontFogSwitch = false;
                    AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect01"));
                }
            }
        }
    }
}

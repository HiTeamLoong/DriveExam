using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIExamWindowBenTeng : UIExamWindowBase
{
    [Space(20)]
    public Image imgOpenLight;
    public Image imgHeadFar;
    public Image imgFrontFog;

    public Button btnControlRight;
    public Button btnControlClose;
    public Button btnControlLeft;

    public ButtonState btsControlForward;
    public ButtonState btsControlNormal;
    public ButtonState btsControlBackward;

    public ButtonState btsDoubleJump;

    public Transform transControlRod;

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
    }

    public ControlRod controlRodForward;
    public ControlRod controlRodNormal;
    public ControlRod controlRodBackward;

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
                controlRodForward.objRoot.SetActive(true);
                controlRodNormal.objRoot.SetActive(false);
                controlRodBackward.objRoot.SetActive(false);
                //TODO：UI标识
                AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect yuan"));
            }
        });
        btsControlNormal.button.onClick.AddListener(() =>
        {
            if (FarHeadlightSwitch && !ToggleHeadlightSwitch)
            {
                FarHeadlightSwitch = false;
                controlRodForward.objRoot.SetActive(false);
                controlRodNormal.objRoot.SetActive(true);
                controlRodBackward.objRoot.SetActive(false);
                //TODO：UI标识
                AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect jin"));
            }
        });
        UIEventListener.Get(btsControlBackward.button.gameObject).onDown += (go) =>
        {
            ToggleHeadlightSwitch = true;
            FarHeadlightSwitch = true;
            controlRodForward.objRoot.SetActive(false);
            controlRodNormal.objRoot.SetActive(false);
            controlRodBackward.objRoot.SetActive(true);
            //TODO：UI标识
            AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect yuan"));
        };
        UIEventListener.Get(btsControlBackward.button.gameObject).onUp += (go) =>
        {
            ToggleHeadlightSwitch = false;
            FarHeadlightSwitch = false;
            controlRodForward.objRoot.SetActive(false);
            controlRodNormal.objRoot.SetActive(true);
            controlRodBackward.objRoot.SetActive(false);
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
                    AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect01"));
                }
                else if (ClearanceSwitch)
                {
                    ClearanceSwitch = false;
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
            if (currPos.x > passPos.x && currPos.y > passPos.y)//右上
            {
                if (!FrontFogSwitch && !RearFogSwitch)
                {
                    FrontFogSwitch = true;
                    AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect01"));
                }
                else if (FrontFogSwitch)
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
    }
}

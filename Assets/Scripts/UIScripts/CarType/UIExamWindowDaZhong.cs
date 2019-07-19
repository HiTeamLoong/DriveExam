﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIExamWindowDaZhong : UIExamWindowBase
{

    [Space(20)]
    public Image imgHeadFar;
    public Image imgFrontFog;

    public KnobSwitch knobSwitch;       //灯光控制旋钮 [示廓灯、近光灯、雾灯]

    public Button btnControlRigth;      //右转向
    public Button btnControlClose;      //关闭转向
    public Button btnControlLeft;       //左转向

    public ButtonState btsCantrolForward;   //远光灯
    public ButtonState btsControlNormal;    //近光灯
    public ButtonState btsControlBackward;  //远近切换

    public ButtonState btsDoubleJump;       //双闪灯

    public Image imgControlRod;         //灯光控制杆

    public Sprite sprControlForward;    //往前--变小
    public Sprite sprControlNormal;     //默认
    public Sprite sprControlBackward;   //往后--变大

    public override bool ClearanceSwitch
    {
        set
        {
            if (ClearanceSwitch != value)
            {
                base.ClearanceSwitch = value;
                if (value)
                {
                    knobSwitch.SetLevel(1);
                }
                else if (!HeadlightSwitch)
                {
                    knobSwitch.SetLevel(0);
                }
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
                if (value)
                {
                    knobSwitch.SetLevel(2);
                }
                else if (!ClearanceSwitch)
                {
                    knobSwitch.SetLevel(0);
                }
                imgHeadFar.DOFade(HigBeamLight ? 1f : 0f, 0);
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
                if (!FrontFogSwitch && !RearFogSwitch)
                {
                    knobSwitch.IsTriggerOn = false;
                }

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
                if (!FrontFogSwitch && !RearFogSwitch)
                {
                    knobSwitch.IsTriggerOn = false;
                }
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
                    imgControlRod.transform.localEulerAngles = new Vector3(0, 0, 10);
                }
                else if (!RightIndicatorSwitch)
                {
                    imgControlRod.transform.localEulerAngles = new Vector3(0, 0, 0);
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
                    imgControlRod.transform.localEulerAngles = new Vector3(0, 0, -10);
                }
                else if (!LeftIndicatorSwitch)
                {
                    imgControlRod.transform.localEulerAngles = new Vector3(0, 0, 0);
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
                (btsCantrolForward.button.targetGraphic as Image).sprite = (value && !ToggleHeadlightSwitch) ? btsCantrolForward.sprSelect : btsCantrolForward.sprNormal;
                (btsControlNormal.button.targetGraphic as Image).sprite = (!value && !ToggleHeadlightSwitch) ? btsControlNormal.sprSelect : btsControlNormal.sprNormal;
                imgControlRod.sprite = value ? ((!ToggleHeadlightSwitch) ? sprControlForward : sprControlBackward) : sprControlNormal;

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
                (btsCantrolForward.button.targetGraphic as Image).sprite = value ? btsCantrolForward.sprNormal : FarHeadlightSwitch ? btsCantrolForward.sprSelect : btsCantrolForward.sprNormal;
                (btsControlNormal.button.targetGraphic as Image).sprite = value ? btsControlNormal.sprNormal : FarHeadlightSwitch ? btsControlNormal.sprNormal : btsControlNormal.sprSelect;
                (btsControlBackward.button.targetGraphic as Image).sprite = value ? btsControlBackward.sprSelect : btsControlBackward.sprNormal;
                imgControlRod.sprite = value ? sprControlBackward : FarHeadlightSwitch ? sprControlForward : sprControlNormal;
            }
        }
    }

    public override void OnCreate()
    {
        base.OnCreate();

        knobSwitch.onChangeLevel = OnChangeKnobLevel;
        knobSwitch.onChangeSwitch = OnChangeKnobSwitch;
        btnControlLeft.onClick.AddListener(() =>
        {
            if (!LeftIndicatorSwitch)
            {
                LeftIndicatorSwitch = true;
                AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect jin"));
                OnSwitchChange();
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
        btsCantrolForward.button.onClick.AddListener(() => {
            if (!FarHeadlightSwitch)
            {
                FarHeadlightSwitch = true;
                AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect yuan"));
            }
            OnSwitchChange();
        });
        btsControlNormal.button.onClick.AddListener(() => {
            if (FarHeadlightSwitch)
            {
                FarHeadlightSwitch = false;
                AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect jin"));
            }
            OnSwitchChange();
        });
        UIEventListener.Get(btsControlBackward.button.gameObject).onDown += (go) =>
        {
            ToggleHeadlightSwitch = true; FarHeadlightSwitch = true;
            AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect yuan"));
            OnSwitchChange();
        };
        UIEventListener.Get(btsControlBackward.button.gameObject).onUp += (go) =>
        {
            ToggleHeadlightSwitch = false; FarHeadlightSwitch = false;
            AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("L Effect jin"));
            OnSwitchChange();
        };
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

    }
    void OnChangeKnobLevel(int value)
    {
        switch (value)
        {
            case 0:
                ClearanceSwitch = false;
                HeadlightSwitch = false;
                break;
            case 1:
                ClearanceSwitch = true;
                break;
            case 2:
                HeadlightSwitch = true;
                break;
        }
        OnSwitchChange();
    }
    void OnChangeKnobSwitch(bool value)
    {
        FrontFogSwitch = value;
        RearFogSwitch = value;
        OnSwitchChange();
    }
}
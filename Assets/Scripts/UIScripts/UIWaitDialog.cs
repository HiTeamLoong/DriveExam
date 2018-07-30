using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIWaitDialog : UIDialog
{
    public RectTransform transWait;


    public void Start()
    {
        float interval = 0.1f;
        Sequence loopSequence = DOTween.Sequence();
        loopSequence.AppendInterval(interval);
        loopSequence.Append(transWait.DOLocalRotate(new Vector3(0, 0, 0), 0));
        loopSequence.AppendInterval(interval);
        loopSequence.Append(transWait.DOLocalRotate(new Vector3(0, 0, -30), 0));
        loopSequence.AppendInterval(interval);
        loopSequence.Append(transWait.DOLocalRotate(new Vector3(0, 0, -60), 0));
        loopSequence.AppendInterval(interval);
        loopSequence.Append(transWait.DOLocalRotate(new Vector3(0, 0, -90), 0));
        loopSequence.AppendInterval(interval);
        loopSequence.Append(transWait.DOLocalRotate(new Vector3(0, 0, -120), 0));
        loopSequence.AppendInterval(interval);
        loopSequence.Append(transWait.DOLocalRotate(new Vector3(0, 0, -150), 0));
        loopSequence.AppendInterval(interval);
        loopSequence.Append(transWait.DOLocalRotate(new Vector3(0, 0, -180), 0));
        loopSequence.AppendInterval(interval);
        loopSequence.Append(transWait.DOLocalRotate(new Vector3(0, 0, -210), 0));
        loopSequence.AppendInterval(interval);
        loopSequence.Append(transWait.DOLocalRotate(new Vector3(0, 0, -240), 0));
        loopSequence.AppendInterval(interval);
        loopSequence.Append(transWait.DOLocalRotate(new Vector3(0, 0, -270), 0));
        loopSequence.AppendInterval(interval);
        loopSequence.Append(transWait.DOLocalRotate(new Vector3(0, 0, -300), 0));
        loopSequence.AppendInterval(interval);
        loopSequence.Append(transWait.DOLocalRotate(new Vector3(0, 0, -330), 0));
        loopSequence.SetLoops(-1);
    }

}

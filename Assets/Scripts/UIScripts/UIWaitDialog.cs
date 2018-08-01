using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIWaitDialog : UIDialog
{
    public RectTransform transWait;

    private Sequence loopSequence;
    public void Start()
    {
        StartCoroutine(_ImageTurn());
    }
    public override void OnDispose()
    {
        StopCoroutine(_ImageTurn());
        base.OnDispose();
    }

    IEnumerator _ImageTurn(){
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            Vector3 angle = transWait.localEulerAngles;
            angle.z -= 30;
            transWait.localEulerAngles = angle;
        }
    }
}

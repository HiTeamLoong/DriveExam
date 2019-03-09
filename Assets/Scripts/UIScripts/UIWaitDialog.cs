using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIWaitDialog : UIDialog
{
    public GameObject rootWait;
    public RectTransform transWait;

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
        rootWait.SetActive(false);
        yield return new WaitForSeconds(.2f);
        rootWait.SetActive(true);

        while (true)
        {
            yield return new WaitForSeconds(.1f);
            Vector3 angle = transWait.localEulerAngles;
            angle.z -= 30;
            transWait.localEulerAngles = angle;
        }
    }
}

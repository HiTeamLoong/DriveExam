using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
    public Image imgProgress;
    public Text textProg;

    public float Value
    {
        get { return imgProgress.fillAmount; }
        set
        {
            float prog = Mathf.Clamp01(value);
            imgProgress.fillAmount = prog;
            if (textProg != null)
            {
                textProg.text = string.Format("加载中 {0}", prog.ToString("P"));
            }
        }
    }
}

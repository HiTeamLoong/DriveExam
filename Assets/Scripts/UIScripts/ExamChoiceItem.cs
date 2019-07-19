using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExamChoiceItem : MonoBehaviour
{
    public Text textTitle;
    public Button button;

    private int index;
    private Callback<int> callback;

    void Start()
    {
        button.onClick.AddListener(OnClickItem);
    }

    public void InitWith(int index,Callback<int> callback)
    {
        this.index = index;
        this.callback = callback;
        textTitle.text = string.Format("考试练习-{0}", index + 1);
    }

    void OnClickItem()
    {
        if (callback!=null)
        {
            callback(index);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChoiceDialog : UIDialog
{
    public Button btnClose;

    public Transform itemList;
    public ExamChoiceItem examChoiceItem;

    private Callback<int> ChoiceBack;
    private Callback CloseBack;

    public override void OnCreate()
    {
        base.OnCreate();
        btnClose.onClick.AddListener(OnClickClose);
    }

    public void InitWith( Callback<int> choiceBack,Callback closeBack)
    {
        this.ChoiceBack = choiceBack;
        this.CloseBack = closeBack;

        examChoiceItem.gameObject.SetActive(false);
        List<List<int>> examList = GameDataMgr.Instance.carInfo.GetExams();
        for (int i = 0; i < examList.Count; i++)
        {
            GameObject go = Instantiate(examChoiceItem.gameObject, itemList);
            go.SetActive(true);
            ExamChoiceItem choiceItem = go.GetComponent<ExamChoiceItem>();
            choiceItem.InitWith(i, OnClickItem);
        }
    }

    void OnClickItem(int index)
    {
        if (ChoiceBack!=null)
        {
            ChoiceBack(index);
        }
        UIManager.Instance.CloseUI(this);
    }

    void OnClickClose()
    {
        if (CloseBack!=null)
        {
            CloseBack();
        }
        UIManager.Instance.CloseUI(this);
    }
}

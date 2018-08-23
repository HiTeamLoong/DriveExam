using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPrompDialog : UIDialog
{
    public static void ShowPromp(PrompType type, string title, string content, Callback<bool> callback)
    {
        UIPrompDialog uIPrompDialog = UIManager.Instance.OpenUI<UIPrompDialog>();

    }

    public enum PrompType
    {
        Confirm,
        CancelAndConfirm
    }

    public Text textTitle;
    public Text textContent;

    public Button btnCancel;
    public Button btnConfirm;

    private Callback<bool> callback;

    private void Start()
    {
        btnCancel.onClick.AddListener(OnClickCancelBtn);
        btnConfirm.onClick.AddListener(OnClickConfirmBtn);
    }

    public void InitWith(PrompType type, string title, string content, Callback<bool> callback)
    {
        btnCancel.gameObject.SetActive(type == PrompType.CancelAndConfirm);
        textTitle.text = title;
        textContent.text = content;

        this.callback = callback;
    }

    void OnClickCancelBtn()
    {
        if (callback != null)
        {
            callback(false);
        }
        UIManager.Instance.CloseUI(this);
    }
    void OnClickConfirmBtn()
    {
        if (callback != null)
        {
            callback(true);
        }
        UIManager.Instance.CloseUI(this);
    }
}

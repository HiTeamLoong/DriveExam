using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPrompDialog : UIDialog
{
    public static void ShowPromp(PrompType type, string title, string content, Callback<bool> callback)
    {
        UIPrompDialog uIPrompDialog = UIManager.Instance.OpenUI<UIPrompDialog>();
        uIPrompDialog.InitWith(type, title, content, callback);
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
        UIManager.Instance.CloseUI(this);
        if (callback != null)
        {
            callback(false);
        }
    }
    void OnClickConfirmBtn()
    {
        UIManager.Instance.CloseUI(this);
        if (callback != null)
        {
            callback(true);
        }
    }
}

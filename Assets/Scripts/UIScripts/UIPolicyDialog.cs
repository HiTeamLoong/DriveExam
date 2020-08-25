using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPolicyDialog : UIDialog {


    public Button btnRefuse;
    public Button btnAgree;

    public LinkImageText textLink;


    public override void OnCreate()
    {
        base.OnCreate();
        btnRefuse.onClick.AddListener(OnClickRefuse);
        btnAgree.onClick.AddListener(OnClickAgree);
        textLink.onHrefClick.AddListener(onHerfClick);
    }


    void OnClickRefuse()
    {
        Application.Quit();
    }
    void OnClickAgree()
    {
        PlayerPrefs.SetInt("POLICY",1);
        UIManager.Instance.CloseUI(this);
    }

    void onHerfClick(string content)
    {
        print("点击内容" + content);
        UIManager.Instance.OpenUI<UIPolicyDetialDialog>();
    }
}

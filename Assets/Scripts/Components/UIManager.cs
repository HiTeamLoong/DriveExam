﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : XMonoSingleton<UIManager>
{
    [TextArea]
    public string testContent;


    public Transform wndParent;
    public Transform dlgParent;

    private Canvas canvas;
    private CanvasScaler canvasScaler;
    public Vector2 defaultResolution
    {
        get
        {
            if (canvasScaler == null)
            {
                canvasScaler = GetComponent<CanvasScaler>();
            }
            return canvasScaler.referenceResolution;
        }
    }
    public Vector2 realResolution
    {
        get
        {
            if (canvas == null)
            {
                canvas = GetComponent<Canvas>();
            }
            RectTransform canvasTrans = (canvas.transform as RectTransform);
            return canvasTrans.sizeDelta;
        }
    }

    public Stack<UIWindow> windowsStack = new Stack<UIWindow>();
    public Stack<UIDialog> dialogsStack = new Stack<UIDialog>();

    private void Awake()
    {
        Vector2 defaultRatio = UIManager.Instance.defaultResolution;
        Vector2 realRatio = UIManager.Instance.realResolution;
        canvasScaler.matchWidthOrHeight = (defaultRatio.y / defaultRatio.x) < (realRatio.y / realRatio.x) ? 0 : 1;
    }

    public override void OnInit()
    {
        base.OnInit();

    }

    public T OpenUI<T>() where T : UIPanelBase
    {
        System.Type type = typeof(T);

        GameObject prefab = ResourcesMgr.Instance.LoadUIPrefab(type.ToString());
        if (type.IsSubclassOf(typeof(UIWindow)))
        {
            GameObject go = Instantiate(prefab, Vector3.zero, Quaternion.identity, wndParent);
            UIWindow wnd = go.GetComponent<UIWindow>();
            wnd.OnCreate();
            //创建新的主界面时，删除所有界面
            if (wnd.isMain)
            {
                while (windowsStack.Count > 0)
                {
                    UIWindow temp = windowsStack.Pop();
                    temp.OnDispose();
                    Destroy(temp.gameObject);
                }
            }
            else if (windowsStack.Count > 0)
            {
                UIWindow temp = windowsStack.Peek();
                temp.OnHide();
                temp.gameObject.SetActive(false);
            }
            wnd.OnShow();
            windowsStack.Push(wnd);
            return wnd as T;
        }
        else if (type.IsSubclassOf(typeof(UIDialog)))
        {
            GameObject go = Instantiate(prefab, Vector3.zero, Quaternion.identity, dlgParent);
            UIDialog dlg = go.GetComponent<UIDialog>();
            dlg.OnCreate();
            dlg.OnShow();
            dialogsStack.Push(dlg);
            return dlg as T;
        }
        else
        {
            Debug.LogError("打開的UI類型有問題，請檢測類型");
            return null;
        }
    }

    public void CloseUI(UIPanelBase uiPanel)
    {
        if (uiPanel.GetType().IsSubclassOf(typeof(UIWindow)))
        {
            UIWindow tempWnd = windowsStack.Peek();
            Debug.Log(tempWnd.name);
            if (!(uiPanel as UIWindow).isMain && tempWnd == uiPanel)
            {
                tempWnd = windowsStack.Pop();
                tempWnd.OnDispose();
                Destroy(tempWnd.gameObject);

                tempWnd = windowsStack.Peek();
                tempWnd.gameObject.SetActive(true);
                tempWnd.OnShow();
            }
            else
            {
                Debug.LogError("要关闭的界面不是栈顶元素或是栈底元素");
            }
        }
        else if (uiPanel.GetType().IsSubclassOf(typeof(UIDialog)))
        {
            UIDialog tempDlg = dialogsStack.Peek();

            Debug.Log(tempDlg.gameObject.name + "  " + uiPanel.gameObject.name);

            if (tempDlg == uiPanel)
            {
                tempDlg = dialogsStack.Pop();
                tempDlg.OnDispose();
                Destroy(tempDlg.gameObject);

                if (dialogsStack.Count > 0)
                {
                    tempDlg = dialogsStack.Peek();
                    tempDlg.OnShow();
                }
            }
            else
            {
                Debug.LogError("要关闭的界面不是栈顶元素");
            }
        }
        else
        {
            Debug.LogError("关闭的UI類型有問題，請檢測類型");
        }
    }

    public void CloseAllWindow()
    {

    }

}

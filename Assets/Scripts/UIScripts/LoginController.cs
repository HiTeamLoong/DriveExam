using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    public Stack<GameObject> stackLayer = new Stack<GameObject>();

    public Button btnReturn;

    [System.Serializable]
    public class AccountLogin
    {
        public GameObject root;
        public InputField inputAccount;
        public InputField inputPwd;
        public Button btnLogin;
        public Button btnWechat;
        public Button btnMobile;
    }
    [System.Serializable]
    public class MobileLogin
    {
        public GameObject root;
        public InputField inputMobile;
        public InputField inputPwd;
        public Button btnLogin;
        public Button btnSignup;
        public Button btnForget;
        public Button btnWechat;
    }
    [System.Serializable]
    public class FreeSignup
    {
        public GameObject root;
        public InputField inputMobile;
        public InputField inputAnswer;
        public Image imgQuestion;
        public InputField inputIdentifying;
        public Text textIdentifying;
        public InputField inputPwd1;
        public InputField inputPwd2;
        public Button btnConfirm;
    }
    [System.Serializable]
    public class ForgetPassword1
    {
        public GameObject root;
        public InputField inputMobile;
        public InputField inputAnswer;
        public Image imgQuestion;
        public InputField inputIdentifying;
        public Text textIdentifying;

        public Button btnContinue;
    }
    [System.Serializable]
    public class ForgetPassword2
    {
        public InputField inputPwd1;
        public InputField inputPwd2;
        public Button btnConfirm;
    }

    public AccountLogin accountLogin;
    public MobileLogin mobileLogin;
    public FreeSignup freeSignup;
    public ForgetPassword1 forgetPassword1;
    public ForgetPassword2 forgetPassword2;

    private void Start()
    {
        btnReturn.onClick.AddListener(OnClickReturn);
    }
    private void OnClickReturn()
    {

    }
    void Init()
    {

    }

}

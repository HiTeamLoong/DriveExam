using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    public Stack<GameObject> stackLayer = new Stack<GameObject>();

    public Button btnReturn;
    public GameObject screenMask;

    [System.Serializable]
    public class AccountLogin
    {
        public GameObject layer;
        public InputField inputAccount;
        public InputField inputPwd;
        public Button btnLogin;
        public Button btnWechat;
        public Button btnMobile;
    }
    [System.Serializable]
    public class MobileLogin
    {
        public GameObject layer;
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
        public GameObject layer;
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
        public GameObject layer;
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
        public GameObject layer;
        public InputField inputPwd1;
        public InputField inputPwd2;
        public Button btnConfirm;
    }

    public AccountLogin loginLayer1;
    public MobileLogin loginLayer2;
    public FreeSignup signupLayer;
    public ForgetPassword1 forgetLayer1;
    public ForgetPassword2 forgetLayer2;

    private Callback<ResponseLogin> loginCallback;
    private void Start()
    {
        btnReturn.onClick.AddListener(OnClickReturn);

        loginLayer1.btnLogin.onClick.AddListener(LoginLayer1LoginBtn);
        loginLayer1.btnWechat.onClick.AddListener(LoginLayer1WechatBtn);
        loginLayer1.btnMobile.onClick.AddListener(LoginLayer1MobileBtn);

        loginLayer2.btnLogin.onClick.AddListener(LoginLayer2LoginBtn);
        loginLayer2.btnSignup.onClick.AddListener(LoginLayer2SignupBtn);
        loginLayer2.btnForget.onClick.AddListener(LoginLayer2ForgetBtn);
        loginLayer2.btnWechat.onClick.AddListener(LoginLayer2WechatBtn);

        UIEventListener.Get(signupLayer.textIdentifying.gameObject).onClick += SignupLayerIdentifyText;
        signupLayer.btnConfirm.onClick.AddListener(SignupLayerConfirmBtn);

        UIEventListener.Get(forgetLayer1.textIdentifying.gameObject).onClick += ForgetLayer1IdentifyText;
        forgetLayer1.btnContinue.onClick.AddListener(ForgetLayer1ContinueBtn);

        forgetLayer2.btnConfirm.onClick.AddListener(ForgetLayer2ConfirmBtn);
    }
    public void InitWith(Callback<ResponseLogin> loginCallback)
    {
        this.loginCallback = loginCallback;
        stackLayer = new Stack<GameObject>();
        OpenLayer(loginLayer1.layer);
    }

    void OpenLayer(GameObject layer)
    {
        if (!stackLayer.Contains(layer))
        {
            stackLayer.Push(layer);
            btnReturn.gameObject.SetActive(stackLayer.Count > 1);
            if (stackLayer.Count == 1)
            {
                layer.SetActive(true);
            }
            else
            {
                EnterAnim(layer);
            }
        }
    }
    void CloseLayer()
    {
        if (stackLayer.Count>0)
        {
            GameObject layer = stackLayer.Pop();
            btnReturn.gameObject.SetActive(stackLayer.Count > 1);
            if (stackLayer.Count == 0)
            {
                layer.SetActive(false);
            }
            else
            {
                ExitAnim(layer);
                btnReturn.gameObject.SetActive(stackLayer.Count > 1);
            }
        }
    }

    void EnterAnim(GameObject layer)
    {
        RectTransform layerTrans = layer.transform as RectTransform;
        layerTrans.anchoredPosition = new Vector2(layerTrans.rect.width, layerTrans.anchoredPosition.y);
        
        Sequence enterSequence = DOTween.Sequence();
        enterSequence.AppendCallback(() => { layer.SetActive(true); });
        enterSequence.AppendCallback(() => { screenMask.SetActive(true); });
        enterSequence.Append(layerTrans.DOAnchorPosX(0, 0.5f).SetEase(Ease.OutQuint));
        enterSequence.AppendCallback(() => { screenMask.SetActive(false); });
    }
    void ExitAnim(GameObject layer)
    {
        RectTransform layerTrans = layer.transform as RectTransform;
        Vector2 targetPos = new Vector2(layerTrans.rect.width, layerTrans.anchoredPosition.y);

        Sequence exitSequence = DOTween.Sequence();
        exitSequence.AppendCallback(() => { screenMask.SetActive(true); });
        exitSequence.Append(layerTrans.DOAnchorPosX(layerTrans.rect.width, 0.5f).SetEase(Ease.OutQuint));
        exitSequence.AppendCallback(() => { screenMask.SetActive(false); });
        exitSequence.AppendCallback(() => { layer.SetActive(false); });
    }


    /// <summary>
    /// 点击返回
    /// </summary>
    private void OnClickReturn()
    {
        CloseLayer();
    }

    void LoginLayer1LoginBtn()
    {
        if (string.IsNullOrEmpty(loginLayer1.inputAccount.text))
        {
            UITipsDialog.ShowTips("请输入理论保过卡卡号");
            return;
        }
        if (string.IsNullOrEmpty(loginLayer1.inputPwd.text))
        {
            UITipsDialog.ShowTips("请输入理论保过卡密码");
            return;
        }
        RequestLogin requestLogin = new RequestLogin();
        requestLogin.phone = loginLayer1.inputAccount.text;
        requestLogin.password = loginLayer1.inputPwd.text;
        requestLogin.equitment = SystemInfo.deviceUniqueIdentifier;


        LoginManager.Instance.SendLoginMessage<ResponseLogin>(requestLogin, (responseData) =>
        {
            if (responseData.status=="200")
            {
                Debug.Log("登录成功"+responseData.msg);
                UITipsDialog.ShowTips("登录成功");
                loginCallback(responseData.data);
            }
            else
            {
                UITipsDialog.ShowTips(responseData.msg);
            }
        });
    }
    void LoginLayer1WechatBtn()
    {

    }
    void LoginLayer1MobileBtn()
    {
        OpenLayer(loginLayer2.layer);
    }

    void LoginLayer2LoginBtn()
    {

    }
    void LoginLayer2SignupBtn()
    {
        OpenLayer(signupLayer.layer);
    }
    void LoginLayer2ForgetBtn()
    {
        OpenLayer(forgetLayer1.layer);
    }
    void LoginLayer2WechatBtn()
    {

    }

    void SignupLayerIdentifyText(GameObject go)
    {

    }
    void SignupLayerConfirmBtn()
    {

    }
    void ForgetLayer1IdentifyText(GameObject go)
    {

    }
    void ForgetLayer1ContinueBtn()
    {
        OpenLayer(forgetLayer2.layer);
    }

    void ForgetLayer2ConfirmBtn()
    {

    }
}

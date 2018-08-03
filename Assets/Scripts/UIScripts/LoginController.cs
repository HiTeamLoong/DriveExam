using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    public Stack<LayerBase> stackLayer = new Stack<LayerBase>();

    public Button btnReturn;
    public GameObject screenMask;

    public abstract class LayerBase
    {
        public GameObject root;
        public abstract void Show();
        public abstract void Hide();
    }
    [System.Serializable]
    public class AccountLogin : LayerBase
    {
        public InputField inputAccount;
        public InputField inputPwd;
        public Button btnLogin;
        public Button btnWechat;
        public Button btnMobile;

        public override void Hide()
        {

        }

        public override void Show()
        {
            inputAccount.text = "";
            inputPwd.text = "";
        }
    }
    [System.Serializable]
    public class MobileLogin : LayerBase
    {
        public InputField inputMobile;
        public InputField inputPwd;
        public Button btnLogin;
        public Button btnSignup;
        public Button btnForget;
        public Button btnWechat;

        public override void Hide()
        {

        }

        public override void Show()
        {

        }
    }
    [System.Serializable]
    public class FreeSignup : LayerBase
    {
        public InputField inputMobile;
        public InputField inputAnswer;
        public RawImage imgQuestion;
        public InputField inputIdentifying;
        public Text textIdentifying;
        public InputField inputPwd1;
        public InputField inputPwd2;
        public Button btnConfirm;

        [HideInInspector]
        public string authCode;
        private int timerId = -1;
        private int countDown = 0;
        public int CountDown
        {
            get
            {
                return countDown;
            }
            set
            {
                XTime.Instance.RemoveTimer(timerId);
                countDown = value;
                if (countDown > 0)
                {
                    textIdentifying.text = string.Format("重新获取({0})", countDown);
                    timerId = XTime.Instance.AddTimer(1, countDown, () =>
                    {
                        countDown--;
                        if (countDown > 0)
                        {
                            textIdentifying.text = string.Format("重新获取({0})", countDown);
                        }
                        else
                        {
                            textIdentifying.text = "获取验证码";
                        }
                    });
                }
                else
                {
                    textIdentifying.text = "获取验证码";
                }
            }
        }

        public override void Hide()
        {

        }

        public override void Show()
        {

        }
    }
    [System.Serializable]
    public class ForgetPassword1 : LayerBase
    {
        public InputField inputMobile;
        public InputField inputAnswer;
        public RawImage imgQuestion;
        public InputField inputIdentifying;
        public Text textIdentifying;

        public Button btnContinue;

        [HideInInspector]
        public string authCode;
        private int timerId = -1;
        private int countDown = 0;
        public int CountDown
        {
            get
            {
                return countDown;
            }
            set
            {
                XTime.Instance.RemoveTimer(timerId);
                countDown = value;
                if (countDown > 0)
                {
                    textIdentifying.text = string.Format("重新获取({0})", countDown);
                    timerId = XTime.Instance.AddTimer(1, countDown, () =>
                    {
                        countDown--;
                        if (countDown > 0)
                        {
                            textIdentifying.text = string.Format("重新获取({0})", countDown);
                        }
                        else
                        {
                            textIdentifying.text = "获取验证码";
                        }
                    });
                }
                else
                {
                    textIdentifying.text = "获取验证码";
                }
            }
        }

        public override void Hide()
        {
        }

        public override void Show()
        {
            inputMobile.text = "";
            inputAnswer.text = "";
            inputIdentifying.text = "";
            CountDown = 0;
        }
    }
    [System.Serializable]
    public class ForgetPassword2 : LayerBase
    {
        public GameObject layer;
        public InputField inputPwd1;
        public InputField inputPwd2;
        public Button btnConfirm;

        public override void Hide()
        {

        }

        public override void Show()
        {

        }
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
        stackLayer = new Stack<LayerBase>();
        OpenLayer(loginLayer1);
    }

    public void OnDispose(){
        forgetLayer1.CountDown = 0;
        signupLayer.CountDown = 0;
    }

    void OpenLayer(LayerBase layer)
    {
        if (!stackLayer.Contains(layer))
        {
            layer.Show();
            if (stackLayer.Count == 0)
            {
                layer.root.SetActive(true);
            }
            else
            {
                LayerBase hideLayer = stackLayer.Peek();
                hideLayer.Hide();
                EnterAnim(layer.root);
            }
            stackLayer.Push(layer);
            btnReturn.gameObject.SetActive(stackLayer.Count > 1);
        }
    }
    void CloseLayer()
    {
        if (stackLayer.Count > 0)
        {
            LayerBase closeLayer = stackLayer.Pop();
            btnReturn.gameObject.SetActive(stackLayer.Count > 1);
            if (stackLayer.Count == 0)
            {
                closeLayer.root.SetActive(false);
                closeLayer.Hide();
            }
            else
            {
                ExitAnim(closeLayer.root, () => { closeLayer.Hide(); });
                LayerBase openLayer = stackLayer.Peek();
                openLayer.Show();
            }
        }
    }

    void EnterAnim(GameObject layer, Callback callback = null)
    {
        RectTransform layerTrans = layer.transform as RectTransform;
        layerTrans.anchoredPosition = new Vector2(layerTrans.rect.width, layerTrans.anchoredPosition.y);

        Sequence enterSequence = DOTween.Sequence();
        enterSequence.AppendCallback(() => { layer.SetActive(true); });
        enterSequence.AppendCallback(() => { screenMask.SetActive(true); });
        enterSequence.Append(layerTrans.DOAnchorPosX(0, 0.5f).SetEase(Ease.OutQuint));
        enterSequence.AppendCallback(() => { screenMask.SetActive(false); });
        enterSequence.AppendCallback(() => { if (callback != null) { callback(); } });
    }
    void ExitAnim(GameObject layer, Callback callback = null)
    {
        RectTransform layerTrans = layer.transform as RectTransform;
        Vector2 targetPos = new Vector2(layerTrans.rect.width, layerTrans.anchoredPosition.y);

        Sequence exitSequence = DOTween.Sequence();
        exitSequence.AppendCallback(() => { screenMask.SetActive(true); });
        exitSequence.Append(layerTrans.DOAnchorPosX(layerTrans.rect.width, 0.5f).SetEase(Ease.OutQuint));
        exitSequence.AppendCallback(() => { screenMask.SetActive(false); });
        exitSequence.AppendCallback(() => { layer.SetActive(false); });
        exitSequence.AppendCallback(() => { if (callback != null) { callback(); } });
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
            if (responseData.status == "200")
            {
                Debug.Log("登录成功" + responseData.msg);
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
        GlobalManager.Instance.AuthWechat((requestOther) =>
        {
            if (requestOther != null)
            {

                LoginManager.Instance.SendOtherMessage<ResponseLogin>(requestOther, (responseData) =>
                    {
                        if (responseData.status == "200")
                        {
                            Debug.Log("登录成功" + responseData.msg);
                            UITipsDialog.ShowTips("登录成功");
                            loginCallback(responseData.data);
                        }
                        else
                        {
                            UITipsDialog.ShowTips(responseData.msg);
                        }
                    });
            }
        });
    }
    void LoginLayer1MobileBtn()
    {
        OpenLayer(loginLayer2);
    }

    void LoginLayer2LoginBtn()
    {
        if (string.IsNullOrEmpty(loginLayer2.inputMobile.text))
        {
            UITipsDialog.ShowTips("请输入手机号");
            return;
        }
        if (string.IsNullOrEmpty(loginLayer2.inputPwd.text))
        {
            UITipsDialog.ShowTips("请输入密码");
            return;
        }
        RequestLogin requestLogin = new RequestLogin();
        requestLogin.phone = loginLayer2.inputMobile.text;
        requestLogin.password = loginLayer2.inputPwd.text;
        requestLogin.equitment = SystemInfo.deviceUniqueIdentifier;

        LoginManager.Instance.SendLoginMessage<ResponseLogin>(requestLogin, (responseData) =>
        {
            if (responseData.status == "200")
            {
                Debug.Log("登录成功" + responseData.msg);
                UITipsDialog.ShowTips("登录成功");
                loginCallback(responseData.data);
            }
            else
            {
                UITipsDialog.ShowTips(responseData.msg);
            }
        });
    }
    void LoginLayer2SignupBtn()
    {
        OpenLayer(signupLayer);
        LoginManager.Instance.GetValidateCodeImage(SystemInfo.deviceUniqueIdentifier, (result, texture) =>
        {
            if (result)
            {
                signupLayer.imgQuestion.texture = texture;
            }
        });
    }
    void LoginLayer2ForgetBtn()
    {
        OpenLayer(forgetLayer1);
        LoginManager.Instance.GetValidateCodeImage(SystemInfo.deviceUniqueIdentifier, (result, texture) =>
        {
            if (result)
            {
                forgetLayer1.imgQuestion.texture = texture;
            }
        });
    }
    void LoginLayer2WechatBtn()
    {
        GlobalManager.Instance.AuthWechat((requestOther) =>
        {
            if (requestOther != null)
            {
                LoginManager.Instance.SendOtherMessage<ResponseLogin>(requestOther, (responseData) =>
                {
                    if (responseData.status == "200")
                    {
                        Debug.Log("登录成功" + responseData.msg);
                        UITipsDialog.ShowTips("登录成功");
                        loginCallback(responseData.data);
                    }
                    else
                    {
                        UITipsDialog.ShowTips(responseData.msg);
                    }
                });
            }
        });
    }

    void SignupLayerIdentifyText(GameObject go)
    {
        if (signupLayer.CountDown > 0)
        {
            return;
        }
        string mobile = signupLayer.inputMobile.text;
        string answer = signupLayer.inputAnswer.text;
        if (string.IsNullOrEmpty(mobile))
        {
            UITipsDialog.ShowTips("请输入手机号");
            return;
        }
        if (string.IsNullOrEmpty(signupLayer.inputAnswer.text))
        {
            UITipsDialog.ShowTips("请输入问题答案");
            return;
        }
        RequestAuthCode param = new RequestAuthCode();
        param.phone = mobile;
        param.zuoti = answer;
        param.type = "register";
        param.equitmentTime = SystemInfo.deviceUniqueIdentifier;

        LoginManager.Instance.SendAuthCode<ResponseAuthCode>(param, (responseData) =>
        {
            if (responseData.status == "200")
            {
                signupLayer.CountDown = 60;
                signupLayer.authCode = responseData.data.code;
            }
            else
            {
                UITipsDialog.ShowTips(responseData.msg);
            }
        });
    }
    void SignupLayerConfirmBtn()
    {
        if (string.IsNullOrEmpty(signupLayer.inputMobile.text))
        {
            UITipsDialog.ShowTips("请输入手机号");
            return;
        }
        if (string.IsNullOrEmpty(signupLayer.inputAnswer.text))
        {
            UITipsDialog.ShowTips("请输入问题答案");
            return;
        }
        if (string.IsNullOrEmpty(signupLayer.inputIdentifying.text))
        {
            UITipsDialog.ShowTips("请输入手机验证码");
            return;
        }
        string pwd1 = signupLayer.inputPwd1.text;
        string pwd2 = signupLayer.inputPwd2.text;
        if (string.IsNullOrEmpty(pwd1))
        {
            UITipsDialog.ShowTips("请输入新密码");
            return;
        }
        if (string.IsNullOrEmpty(pwd2))
        {
            UITipsDialog.ShowTips("请确认新密码");
            return;
        }
        if (!string.Equals(pwd1, pwd2))
        {
            UITipsDialog.ShowTips("两次密码不一致");
            return;
        }

        RequestFreeSignup param = new RequestFreeSignup();
        param.phone = signupLayer.inputMobile.text;
        param.code = signupLayer.inputIdentifying.text;
        param.password = pwd1;
        param.equitment = SystemInfo.deviceUniqueIdentifier;


        LoginManager.Instance.SendFreeSingup<ResponseFreeSignup>(param, (responseData) =>
        {
            if (responseData.status == "200")
            {
                UITipsDialog.ShowTips("注册成功");
            }
            else
            {
                UITipsDialog.ShowTips(responseData.msg);
            }
        });
    }

    void ForgetLayer1IdentifyText(GameObject go)
    {
        if (forgetLayer1.CountDown > 0)
        {
            return;
        }
        if (string.IsNullOrEmpty(forgetLayer1.inputMobile.text))
        {
            UITipsDialog.ShowTips("请输入手机号");
            return;
        }
        if (string.IsNullOrEmpty(forgetLayer1.inputAnswer.text))
        {
            UITipsDialog.ShowTips("请输入问题答案");
            return;
        }
        RequestAuthCode param = new RequestAuthCode();
        param.phone = forgetLayer1.inputMobile.text;
        param.zuoti = forgetLayer1.inputAnswer.text;
        param.type = "forget";
        param.equitmentTime = SystemInfo.deviceUniqueIdentifier;

        LoginManager.Instance.SendAuthCode<ResponseAuthCode>(param, (responseData) =>
        {
            if (responseData.status == "200")
            {
                forgetLayer1.CountDown = 60;
                forgetLayer1.authCode = responseData.data.code;
            }
            else
            {
                UITipsDialog.ShowTips(responseData.msg);
            }
        });
    }
    void ForgetLayer1ContinueBtn()
    {
        if (string.IsNullOrEmpty(forgetLayer1.inputMobile.text))
        {
            UITipsDialog.ShowTips("请输入手机号");
            return;
        }
        if (string.IsNullOrEmpty(forgetLayer1.inputAnswer.text))
        {
            UITipsDialog.ShowTips("请输入问题答案");
            return;
        }
        if (string.IsNullOrEmpty(forgetLayer1.inputIdentifying.text))
        {
            UITipsDialog.ShowTips("请输入手机验证码");
            return;
        }

        OpenLayer(forgetLayer2);
    }

    void ForgetLayer2ConfirmBtn()
    {
        string pwd1 = forgetLayer2.inputPwd1.text;
        string pwd2 = forgetLayer2.inputPwd2.text;
        if (string.IsNullOrEmpty(pwd1))
        {
            UITipsDialog.ShowTips("请输入新密码");
            return;
        }
        if (string.IsNullOrEmpty(pwd2))
        {
            UITipsDialog.ShowTips("请确认新密码");
            return;
        }
        if (!string.Equals(pwd1, pwd2))
        {
            UITipsDialog.ShowTips("两次密码不一致");
            return;
        }
        RequestForgetPwd requestForgetPwd = new RequestForgetPwd();
        requestForgetPwd.phone = forgetLayer1.inputMobile.text;
        requestForgetPwd.code = forgetLayer1.inputIdentifying.text;
        requestForgetPwd.password = pwd1;
        requestForgetPwd.equitment = SystemInfo.deviceUniqueIdentifier;

        LoginManager.Instance.SendForgetPwd<ResponseForgetPwd>(requestForgetPwd, (responseData) =>
        {
            if (responseData.status == "200")
            {
                UITipsDialog.ShowTips("设置密码成功");
            }
            else
            {
                UITipsDialog.ShowTips(responseData.msg);
            }
        });
    }
}

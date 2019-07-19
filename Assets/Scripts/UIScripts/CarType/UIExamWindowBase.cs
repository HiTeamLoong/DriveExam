using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIExamWindowBase : UIWindow
{
    [System.Serializable]
    public class ButtonState
    {
        public Button button;
        public Image image { get { return button.targetGraphic as Image; } }
        public Sprite sprNormal;
        public Sprite sprSelect;
    }

    public ButtonState btsAnswer;   //显示答案
    public ButtonState btsChioce;   //试题练习
    public ButtonState btsRandom;   //随机题目
    public ButtonState btsVideo;    //视频讲解
    public ButtonState btsLightExam;//灯光考试
    public ButtonState btsRules;    //新规内容
    public ButtonState btsNext;     //下一题/下一套
    public Button btnHelp;          //内容提示
    public Button btnReturn;        //返回主界面

    public Text textQuestion;
    public Text textAnswer;
    public Image imgResult;
    public Sprite sprRight;
    public Sprite sprError;

    public Image imgLeftIndicator;
    public Image imgRightIndicator;

    private bool isShowVideo;   //显示视频
    private bool isNewRules;    //显示新规
    private UIVideoDialog uiVideoDialog;
    public bool IsShowVideo
    {
        get { return isShowVideo; }
        set
        {
            if (isShowVideo != value)
            {
                isShowVideo = value;
                btsVideo.image.sprite = value ? btsVideo.sprSelect : btsVideo.sprNormal;
                if (value)
                {
                    if (uiVideoDialog == null)
                    {
                        uiVideoDialog = UIManager.Instance.OpenUI<UIVideoDialog>();
                    }
                }
                else
                {
                    if (uiVideoDialog != null)
                    {
                        UIManager.Instance.CloseUI(uiVideoDialog);
                    }
                }
            }
        }
    }
    public bool IsNewRules
    {
        get { return isNewRules; }
        set
        {
            if (isNewRules != value)
            {
                isNewRules = value;
                btsRules.image.sprite = value ? btsRules.sprSelect : btsRules.sprNormal;
            }
        }
    }

    private bool isChioce;      //试题练习
    private bool isRandom;      //随机题目
    private bool isLightExam;   //灯光考试
    private bool isShowAnswer;  //显示答案


    public bool IsChioce
    {
        get { return isChioce; }
        set
        {
            if (isChioce != value)
            {
                isChioce = value;
                btsChioce.image.sprite = value ? btsChioce.sprSelect : btsChioce.sprNormal;
            }
        }
    }
    public bool IsRandom
    {
        get { return isRandom; }
        set
        {
            if (isRandom != value)
            {
                isRandom = value;
                btsRandom.image.sprite = value ? btsRandom.sprSelect : btsRandom.sprNormal;
                btsNext.button.gameObject.SetActive(isRandom);
                (btsNext.button.targetGraphic as Image).sprite = btsNext.sprSelect;
            }
        }
    }
    public bool IsLightExam
    {
        get { return isLightExam; }
        set
        {
            if (isLightExam != value)
            {
                isLightExam = value;
                btsLightExam.image.sprite = value ? btsLightExam.sprSelect : btsLightExam.sprNormal;
                btsNext.button.gameObject.SetActive(isLightExam);
                (btsNext.button.targetGraphic as Image).sprite = btsNext.sprNormal;
            }
        }
    }
    public bool IsShowAnswer
    {
        get { return isShowAnswer; }
        set
        {
            if (isShowAnswer != value)
            {
                isShowAnswer = value;
                btsAnswer.image.sprite = value ? btsAnswer.sprSelect : btsAnswer.sprNormal;
                textAnswer.gameObject.SetActive(value);
            }
        }
    }

    private LightController lightController;

    public override void OnCreate()
    {
        base.OnCreate();
        btsAnswer.button.onClick.AddListener(OnClickAnswer);
        btsRandom.button.gameObject.SetActive(false);
        btsChioce.button.onClick.AddListener(OnClickChioce);
        btsRandom.button.onClick.AddListener(OnClickRandom);
        btsVideo.button.onClick.AddListener(OnClickVideo);
        btsLightExam.button.onClick.AddListener(OnClickExam);
        btsRules.button.onClick.AddListener(OnClickRules);

        btsNext.button.onClick.AddListener(OnClickNext);
        btnHelp.onClick.AddListener(OnClickHelp);
        btnReturn.onClick.AddListener(() =>
        {
            IsShowVideo = false;
            CleanQuestion();
            CloseAllLight();
            SwitchSceneMgr.Instance.SwitchToMain();
        });

        lightController = FindObjectOfType<LightController>();
        if (lightController == null)
        {
            Debug.LogError("ERROR:ILightController is lost");
        }
        //#if CHAPTER_ONE
        //        List<VideoData> videoDatas = ConfigDataMgr.Instance.GetVideoList(GameDataMgr.Instance.carType);
        //        btsVideo.button.gameObject.SetActive(videoDatas.Count > 0);
        //        examTip = GameDataMgr.Instance.carVersion == CarVersion.NEW ? ConfigDataMgr.Instance.gameConfig.examtip_new : ConfigDataMgr.Instance.gameConfig.examtip_old;
        //#elif CHAPTER_TWO
        btsVideo.button.gameObject.SetActive(GameDataMgr.Instance.carInfo.listvideo.Count > 0);

        examTip = new ExamTipData()
        {
            exam_audio = GameDataMgr.Instance.carInfo.TypeModel.examaudio,
            broadcast_end = GameDataMgr.Instance.carInfo.TypeModel.broadcastend.Trim(),
            exam_tip = GameDataMgr.Instance.carInfo.TypeModel.examtip
        };
        //#endif
    }

    #region BaseFunction
    /// <summary>
    /// 点击视频教学
    /// </summary>
    void OnClickVideo()
    {
        IsShowVideo = !IsShowVideo;
    }
    /// <summary>
    /// 点击查看新规
    /// </summary>
    void OnClickRules()
    {
        UIManager.Instance.OpenUI<UIRulesDialog>();
    }

    /// <summary>
    /// 点击显示答案
    /// </summary>
    void OnClickAnswer()
    {
        IsShowAnswer = !IsShowAnswer;
    }

    /// <summary>
    /// 试题选择
    /// </summary>
    void OnClickChioce()
    {
        IsRandom = false;
        IsLightExam = false;
        IsChioce = !IsChioce;
        if (IsChioce)
        {
            UIChoiceDialog dialog = UIManager.Instance.OpenUI<UIChoiceDialog>();
            dialog.InitWith(
                (index) =>
                {
                    StartChoiceExam(index);
                },
                () =>
                {
                    IsChioce = false;
                });
        }
        else
        {
            CleanQuestion();
        }
    }

    /// <summary>
    /// 点击随机题目
    /// </summary>
    void OnClickRandom()
    {
        IsChioce = false;
        IsLightExam = false;
        IsRandom = !IsRandom;
        if (IsRandom)
        {
            StartExercise();
        }
        else
        {
            CleanQuestion();
        }
    }

    /// <summary>
    /// 点击灯光考试
    /// </summary>
    void OnClickExam()
    {
        IsChioce = false;
        IsRandom = false;
        IsLightExam = !IsLightExam;
        if (IsLightExam)
        {
            StartLightExam();
        }
        else
        {
            CleanQuestion();
        }
    }
    /// <summary>
    /// 点击下一套/下一题
    /// </summary>
    void OnClickNext()
    {
        if (IsLightExam)//灯光考试
        {
            StartLightExam();
        }
        else if (IsRandom)//随机练习
        {
            StartExercise();
        }
    }
    /// <summary>
    /// 帮助提示
    /// </summary>
    void OnClickHelp()
    {
        UIManager.Instance.OpenUI<UIExamTipsDialog>();
    }
    #endregion

    #region SwitchModule
    /// <summary>
    /// 示廓灯开关
    /// </summary>
    private bool clearanceSwitch;
    public virtual bool ClearanceSwitch
    {
        get { return clearanceSwitch; }
        set
        {
            clearanceSwitch = value;
            HeadlightSwitch &= !value;
            lightController.SetClearanc(ClearanceLamp);
            lightController.SetFogLight(FrontFogLamp);
        }
    }
    /// <summary>
    /// 前照灯开关
    /// </summary>
    private bool headlightSwitch;
    public virtual bool HeadlightSwitch
    {
        get { return headlightSwitch; }
        set
        {
            headlightSwitch = value;
            ClearanceSwitch &= !value;
            lightController.SetClearanc(ClearanceLamp);
            lightController.SetHeadFar(HigBeamLight);
            lightController.SetHeadNear(LowBeamLight);
            lightController.SetFogLight(FrontFogLamp);
        }
    }
    /// <summary>
    /// 前雾灯开关
    /// </summary>
    private bool frontFogSwitch;
    public virtual bool FrontFogSwitch
    {
        get { return frontFogSwitch; }
        set
        {
            frontFogSwitch = value;
            lightController.SetFogLight(FrontFogLamp);
        }
    }
    /// <summary>
    /// 后雾灯开关
    /// </summary>
    private bool rearFogSwitch;
    public virtual bool RearFogSwitch
    {
        get { return rearFogSwitch; }
        set
        {
            rearFogSwitch = value;
        }
    }
    /// <summary>
    /// 左指示器开关
    /// </summary>
    private bool leftIndicatorSwitch;
    public virtual bool LeftIndicatorSwitch
    {
        get { return leftIndicatorSwitch; }
        set
        {
            if (value != leftIndicatorSwitch)
            {
                leftIndicatorSwitch = value;
                RightIndicatorSwitch &= !value;
                SetLeftIndicator();
            }
        }
    }
    /// <summary>
    /// 右指示器开关
    /// </summary>
    private bool rightIndicatorSwitch;
    public virtual bool RightIndicatorSwitch
    {
        get { return rightIndicatorSwitch; }
        set
        {
            if (value != rightIndicatorSwitch)
            {
                rightIndicatorSwitch = value;
                LeftIndicatorSwitch &= !value;
                SetRightIndicator();
            }
        }
    }
    /// <summary>
    /// 双闪灯开关
    /// </summary>
    private bool doubleJumpSwitch;
    public virtual bool DoubleJumpSwitch
    {
        get { return doubleJumpSwitch; }
        set
        {
            doubleJumpSwitch = value;
            SetLeftIndicator();
            SetRightIndicator();
            SetDoubleJumpLamp();
        }
    }
    /// <summary>
    /// 远光大灯开关
    /// </summary>
    private bool farHeadlightSwitch;//默认是近光状态
    public virtual bool FarHeadlightSwitch
    {
        get { return farHeadlightSwitch; }
        set
        {
            farHeadlightSwitch = value;
            lightController.SetHeadFar(HigBeamLight);
            lightController.SetHeadNear(LowBeamLight);
        }
    }
    /// <summary>
    /// 远近切换开关
    /// </summary>
    private bool toggleHeadlightSwitch;
    public virtual bool ToggleHeadlightSwitch
    {
        get { return toggleHeadlightSwitch; }
        set
        {
            if (value != toggleHeadlightSwitch)
            {
                toggleHeadlightSwitch = value;
                if (!value)
                {
                    LowToHigCount += 1;
                }
            }
            Debug.Log("lightChange :\t" + lowToHigCount);
        }
    }
    /// <summary>
    /// 远近切换计数
    /// </summary>
    private int lowToHigCount = 0;
    public int LowToHigCount
    {
        get { return lowToHigCount; }
        set
        {
            Debug.Log("lightChange :\t" + value + "       " + lowToHigCount);
            if (value == 0 || (value - 1) == lowToHigCount)
            {
                lowToHigCount = value;
            }
            Debug.Log("lightChange :\t" + value + "       " + lowToHigCount);
        }
    }
    #endregion

    #region LightStatus
    public bool DoubleJumpLamp
    {
        get { return DoubleJumpSwitch; }
    }
    public bool ClearanceLamp
    {
        get { return ClearanceSwitch || HeadlightSwitch; }
    }
    public bool LowBeamLight
    {
        get { return HeadlightSwitch && !FarHeadlightSwitch; }
    }
    public bool HigBeamLight
    {
        get { return HeadlightSwitch && FarHeadlightSwitch; }
    }
    public bool FrontFogLamp
    {
        get { return FrontFogSwitch && (ClearanceSwitch || HeadlightSwitch); }
    }
    public bool RearFogLamp
    {
        get { return RearFogSwitch && (ClearanceSwitch || HeadlightSwitch); }
    }
    public bool LeftIndicator
    {
        get { return LeftIndicatorSwitch; }
    }
    public bool RightIndicator
    {
        get { return RightIndicatorSwitch; }
    }
    public bool LowToHigLight
    {
        get { return lowToHigCount == 2; }
    }

    #endregion

    private bool isOperation;//正确后是否操作
    /// <summary>
    /// 进行了开关变更操作--
    /// </summary>
    protected void OnSwitchChange()
    {
        //正确性检测
        isOperation = true;
        //灯光变更修正
    }

    /// <summary>
    /// 关闭所有灯光
    /// </summary>
    private void CloseAllLight()
    {
        ClearanceSwitch = false;
        HeadlightSwitch = false;
        FrontFogSwitch = false;
        RearFogSwitch = false;
        LeftIndicatorSwitch = false;
        RightIndicatorSwitch = false;
        DoubleJumpSwitch = false;
        FarHeadlightSwitch = false;
        ToggleHeadlightSwitch = false;
    }


    private Sequence leftSequence;
    private Sequence rightSequence;
    private Sequence doubleSequence;

    private AudioObject leftLightAudio;
    private AudioObject rightLightAudio;
    private AudioObject doubleLightAudio;

    private readonly float loopInterval = 0.915f;

    void SetLeftIndicator()
    {
        if (LeftIndicator && !DoubleJumpLamp)
        {
            leftSequence = DOTween.Sequence();
            leftSequence.SetLoops(-1);
            leftSequence.Append(imgLeftIndicator.DOFade(1f, loopInterval / 2f).SetEase(Ease.OutQuint));
            leftSequence.Append(imgLeftIndicator.DOFade(0f, loopInterval / 2f).SetEase(Ease.OutQuint));
            leftLightAudio = AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("soundforlight"), true);
        }
        else
        {
            if (leftLightAudio != null)
            {
                AudioSystemMgr.Instance.StopSoundByAudio(leftLightAudio);
                leftLightAudio = null;
            }
            if (leftSequence != null)
            {
                leftSequence.Kill();
                leftSequence = null;
            }
            imgLeftIndicator.DOFade(0f, 0);
        }
    }
    void SetRightIndicator()
    {
        if (RightIndicator && !DoubleJumpLamp)
        {
            rightSequence = DOTween.Sequence();
            rightSequence.SetLoops(-1);
            rightSequence.Append(imgRightIndicator.DOFade(1f, loopInterval / 2f).SetEase(Ease.OutQuint));
            rightSequence.Append(imgRightIndicator.DOFade(0f, loopInterval / 2f).SetEase(Ease.OutQuint));
            rightLightAudio = AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("soundforlight"), true);
        }
        else
        {
            if (rightLightAudio != null)
            {
                AudioSystemMgr.Instance.StopSoundByAudio(rightLightAudio);
                rightLightAudio = null;
            }
            if (rightSequence != null)
            {
                rightSequence.Kill();
                rightSequence = null;
            }
            imgRightIndicator.DOFade(0f, 0);
        }
    }

    void SetDoubleJumpLamp()
    {
        if (DoubleJumpLamp)
        {
            doubleSequence = DOTween.Sequence();
            doubleSequence.SetLoops(-1);
            doubleSequence.Append(imgLeftIndicator.DOFade(1f, loopInterval / 2f).SetEase(Ease.OutQuint));
            doubleSequence.Join(imgRightIndicator.DOFade(1f, loopInterval / 2f).SetEase(Ease.OutQuint));
            doubleSequence.Append(imgLeftIndicator.DOFade(0f, loopInterval / 2f).SetEase(Ease.OutQuint));
            doubleSequence.Join(imgRightIndicator.DOFade(0f, loopInterval / 2f).SetEase(Ease.OutQuint));
            doubleLightAudio = AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("soundforlight"), true);
        }
        else
        {
            if (doubleLightAudio != null)
            {
                AudioSystemMgr.Instance.StopSoundByAudio(doubleLightAudio);
                doubleLightAudio = null;
            }
            if (doubleSequence != null)
            {
                doubleSequence.Kill();
                doubleSequence = null;
            }
            imgLeftIndicator.DOFade(0f, 0);
            imgRightIndicator.DOFade(0f, 0);
        }
    }



    /// <summary>
    /// 新旧版本的提示配置
    /// </summary>
    private ExamTipData examTip;
    /// <summary>
    /// 语音播放控件
    /// </summary>
    private AudioObject audioObject;

    /// <summary>
    /// 停止试题运行
    /// </summary>
    private void PauseQuestion()
    {
        StopAllCoroutines();
        AudioSystemMgr.Instance.StopSoundByAudio(audioObject);
        //imgResult.gameObject.SetActive(false);
    }

    /// <summary>
    /// 清理试题信息
    /// </summary>
    private void CleanQuestion()
    {
        PauseQuestion();
        CloseAllLight();
        textQuestion.text = "";
        textAnswer.text = "";
        imgResult.gameObject.SetActive(false);
    }



    //#if CHAPTER_ONE

    //    /// <summary>
    //    /// 考试试题列表
    //    /// </summary>
    //    private List<string> examList = new List<string>();
    //    /// <summary>
    //    /// 随机练习列表
    //    /// </summary>
    //    private List<string> exerList = new List<string>();


    //    /// <summary>
    //    /// 开始随机练习
    //    /// </summary>
    //    private void StartExercise()
    //    {
    //        CleanQuestion();
    //        CloseAllLight();
    //        string index = RandomExerIndex();
    //        cQuestionData question = ConfigDataMgr.Instance.GetQuestionByIndex(index);
    //        StartCoroutine(BeginQuestion(question, false));
    //    }

    //    /// <summary>
    //    /// 开始灯光考试
    //    /// </summary>
    //    private void StartLightExam()
    //    {
    //        CleanQuestion();
    //        CloseAllLight();
    //        //生成试题列表
    //        RandomExamList();
    //        StartCoroutine(_BeginLightExam());
    //    }

    //    private void RandomExamList()
    //    {
    //        ExamData examData = ConfigDataMgr.Instance.gameConfig.examList[GameDataMgr.Instance.carType.ToString().ToUpper()];
    //        int count = examData.exam.Count;
    //        if (examList != null)
    //        {
    //            int index = examData.exam.IndexOf(examList);
    //            index++;
    //            index %= examData.exam.Count;
    //            examList = examData.exam[index];
    //        }
    //        else
    //        {
    //            examList = examData.exam[Random.Range(0, examData.exam.Count)];
    //        }
    //    }

    //    private string RandomExerIndex()
    //    {
    //        ExamData examData = ConfigDataMgr.Instance.gameConfig.examList[GameDataMgr.Instance.carType.ToString().ToUpper()];
    //        int count = examData.random.Count;
    //        exerList = new List<string>();
    //        for (int i = 0; i < count; i++)
    //        {
    //            exerList.Add(examData.random[i]);
    //        }
    //        int random = Random.Range(0, count / 2);
    //        string index = exerList[random];
    //        exerList.Remove(index);
    //        exerList.Add(index);
    //        return index;
    //    }


    //    IEnumerator _BeginLightExam()
    //    {
    //        textQuestion.text = examTip.exam_tip;
    //        textAnswer.text = "";
    //        audioObject = AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.GetAudioWithURL(examTip.exam_audio));
    //        Debug.Log(audioObject.playTime);
    //        yield return new WaitForSeconds(audioObject.playTime);
    //        audioObject = null;
    //        yield return new WaitForSeconds(3f);
    //        for (int i = 0; i < examList.Count; i++)
    //        {
    //            cQuestionData question = ConfigDataMgr.Instance.GetQuestionByIndex(examList[i]);
    //            yield return StartCoroutine(BeginQuestion(question, true, i != 0));
    //        }
    //        //yield return StartCoroutine(BeginQuestion(ConfigDataMgr.ExamEnd, true));
    //        textAnswer.text = "恭喜你全部操作成功！！！";
    //        imgResult.gameObject.SetActive(true);
    //        imgResult.sprite = sprRight;
    //        AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("right"));
    //    }

    //    /// <summary>
    //    /// Begins the question.
    //    /// </summary>
    //    /// <returns>The question.</returns>
    //    /// <param name="question">Question.</param>
    //    IEnumerator BeginQuestion(cQuestionData question, bool isExam, bool ding = true)
    //    {
    //        textQuestion.text = question.question;
    //        textAnswer.text = question.answer;
    //        textAnswer.gameObject.SetActive(false || IsShowAnswer);
    //        audioObject = AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.GetAudioWithURL(question.audio));
    //        Debug.Log(audioObject.playTime);
    //        yield return new WaitForSeconds(audioObject.playTime);
    //        audioObject = null;

    //        if (ding && !string.IsNullOrEmpty(examTip.broadcast_end))
    //        {
    //            AudioObject audioBroadcast = AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.GetAudioWithURL(examTip.broadcast_end));
    //            yield return new WaitForSeconds(audioBroadcast.playTime);
    //        }

    //        LowToHigCount = 0;//防止抢先操作
    //        yield return new WaitForSeconds(4f);//操作时间

    //        bool result = true;
    //        result &= (question.DoubleJumpLamp == DoubleJumpLamp);
    //        result &= (question.ClearAnceLamp == ClearanceLamp);
    //        result &= (question.LowBeamLight == LowBeamLight);
    //        result &= (question.HigBeamLight == HigBeamLight);
    //        result &= (question.FrontFogLamp == FrontFogLamp);
    //        result &= (question.RearFogLamp == RearFogLamp);
    //        result &= (question.LeftIndicator == LeftIndicator);
    //        result &= (question.RightIndicator == RightIndicator);
    //        result &= (question.LowToHigLight == (LowToHigCount >= 2));

    //        result &= !(!question.LowToHigLight && lowToHigCount > 0);
    //        textAnswer.gameObject.SetActive(true);

    //        imgResult.gameObject.SetActive(true);
    //        imgResult.sprite = result ? sprRight : sprError;
    //        if (result)
    //        {
    //            if (!isExam)
    //            {
    //                imgResult.gameObject.SetActive(true);
    //                imgResult.sprite = result ? sprRight : sprError;
    //                AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("right"));
    //            }
    //            yield return new WaitForSeconds(3.0f);
    //            imgResult.gameObject.SetActive(false);
    //        }
    //        else
    //        {
    //            imgResult.gameObject.SetActive(true);
    //            imgResult.sprite = result ? sprRight : sprError;
    //            AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("error"));
    //            PauseQuestion();
    //        }
    //    }

    //#elif CHAPTER_TWO
    /// <summary>
    /// 开始随机练习
    /// </summary>
    void StartExercise()
    {
        CleanQuestion();
        CloseAllLight();
        sQuestionData question = RandomExerIndex();
        StartCoroutine(BeginExercise(question));
    }

    /// <summary>
    /// 开始灯光考试
    /// </summary>
    void StartLightExam()
    {
        CleanQuestion();
        CloseAllLight();
        List<int> examList = GameDataMgr.Instance.carInfo.GetExamList();
        StartCoroutine(BeginLightExam(examList));
    }

    /// <summary>
    /// 开始某套试题练习
    /// </summary>
    /// <param name="index">试题的索引</param>
    void StartChoiceExam(int index)
    {
        CleanQuestion();
        CloseAllLight();
        List<int> examList = GameDataMgr.Instance.carInfo.GetExamList(index);
        StartCoroutine(BeginLightExam(examList));
    }

    private List<int> exerList = null;

    private sQuestionData RandomExerIndex()
    {
        if (exerList == null)
        {
            exerList = new List<int>(GameDataMgr.Instance.carInfo.suiji);
        }
        int count = exerList.Count;
        int random = Random.Range(0, count / 2);
        int index = exerList[random];
        exerList.Remove(index);
        exerList.Add(index);

        return GameDataMgr.Instance.carInfo.GetQuestionWithId(index);
    }

    /// <summary>
    /// Begins the question.
    /// </summary>
    /// <returns>The question.</returns>
    /// <param name="question">Question.</param>
    IEnumerator BeginExercise(sQuestionData question)
    {
        #region MyRegion
        textQuestion.text = question.question;
        textAnswer.text = question.answer;
        textAnswer.gameObject.SetActive(IsShowAnswer);
        audioObject = AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.GetAudioWithURL(question.audio));
        Debug.Log(audioObject.playTime);
        yield return new WaitForSeconds(audioObject.playTime);
        audioObject = null;

        if (/*ding && */!string.IsNullOrEmpty(examTip.broadcast_end))
        {
            AudioObject audioBroadcast = AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.GetAudioWithURL(examTip.broadcast_end));
            yield return new WaitForSeconds(audioBroadcast.playTime);
        }
        LowToHigCount = 0;//防止抢先操作
        yield return new WaitForSeconds(4f);//操作时间

        bool result = GetQuestionResult(question);

        textAnswer.gameObject.SetActive(true);

        imgResult.gameObject.SetActive(true);
        imgResult.sprite = result ? sprRight : sprError;

        if (result)
        {
            imgResult.gameObject.SetActive(true);
            imgResult.sprite = result ? sprRight : sprError;
            AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("right"));

            yield return new WaitForSeconds(3.0f);
            imgResult.gameObject.SetActive(false);
        }
        else
        {
            imgResult.gameObject.SetActive(true);
            imgResult.sprite = result ? sprRight : sprError;
            AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("error"));
            PauseQuestion();
        }
        #endregion
    }

    IEnumerator BeginLightExam(List<int> examList)
    {
        bool isPass = true;     //是否通过考试
        //bool isBreak = false;   //是否中断考试
        int totalScore = 100;       //考试总成绩
        float totalTime = 5f;       //考试总时长  

        //试题开始的提示信息
        textQuestion.text = examTip.exam_tip;
        textAnswer.text = "";
        audioObject = AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.GetAudioWithURL(examTip.exam_audio));
        Debug.Log(audioObject.playTime);
        yield return new WaitForSeconds(audioObject.playTime);
        audioObject = null;
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < examList.Count; i++)
        {
            sQuestionData question = GameDataMgr.Instance.carInfo.GetQuestionWithId(examList[i]);

            #region MyRegion
            textQuestion.text = question.question;
            textAnswer.text = question.answer;
            textAnswer.gameObject.SetActive(IsShowAnswer);
            audioObject = AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.GetAudioWithURL(question.audio));
            Debug.Log(audioObject.playTime);
            float playTime = Mathf.Clamp(audioObject.playTime - 0.2f, 0.2f, audioObject.playTime);
            LowToHigCount = 0;//防止抢先操作
            yield return new WaitForSeconds(playTime);
            audioObject = null;
            Debug.LogWarning("开始答题：" + question.question);
            if (!string.IsNullOrEmpty(examTip.broadcast_end))
            {
                Debug.LogWarning("Ding ^^^^^^^^^^^^ ");
                AudioObject audioBroadcast = AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.GetAudioWithURL(examTip.broadcast_end));
                yield return new WaitForSeconds(audioBroadcast.playTime);
            }

            bool result = GetQuestionResult(question);
            float delay;
            if (result)     //默认灯光是正确的
            {
                Debug.LogWarning("答案默认正确");
                delay = totalTime;
                isOperation = false;
                while (delay > 0f)
                {
                    yield return null;
                    delay -= Time.deltaTime;
                    if (isOperation)  //如果正确时进行其他操作变错误
                    {

                        Debug.LogWarning("等待时间中进行了操作");
                        result = false;
                        break;
                    }
                }
            }
            else            //默认灯光是不正确
            {
                Debug.LogWarning("答案默认错误");
                delay = totalTime;
                do
                {
                    yield return null;
                    delay -= Time.deltaTime;
                    if (isOperation)
                    {
                        isOperation = false;
                        result = GetQuestionResult(question);
                    }
                } while (delay > 0 && !result);

                if (result)     //等待剩余时间
                {
                    Debug.LogWarning("答题时间：" + delay);
                    isOperation = false;
                    delay = Mathf.Clamp(delay, 0f, 1f);
                    while (delay > 0f)
                    {
                        yield return null;
                        delay -= Time.deltaTime;
                        if (isOperation)  //如果正确时进行其他操作变错误
                        {
                            Debug.LogWarning("等待时间中进行了操作");
                            result = false;
                            break;
                        }
                    }
                }
            }

            textAnswer.gameObject.SetActive(true);
            imgResult.gameObject.SetActive(true);
            imgResult.sprite = result ? sprRight : sprError;
            if (result)
            {
                Debug.LogWarning("答题----正确");
                yield return new WaitForSeconds(3.0f);
                imgResult.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("答题----错误");
                imgResult.gameObject.SetActive(true);
                imgResult.sprite = result ? sprRight : sprError;
                AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("error"));
                totalScore -= question.score;
                //PauseQuestion();
            }

            //未通过考试，进行首次提示
            if (isPass && totalScore < 90)
            {

                Debug.LogWarning("未通过考试---提示");
                isPass = false;
                bool waiting = true;
                bool choice = false;
                UIPrompDialog.ShowPromp(UIPrompDialog.PrompType.CancelAndConfirm, "", "考试不合格，是否继续考试？",
                    (confirm) =>
                    {
                        choice = confirm;
                        waiting = false;
                    }, "继续考试", "放弃考试");

                while (waiting)
                {
                    yield return null;
                }

                if (!choice)    //选择放弃考试
                {

                    Debug.LogWarning("放弃考试");
                    IsLightExam = false;
                    IsChioce = false;
                    yield break;
                }
            }
            #endregion
        }

        textQuestion.text = "";
        if (isPass)     //通过考试
        {
            if (totalScore == 100)       //满分通过
            {
                textAnswer.text = "<color=#00ff00>恭喜你满分通过考试！！！</color>";
                imgResult.gameObject.SetActive(true);
                imgResult.sprite = sprRight;
                AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("right"));
            }
            else
            {
                textAnswer.text = "<color=#00ff00>恭喜你通过考试！！！</color>";
                imgResult.gameObject.SetActive(true);
                imgResult.sprite = sprRight;
                AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("right"));
            }
        }
        else            //未通过考试
        {
            textAnswer.text = "<color=#ff0000>考试未通过</color>";
            imgResult.gameObject.SetActive(true);
            imgResult.sprite = sprError;
            AudioSystemMgr.Instance.PlaySoundByClip(ResourcesMgr.Instance.LoadAudioClip("error"));
            PauseQuestion();
        }
    }

    /// <summary>
    /// 获取答题结果是否
    /// </summary>
    /// <returns><c>true</c>, if question result was gotten, <c>false</c> otherwise.</returns>
    /// <param name="question">Question.</param>
    private bool GetQuestionResult(sQuestionData question)
    {
        bool result = true;
        result &= ((question.DoubleJumpLamp == 1) == DoubleJumpLamp);
        result &= ((question.ClearAnceLamp == 1) == ClearanceLamp);
        result &= ((question.LowBeamLight == 1) == LowBeamLight);
        result &= ((question.HigBeamLight == 1) == HigBeamLight);
        result &= ((question.FrontFogLamp == 1) == FrontFogLamp);
        result &= ((question.RearFogLamp == 1) == RearFogLamp);
        result &= ((question.LeftIndicator == 1) == LeftIndicator);
        result &= ((question.RightIndicator == 1) == RightIndicator);
        result &= (question.LowToHigLight == lowToHigCount);

        Debug.Log("DoubleJumpLamp:" + DoubleJumpLamp + "\nClearanceLamp:" + ClearanceLamp + "\nLowBeamLight:" + LowBeamLight + "\nHigBeamLight:" + HigBeamLight + "\nFrontFogLamp:" + FrontFogLamp + "\nRearFogLamp:" + RearFogLamp + "\nlowToHigCount:" + lowToHigCount);

        Debug.Log("DoubleJumpLamp:" + question.DoubleJumpLamp + "\nClearanceLamp:" + question.ClearAnceLamp + "\nLowBeamLight:" + question.LowBeamLight + "\nHigBeamLight:" + question.HigBeamLight + "\nFrontFogLamp:" + question.FrontFogLamp + "\nRearFogLamp:" + question.RearFogLamp + "\nlowToHigCount:" + question.LowToHigLight);

        return result;
    }

    //#endif
}

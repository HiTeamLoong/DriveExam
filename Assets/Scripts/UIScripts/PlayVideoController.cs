using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayVideoController : MonoBehaviour
{

    public VideoPlayer videoPlayer;      //播放控件

    public Button btnStart;     //开始播放
    public GameObject controlList;  //操作列表
    public Button btnVideoList; //播放列表控制
    public Slider sliderProg;   //播放进度控制
    public Button btnPlay;      //播放状态控制
    public Button btnScreen;    //是否全屏

    public Sprite sprPlay;      //播放按钮图片
    public Sprite sprPause;     //暂停按钮图片

    public Text textPlayTime;   //播放时间
    public Text textTotalTime;  //总长时间

    public RectTransform videoParent;    //播放节点父节点
    public RectTransform videoChild;     //播放控件子节点  
    public GameObject objVideoList;         //列表父节点显隐
    public VideoListItem videoListItem;     //预制，用于创建 

    private RenderTexture videoRender;
    private bool showList = true;
    private bool isScreen = false;

    private bool setProg = false;
    private bool isChange = false;

    public bool IsPlay
    {
        get { return videoPlayer.isPlaying; }
        set
        {
            if (videoPlayer.isPrepared)
            {
                if (videoPlayer.isPlaying && !value)
                {
                    videoPlayer.Pause();
                    (btnPlay.targetGraphic as Image).sprite = sprPlay;
                }
                else if (!videoPlayer.isPlaying && value)
                {
                    videoPlayer.Play();
                    (btnPlay.targetGraphic as Image).sprite = sprPause;
                }
            }
        }
    }


    private void Start()
    {
        btnStart.onClick.AddListener(OnClickStart);
        btnVideoList.onClick.AddListener(OnClickVideoList);
        btnPlay.onClick.AddListener(OnClickPlay);
        btnScreen.onClick.AddListener(OnClickScreen);

        sliderProg.onValueChanged.AddListener(OnSliderChange);
        UIEventListener.Get(sliderProg.gameObject).onDown += (go) =>
        {
            setProg = true;
            if (IsPlay)
            {
                IsPlay = false;
                isChange = true;
            }
            OnSliderChange(sliderProg.value);
        };
        UIEventListener.Get(sliderProg.gameObject).onUp += (go) =>
        {
            setProg = false;
            if (isChange)
            {
                isChange = false;
                IsPlay = true;
            }
        };

        videoPlayer.prepareCompleted += prepareCompleted;
        videoPlayer.frameDropped += frameDropped;
        videoPlayer.frameReady += frameReady;
        videoPlayer.loopPointReached += loopPointReached;

        sliderProg.maxValue = 100f;
        sliderProg.minValue = 0.0f;

    }

    private void Update()
    {
        if (videoPlayer.isPrepared && !setProg && IsPlay)
        {
            sliderProg.value = (float)videoPlayer.time;
        }
    }

    public void InitWith(List<VideoData> videoList)
    {
        videoRender = new RenderTexture(1280, 720, 24);
        videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        videoPlayer.targetTexture = videoRender;
        videoPlayer.isLooping = true;

        for (int i = 0; i < videoList.Count; i++)
        {
            GameObject go = Instantiate(videoListItem.gameObject, videoListItem.transform.parent);
            VideoListItem videoItem = go.GetComponent<VideoListItem>();
            videoItem.InitWith(videoList[i], OnClickVideoItem);
        }
        videoListItem.gameObject.SetActive(false);
        SetVideoPlayData(videoList[0]);
    }

    void SetVideoPlayData(VideoData videoData)
    {
        controlList.SetActive(false);
        videoPlayer.GetComponent<RawImage>().texture = ResourcesMgr.Instance.GetTextureWithName(videoData.imgurl);
        videoPlayer.url = videoData.videourl;
        videoPlayer.Prepare();
    }

    void OnClickVideoItem(VideoData videoData)
    {
        btnStart.gameObject.SetActive(true);

        sliderProg.interactable = false;
        btnPlay.interactable = false;
        SetVideoPlayData(videoData);
    }

    void prepareCompleted(VideoPlayer source)
    {
        sliderProg.interactable = true;
        btnPlay.interactable = true;

        sliderProg.minValue = 0;
        sliderProg.maxValue = source.frameCount / source.frameRate;

        int time = Mathf.CeilToInt(sliderProg.maxValue);
        int minute = time / 60;
        int second = time % 60;
        textTotalTime.text = string.Format("{0:D2}:{1:D2}", minute.ToString(), second.ToString());
        if (videoPlayer.isPlaying)
        {
            controlList.SetActive(true);
        }
    }
    void frameDropped(VideoPlayer source)
    {
        Debug.Log("卡住");
    }
    void frameReady(VideoPlayer source, long frameIdx)
    {
        Debug.Log("卡住");
    }
    void loopPointReached(VideoPlayer source)
    {
        IsPlay = false;
    }

    void OnClickStart()
    {
        btnStart.gameObject.SetActive(false);
        videoPlayer.Play();
        videoPlayer.GetComponent<RawImage>().texture = videoRender;
        if (videoPlayer.isPrepared)
        {
            controlList.SetActive(true);
        }
    }

    /// <summary>
    /// Show or Hide VideoList
    /// </summary>
    void OnClickVideoList()
    {
        showList = !showList;
        objVideoList.SetActive(showList);
    }

    void OnSliderChange(float value)
    {
        if (videoPlayer.isPrepared && setProg)
        {
            videoPlayer.time = (long)value;
        }
        int time = (int)value;
        int minute = time / 60;
        int second = time % 60;
        textPlayTime.text = string.Format("{0:D2}:{1:D2}", minute.ToString(), second.ToString());
    }

    void OnClickPlay()
    {
        IsPlay = !IsPlay;
    }

    /// <summary>
    /// 全屏显示
    /// </summary>
    void OnClickScreen()
    {
        float rate = 1f;
        if (isScreen)
        {
            isScreen = false;
            videoChild.SetParent(videoParent);
            videoChild.anchoredPosition = Vector2.zero;
            videoChild.anchorMin = Vector2.zero;
            videoChild.anchorMax = Vector2.one;
            videoChild.sizeDelta = Vector2.zero;

            //Debug.Log(new Vector2(videoParent.rect.width, videoParent.rect.height)+"   "+ videoChild.sizeDelta);
            rate = videoParent.rect.height / (videoPlayer.transform as RectTransform).sizeDelta.y;
        }
        else
        {
            isScreen = true;
            videoChild.SetParent(transform.parent);
            videoChild.anchoredPosition = Vector2.zero;
            videoChild.sizeDelta = (transform.parent as RectTransform).sizeDelta;

            Debug.Log((transform.parent as RectTransform).sizeDelta + "   " + (videoPlayer.transform as RectTransform).sizeDelta);
            rate = (transform.parent as RectTransform).rect.height / (videoPlayer.transform as RectTransform).sizeDelta.y;
        }

        Vector2 renderSize = (videoPlayer.transform as RectTransform).sizeDelta;
        (videoPlayer.transform as RectTransform).sizeDelta = renderSize * rate;
    }
}

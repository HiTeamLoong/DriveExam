using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ResourcesMgr : Singleton<ResourcesMgr>
{

    public static string ConfigPath
    {
        get
        {
#if UNITY_EDITOR
            string configPath = Path.Combine(Application.dataPath, "Temp/config");
#else
            string configPath = Path.Combine(Application.persistentDataPath, "config");
#endif
            if (!Directory.Exists(configPath))
            {
                Directory.CreateDirectory(configPath);
            }
            return configPath;
        }
    }
    public static string AudioPath
    {
        get
        {
#if UNITY_EDITOR
            string audioPath = Path.Combine(Application.dataPath, "Temp/audio");
#else
            string audioPath = Path.Combine(Application.persistentDataPath, "audio");
#endif
            if (!Directory.Exists(audioPath))
            {
                Directory.CreateDirectory(audioPath);
            }
            return audioPath;
        }
    }
    public static string TexturePath
    {
        get
        {
#if UNITY_EDITOR
            string audioPath = Path.Combine(Application.dataPath, "Temp/texture");
#else
            string audioPath = Path.Combine(Application.persistentDataPath, "texture");
#endif
            if (!Directory.Exists(audioPath))
            {
                Directory.CreateDirectory(audioPath);
            }
            return audioPath;
        }
    }
    /// <summary>
    /// Loads the UI Prefab.
    /// </summary>
    /// <returns>The UIP refab.</returns>
    /// <param name="assetName">Asset name.</param>
    public GameObject LoadUIPrefab(string assetName)
    {
        return Resources.Load("Prefab/"+assetName) as GameObject;
    }

    public AudioClip LoadAudioClip(string clipName)
    {
        return Resources.Load<AudioClip>("Sound/" + clipName);
    }

    /// <summary>
    /// Gets the audio with string.
    /// </summary>
    /// <returns>The audio with string.</returns>
    /// <param name="content">Content.</param>
    public AudioClip GetAudioWithStr(string content)
    {
        string fileName = ConfigDataMgr.Instance.resourceDict[content];
        byte[] data = File.ReadAllBytes(Path.Combine(AudioPath, fileName));
        AudioClip audioClip = Util.GetAudioClipFromMP3ByteArray(data);
        return audioClip;
    }
    /// <summary>
    /// 通过url读取语音文件
    /// </summary>
    /// <returns>The audio with URL.</returns>
    /// <param name="audioUrl">Audio URL.</param>
    public AudioClip GetAudioWithURL(string audioUrl)
    {
        string fileName = ConfigDataMgr.Instance.resourceDict[audioUrl];
        byte[] data = File.ReadAllBytes(Path.Combine(AudioPath, fileName));
        //AudioClip audioClip = WavUtility.ToAudioClip(data);
        AudioClip audioClip = Util.GetAudioClipFromMP3ByteArray(data);
        return audioClip;

    }
    public void WriteAudioFile(string fileName, byte[] data)
    {
        File.WriteAllBytes(Path.Combine(AudioPath, fileName), data);
    }

    /// <summary>
    /// Gets the name of the texture2D with.
    /// </summary>
    /// <returns>The texture with name.</returns>
    /// <param name="fileName">File name.</param>
    public Texture2D GetTextureWithName(string imgurl)
    {
        string fileName = ConfigDataMgr.Instance.resourceDict[imgurl];
        byte[] data = File.ReadAllBytes(Path.Combine(TexturePath, fileName));
        Texture2D texture2D = new Texture2D(0, 0, TextureFormat.ARGB32, false);
        texture2D.LoadImage(data);
        return texture2D;
    }
    public void WriteTextureFile(string fileName, byte[] data)
    {
        File.WriteAllBytes(Path.Combine(TexturePath, fileName), data);
    }

    public void AsyncLoadTextureWithName(string imgurl, Callback<Texture2D> callback)
    {
        if (!ConfigDataMgr.Instance.resourceDict.ContainsKey(imgurl))
        {
            LoadNetworkFile(imgurl, (result, data) =>
            {
                if (result)
                {
                    Texture2D texture2D = new Texture2D(0, 0, TextureFormat.ARGB32, false);
                    texture2D.LoadImage(data);
                    callback(texture2D);

                    if (!ConfigDataMgr.Instance.resourceDict.ContainsKey(imgurl))
                    {
                        string fileName = System.Guid.NewGuid().ToString("N");
                        WriteTextureFile(fileName, data);

                        ConfigDataMgr.Instance.resourceDict.Add(imgurl, fileName);
                        ConfigDataMgr.Instance.WriteResourceDictData();
                    }
                }
                else
                {
                    callback(null);
                }
            });
        }
        else
        {
            callback(GetTextureWithName(imgurl));
        }

    }

    /// <summary>
    /// 加载网络资源
    /// </summary>
    /// <typeparam name="T">文件类型</typeparam>
    /// <param name="fileUrl">文件URL</param>
    /// <param name="callback">完成回调</param>
    public void LoadNetworkFile<T>(string fileUrl, Callback<bool, T> callback) where T : Object
    {
        GlobalManager.Instance.RequestNetworkFile(fileUrl, (result, text, data) =>
        {
            if (result)
            {
                if (typeof(Texture2D).IsAssignableFrom(typeof(T)))
                {
                    Texture2D texture2D = new Texture2D(0, 0, TextureFormat.ARGB32, false);
                    texture2D.LoadImage(data);
                    callback(result, texture2D as T);
                }
                else
                {
                    callback(result, null);
                }
            }
            else
            {
                callback(result, default(T));
            }
        });
    }
    public void LoadNetworkFile(string fileUrl, Callback<bool, byte[]> callback)
    {
        GlobalManager.Instance.RequestNetworkFile(fileUrl, (result, text, data) =>
        {
            if (result)
            {
                callback(result, data);
            }
            else
            {
                callback(result, null);
            }
        });
    }



    private Callback<bool, float> downloadCallback;
    private List<string> downloadList;
    private int downloadIndex = 0;

    /// <summary>
    /// 资源下载
    /// </summary>
    /// <returns>The question audio.</returns>
    /// <param name="gameConfig">Game config.</param>
    public void DownLoadAudioResource(GameConfig gameConfig, Callback<bool, float> callback)
    {
        downloadCallback = callback;
        downloadList = new List<string>();
        downloadIndex = 0;
        //检查开始灯光考试的语音需要更新
        ExamTipData examTip = gameConfig.examtip_old;
        if (!ConfigDataMgr.Instance.resourceDict.ContainsKey(examTip.exam_audio))
        {
            downloadList.Add(examTip.exam_audio);
        }
        if (!string.IsNullOrEmpty(examTip.broadcast_end)&&!ConfigDataMgr.Instance.resourceDict.ContainsKey(examTip.broadcast_end))
        {
            downloadList.Add(examTip.broadcast_end);
        }
        //检查开始灯光考试的语音需要更新
        examTip = gameConfig.examtip_new;
        if (!ConfigDataMgr.Instance.resourceDict.ContainsKey(examTip.exam_audio))
        {
            downloadList.Add(examTip.exam_audio);
        }
        if (!string.IsNullOrEmpty(examTip.broadcast_end) && !ConfigDataMgr.Instance.resourceDict.ContainsKey(examTip.broadcast_end))
        {
            downloadList.Add(examTip.broadcast_end);
        }

        //检查试题的语音是否需要更新
        foreach (var item in gameConfig.questions)
        {
            QuestionData questionData = item.Value;
            if (!ConfigDataMgr.Instance.resourceDict.ContainsKey(questionData.audio))
            {
                downloadList.Add(questionData.audio);
            }
        }
        if (downloadList.Count <= 0)
        {
            downloadCallback(true, 1f);
        }
        else
        {
            DownLoadFile();
        }
    }

    private void DownLoadFile()
    {
        if (downloadIndex < downloadList.Count)
        {
            string fileUrl = downloadList[downloadIndex];
            LoadNetworkFile(fileUrl, (result, data) =>
            {
                if (result)
                {
                    string fileName = System.Guid.NewGuid().ToString("N");
                    ResourcesMgr.Instance.WriteAudioFile(fileName, data);
                    ConfigDataMgr.Instance.resourceDict.Add(fileUrl, fileName);
                    ConfigDataMgr.Instance.WriteResourceDictData();
                    downloadIndex += 1;
                    downloadCallback(result, (float)downloadIndex / downloadList.Count);
                    if (downloadIndex < downloadList.Count)
                    {
                        DownLoadFile();
                    }
                }
                else
                {
                    downloadCallback(result, (float)downloadIndex / downloadList.Count);
                }
                Debug.Log("result:" + result + "\t" + "prog:" + (float)downloadIndex / downloadList.Count);
            });
        }
         }
}

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
    public static string TexturePath{
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
    public GameObject LoadUIPrefab(string assetName){
        return Resources.Load(assetName) as GameObject;
    }

    public AudioClip LoadAudioClip(string clipName){
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
    public void WriteTextureFile(string fileName,byte[] data)
    {
        File.WriteAllBytes(Path.Combine(TexturePath, fileName), data);
    }

    /// <summary>
    /// 加载网络资源
    /// </summary>
    /// <typeparam name="T">文件类型</typeparam>
    /// <param name="fileUrl">文件URL</param>
    /// <param name="callback">完成回调</param>
    public void LoadNetworkFile<T>(string fileUrl,Callback<bool,T> callback)where T:Object
    {
        GlobalManager.Instance.RequestNetworkFile(fileUrl, (result, text, data) =>
        {
            if (result)
            {
                if(typeof(Texture2D).IsAssignableFrom(typeof(T)))
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
}

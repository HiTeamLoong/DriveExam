using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ConfigDataMgr : XSingleton<ConfigDataMgr>
{
    public static string ExamStartTip = "下面将进行模拟夜间灯光的考试，请在语音播报叮一声结束后5秒内完成操作";
    public static QuestionData ExamEnd = new QuestionData() { question = "模拟夜间考试完成请关闭所有灯光", answer = "答案：关闭所有灯光" };

    public GameConfig gameConfig = new GameConfig();
    public AuthorizeData authorizeData = new AuthorizeData();
    public Dictionary<string, QuestionData> questions { get { return gameConfig.questions; } }
    public ShareData shareData { get { return gameConfig.share; } }
    public Dictionary<string, string> resourceDict = new Dictionary<string, string>();

    public override void OnInit()
    {
        base.OnInit();
        ReadGameConfigData();
        ReadAudioDictData();
    }

    /// <summary>
    /// Reads the game config data from gameConfig.json.
    /// </summary>
    public void ReadGameConfigData()
    {
        string filePath = Path.Combine(ResourcesMgr.ConfigPath, "gameConfig.json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            gameConfig = LitJson.JsonMapper.ToObject<GameConfig>(json);
        }
        else
        {
            gameConfig = null;
        }
    }
    /// <summary>
    /// Writes the game config data to gameConfig.json.
    /// </summary>
    public void WriteGameConfigData()
    {
        string filePath = Path.Combine(ResourcesMgr.ConfigPath, "gameConfig.json");
        string json = LitJson.JsonMapper.ToJson(gameConfig);
        File.WriteAllText(filePath, json, System.Text.Encoding.UTF8);
    }

    /// <summary>
    /// Reads the audio dict data from audioDict.json.
    /// </summary>
    public void ReadAudioDictData()
    {
        string filePath = Path.Combine(ResourcesMgr.ConfigPath, "resourceDict.json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            resourceDict = LitJson.JsonMapper.ToObject<Dictionary<string, string>>(json);
        }
    }
    /// <summary>
    /// Writes the audio dict data to audioDict.json.
    /// </summary>
    public void WriteAudioDictData()
    {
        string filePath = Path.Combine(ResourcesMgr.ConfigPath, "resourceDict.json");
        string json = LitJson.JsonMapper.ToJson(resourceDict);
        File.WriteAllText(filePath, json, System.Text.Encoding.UTF8);
    }

    public void CheckTurnAudio(string text)
    {

    }


    public QuestionData GetQuestionByIndex(int index)
    {
        //if (index >= 0 && index < questions.Count)
        //{
        //    return questions[index];
        //}
        //else
        //{
        //    Debug.LogError("question index error");
        //    return null;
        //}
        return null;
    }
    public QuestionData GetQuestionByIndex(string index)
    {
        if (questions.ContainsKey(index))
        {
            return questions[index];
        }
        else
        {
            return null;
        }
    }

}

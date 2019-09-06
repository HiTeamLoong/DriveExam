using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using Wit.BaiduAip.Speech;

public class UpdateManager : MonoBehaviour
{
    public string APIKey = "WondCiSwY0RzHYc7hGS2bFoc";
    public string SecretKey = "NjaCdrsUKPi9xiB6X6F6T46B2TdT8ZcT";

    [HideInInspector]
    public UILoginWindow uiLoginWindow;
    public Tts ttsString2Audio;
    private void Awake()
    {

        //string test = "{\n  \"msg\": \"请求成功\",\n  \"data\": {\n    \"exam\": {\n    },\n    \"listvideo\": [\n      {\n        \"imgurl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight002.jpg\",\n        \"chexingId\": 1,\n        \"videourl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight002.mp4\",\n        \"videoname\": \"null\"\n      },\n      {\n        \"imgurl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight001.jpg\",\n        \"chexingId\": 1,\n        \"videourl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight001.mp4\",\n        \"videoname\": \"2001709210000032101.mp4\"\n      }\n    ],\n    \"listnewold\": [\n      {\n        \"examaudio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/start_new.mp3\",\n        \"uid\": 2,\n        \"broadcastend\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/ding.mp3\",\n        \"addTime\": \"2019-06-15 12:03:19\",\n        \"examtip\": \"下面将进行模拟夜间灯光的考试，请在语音播报叮一声结束后5秒内完成操作\",\n        \"type\": \"1\"\n      },\n      {\n        \"examaudio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/start_old.mp3\",\n        \"uid\": 1,\n        \"broadcastend\": \"http://jiakaojingling.oss-cn-qingdao.aliyuncs.com/static/upload/driver/90041560756849403.png\",\n        \"addTime\": \"2019-06-15 12:03:03\",\n        \"examtip\": \"下面将进行模拟夜间行驶场景灯光使用的考试，请按语音指令在5秒内做出相应的灯光操作\",\n        \"type\": \"2\"\n      }\n    ],\n    \"questions\": [\n      {\n        \"uid\": 1,\n        \"question\": \"请开启前照灯\",\n        \"answer\": \"答案：开启大灯，注意是近光\",\n        \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/1.mp3\",\n        \"DoubleJumpLamp\": 0,\n        \"ClearAnceLamp\": 1,\n        \"LowBeamLight\": 1,\n        \"HigBeamLight\": 0,\n        \"FrontFogLamp\": 0,\n        \"RearFogLamp\": 0,\n        \"LeftIndicator\": 0,\n        \"RightIndicator\": 0,\n        \"LowToHigLight\": 0\n      },\n      {\n        \"uid\": 2,\n        \"question\": \"夜间超越前方车辆\",\n        \"answer\": \"答案：变换远近光灯，完成操作后仍为近光\",\n        \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/2.mp3\",\n        \"DoubleJumpLamp\": 0,\n        \"ClearAnceLamp\": 1,\n        \"LowBeamLight\": 1,\n        \"HigBeamLight\": 0,\n        \"FrontFogLamp\": 0,\n        \"RearFogLamp\": 0,\n        \"LeftIndicator\": 0,\n        \"RightIndicator\": 0,\n        \"LowToHigLight\": 2\n      },\n      {\n        \"uid\": 3,\n        \"question\": \"夜间通过急弯\",\n        \"answer\": \"答案：远近光交替\",\n        \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/3.mp3\",\n        \"DoubleJumpLamp\": 0,\n        \"ClearAnceLamp\": 1,\n        \"LowBeamLight\": 1,\n        \"HigBeamLight\": 0,\n        \"FrontFogLamp\": 0,\n        \"RearFogLamp\": 0,\n        \"LeftIndicator\": 0,\n        \"RightIndicator\": 0,\n        \"LowToHigLight\": 2\n      },\n      {\n        \"uid\": 4,\n        \"question\": \"夜间通过拱桥\",\n        \"answer\": \"答案：变换远近光灯，完成操作后仍为近光\",\n        \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/4.mp3\",\n        \"DoubleJumpLamp\": 0,\n        \"ClearAnceLamp\": 1,\n        \"LowBeamLight\": 1,\n        \"HigBeamLight\": 0,\n        \"FrontFogLamp\": 0,\n        \"RearFogLamp\": 0,\n        \"LeftIndicator\": 0,\n        \"RightIndicator\": 0,\n        \"LowToHigLight\": 2\n      },\n      {\n        \"uid\": 9,\n        \"question\": \"夜间同方向近距离跟车行驶\",\n        \"answer\": \"答案：开启近光灯\",\n        \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/9.mp3\",\n        \"DoubleJumpLamp\": 0,\n        \"ClearAnceLamp\": 1,\n        \"LowBeamLight\": 1,\n        \"HigBeamLight\": 0,\n        \"FrontFogLamp\": 0,\n        \"RearFogLamp\": 0,\n        \"LeftIndicator\": 0,\n        \"RightIndicator\": 0,\n        \"LowToHigLight\": 0\n      },\n      {\n        \"uid\": 10,\n        \"question\": \"夜间在窄路、窄桥与机动车会车\",\n        \"answer\": \"答案：开启近光灯\",\n        \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/10.mp3\",\n        \"DoubleJumpLamp\": 0,\n        \"ClearAnceLamp\": 1,\n        \"LowBeamLight\": 1,\n        \"HigBeamLight\": 0,\n        \"FrontFogLamp\": 0,\n        \"RearFogLamp\": 0,\n        \"LeftIndicator\": 0,\n        \"RightIndicator\": 0,\n        \"LowToHigLight\": 0\n      },\n      {\n        \"uid\": 11,\n        \"question\": \"夜间机动车会车\",\n        \"answer\": \"答案：开启近光灯\",\n        \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/11.mp3\",\n        \"DoubleJumpLamp\": 0,\n        \"ClearAnceLamp\": 1,\n        \"LowBeamLight\": 1,\n        \"HigBeamLight\": 0,\n        \"FrontFogLamp\": 0,\n        \"RearFogLamp\": 0,\n        \"LeftIndicator\": 0,\n        \"RightIndicator\": 0,\n        \"LowToHigLight\": 0\n      },\n      {\n        \"uid\": 15,\n        \"question\": \"夜间在道路上发生事故，妨碍交通又难以移动\",\n        \"answer\": \"答案：开启示宽灯和危险警示灯\",\n        \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/15.mp3\",\n        \"DoubleJumpLamp\": 1,\n        \"ClearAnceLamp\": 1,\n        \"LowBeamLight\": 0,\n        \"HigBeamLight\": 0,\n        \"FrontFogLamp\": 0,\n        \"RearFogLamp\": 0,\n        \"LeftIndicator\": 0,\n        \"RightIndicator\": 0,\n        \"LowToHigLight\": 0\n      },\n      {\n        \"uid\": 0,\n        \"question\": \"模拟夜间考试完成请关闭所有灯光\",\n        \"answer\": \"答案：关闭所有灯光\",\n        \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/end.mp3\",\n        \"DoubleJumpLamp\": 0,\n        \"ClearAnceLamp\": 0,\n        \"LowBeamLight\": 0,\n        \"HigBeamLight\": 0,\n        \"FrontFogLamp\": 0,\n        \"RearFogLamp\": 0,\n        \"LeftIndicator\": 0,\n        \"RightIndicator\": 0,\n        \"LowToHigLight\": 0\n      }\n    ],\n    \"suiji\": [\n      {\n        \"uid\": 2\n      },\n      {\n        \"uid\": 3\n      },\n      {\n        \"uid\": 4\n      },\n      {\n        \"uid\": 11\n      },\n      {\n        \"uid\": 15\n      }\n    ]\n  },\n  \"status\": \"200\"\n}";
        //string test = "{\n  \"msg\": \"请求成功\",\n  \"data\": {\n    \"exam\": {\n    },\n    \"listvideo\": [\n      {\n        \"imgurl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight002.jpg\",\n        \"chexingId\": 1,\n        \"videourl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight002.mp4\",\n        \"videoname\": \"null\"\n      },\n      {\n        \"imgurl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight001.jpg\",\n        \"chexingId\": 1,\n        \"videourl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight001.mp4\",\n        \"videoname\": \"2001709210000032101.mp4\"\n      }\n    ],\n    \"listnewold\": [\n      {\n        \"examaudio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/start_new.mp3\",\n        \"uid\": 2,\n        \"broadcastend\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/ding.mp3\",\n        \"addTime\": \"2019-06-15 12:03:19\",\n        \"examtip\": \"下面将进行模拟夜间灯光的考试，请在语音播报叮一声结束后5秒内完成操作\",\n        \"type\": \"1\"\n      },\n      {\n        \"examaudio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/start_old.mp3\",\n        \"uid\": 1,\n        \"broadcastend\": \"http://jiakaojingling.oss-cn-qingdao.aliyuncs.com/static/upload/driver/90041560756849403.png\",\n        \"addTime\": \"2019-06-15 12:03:03\",\n        \"examtip\": \"下面将进行模拟夜间行驶场景灯光使用的考试，请按语音指令在5秒内做出相应的灯光操作\",\n        \"type\": \"2\"\n      }\n    ],\n    \"suiji\": [\n      {\n        \"uid\": 2\n      },\n      {\n        \"uid\": 3\n      },\n      {\n        \"uid\": 4\n      },\n      {\n        \"uid\": 11\n      },\n      {\n        \"uid\": 15\n      }\n    ]\n  },\n  \"status\": \"200\"\n}";
        //string test = "{\n  \"msg\": \"请求成功\",\n  \"data\": {\n    \"exam\": {\n    },\n    \"listvideo\": [\n      {\n        \"imgurl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight002.jpg\",\n        \"chexingId\": 1,\n        \"videourl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight002.mp4\",\n        \"videoname\": \"null\"\n      },\n      {\n        \"imgurl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight001.jpg\",\n        \"chexingId\": 1,\n        \"videourl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight001.mp4\",\n        \"videoname\": \"2001709210000032101.mp4\"\n      }\n    ],\n    \"suiji\": [\n      {\n        \"uid\": 2\n      },\n      {\n        \"uid\": 3\n      },\n      {\n        \"uid\": 4\n      },\n      {\n        \"uid\": 11\n      },\n      {\n        \"uid\": 15\n      }\n    ]\n  },\n  \"status\": \"200\"\n}";
        //string test = "{\n\"msg\": \"请求成功\",\n\"data\": {\n\"exam\": {\n\"0\": [\n{\n\"timuid\": 1\n},\n{\n\"timuid\": 1\n},\n{\n                    \"timuid\": 2\n                },\n                {\n                    \"timuid\": 1\n                }\n            ],\n            \"1\": [\n                {\n                    \"timuid\": 1\n                },\n                {\n                    \"timuid\": 1\n                },\n                {\n                    \"timuid\": 1\n                },\n                {\n                    \"timuid\": 1\n                }\n            ]\n        },\n        \"listvideo\": [\n            {\n                \"imgurl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight002.jpg\",\n                \"chexingId\": 1,\n                \"videourl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight002.mp4\",\n                \"videoname\": \"null\"\n            },\n            {\n                \"imgurl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight001.jpg\",\n                \"chexingId\": 1,\n                \"videourl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight001.mp4\",\n                \"videoname\": \"2001709210000032101.mp4\"\n            }\n        ],\n        \"listnewold\": [\n            {\n                \"examaudio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/start_new.mp3\",\n                \"uid\": 2,\n                \"broadcastend\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/ding.mp3\",\n                \"addTime\": \"2019-06-15 12:03:19\",\n                \"examtip\": \"下面将进行模拟夜间灯光的考试，请在语音播报叮一声结束后5秒内完成操作\",\n                \"type\": \"1\"\n            },\n            {\n                \"examaudio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/start_old.mp3\",\n                \"uid\": 1,\n                \"broadcastend\": \"http://jiakaojingling.oss-cn-qingdao.aliyuncs.com/static/upload/driver/90041560756849403.png\",\n                \"addTime\": \"2019-06-15 12:03:03\",\n                \"examtip\": \"下面将进行模拟夜间行驶场景灯光使用的考试，请按语音指令在5秒内做出相应的灯光操作\",\n                \"type\": \"2\"\n            }\n        ],\n        \"questions\": [\n            {\n                \"RightIndicator\": 0,\n                \"question\": \"请开启前照灯\",\n                \"DoubleJumpLamp\": 0,\n                \"HigBeamLight\": 0,\n                \"LeftIndicator\": 0,\n                \"FrontFogLamp\": 0,\n                \"RearFogLamp\": 0,\n                \"uid\": 1,\n                \"LowBeamLight\": 1,\n                \"answer\": \"答案：开启大灯，注意是近光\",\n                \"ClearAnceLamp\": 1,\n                \"LowToHigLight\": 0,\n                \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/1.mp3\"\n            },\n            {\n                \"RightIndicator\": 0,\n                \"question\": \"夜间超越前方车辆\",\n                \"DoubleJumpLamp\": 0,\n                \"HigBeamLight\": 0,\n                \"LeftIndicator\": 0,\n                \"FrontFogLamp\": 0,\n                \"RearFogLamp\": 0,\n                \"uid\": 2,\n                \"LowBeamLight\": 1,\n                \"answer\": \"答案：变换远近光灯，完成操作后仍为近光\",\n                \"ClearAnceLamp\": 1,\n                \"LowToHigLight\": 1,\n                \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/2.mp3\"\n            }\n        ],\n        \"suiji\": [\n            {\n                \"uid\": 1\n            },\n            {\n                \"uid\": 2\n            }\n        ]\n    },\n    \"status\": \"200\"\n}";

 //       string test = "{\n  \"msg\": \"请求成功\",\n  \"data\": {\n    \"exam\": {\n      \"0\": [\n        {\n          \"timuid\": 1\n        },\n        {\n          \"timuid\": 2\n        },\n        {\n          \"timuid\": 11\n        },\n        {\n          \"timuid\": 5\n        },\n        {\n          \"timuid\": 0\n        }\n      ],\n      \"1\": [\n        {\n          \"timuid\": 1\n        },\n        {\n          \"timuid\": 3\n        },\n        {\n          \"timuid\": 4\n        },\n        {\n          \"timuid\": 15\n        },\n        {\n          \"timuid\": 0\n        }\n      ]\n    },\n    \"listvideo\": [\n      {\n        \"imgurl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight002.jpg\",\n        \"chexingId\": 1,\n        \"videourl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight002.mp4\",\n        \"videoname\": \"null\"\n      },\n      {\n        \"imgurl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight001.jpg\",\n        \"chexingId\": 1,\n        \"videourl\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/video/DZlight/DZlight001.mp4\",\n        \"videoname\": \"2001709210000032101.mp4\"\n      }\n    ],\n    \"listnewold\": [\n      {\n        \"examaudio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/start_new.mp3\",\n        \"uid\": 2,\n        \"broadcastend\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/ding.mp3\",\n        \"addTime\": \"2019-06-15 12:03:19\",\n        \"examtip\": \"下面将进行模拟夜间灯光的考试，请在语音播报叮一声结束后5秒内完成操作\",\n        \"type\": \"1\"\n      },\n      {\n        \"examaudio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/start_old.mp3\",\n        \"uid\": 1,\n        \"broadcastend\": \"http://jiakaojingling.oss-cn-qingdao.aliyuncs.com/static/upload/driver/90041560756849403.png\",\n        \"addTime\": \"2019-06-15 12:03:03\",\n        \"examtip\": \"下面将进行模拟夜间行驶场景灯光使用的考试，请按语音指令在5秒内做出相应的灯光操作\",\n        \"type\": \"2\"\n      }\n    ],\n    \"questions\": [\n      {\n        \"uid\": 1,\n        \"question\": \"请开启前照灯\",\n        \"answer\": \"答案：开启大灯，注意是近光\",\n        \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/1.mp3\",\n        \"DoubleJumpLamp\": 0,\n        \"ClearAnceLamp\": 1,\n        \"LowBeamLight\": 1,\n        \"HigBeamLight\": 0,\n        \"FrontFogLamp\": 0,\n        \"RearFogLamp\": 0,\n        \"LeftIndicator\": 0,\n        \"RightIndicator\": 0,\n        \"LowToHigLight\": 0\n      },\n      {\n        \"uid\": 2,\n        \"question\": \"夜间超越前方车辆\",\n        \"answer\": \"答案：变换远近光灯，完成操作后仍为近光\",\n        \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/2.mp3\",\n        \"DoubleJumpLamp\": 0,\n        \"ClearAnceLamp\": 1,\n        \"LowBeamLight\": 1,\n        \"HigBeamLight\": 0,\n        \"FrontFogLamp\": 0,\n        \"RearFogLamp\": 0,\n        \"LeftIndicator\": 0,\n        \"RightIndicator\": 0,\n        \"LowToHigLight\": 2\n      },\n      {\n        \"uid\": 3,\n        \"question\": \"夜间通过急弯\",\n        \"answer\": \"答案：远近光交替\",\n        \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/3.mp3\",\n        \"DoubleJumpLamp\": 0,\n        \"ClearAnceLamp\": 1,\n        \"LowBeamLight\": 1,\n        \"HigBeamLight\": 0,\n        \"FrontFogLamp\": 0,\n        \"RearFogLamp\": 0,\n        \"LeftIndicator\": 0,\n        \"RightIndicator\": 0,\n        \"LowToHigLight\": 2\n      },\n      {\n        \"uid\": 4,\n        \"question\": \"夜间通过拱桥\",\n        \"answer\": \"答案：变换远近光灯，完成操作后仍为近光\",\n        \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/4.mp3\",\n        \"DoubleJumpLamp\": 0,\n        \"ClearAnceLamp\": 1,\n        \"LowBeamLight\": 1,\n        \"HigBeamLight\": 0,\n        \"FrontFogLamp\": 0,\n        \"RearFogLamp\": 0,\n        \"LeftIndicator\": 0,\n        \"RightIndicator\": 0,\n        \"LowToHigLight\": 2\n      },\n      {\n        \"uid\": 9,\n        \"question\": \"夜间同方向近距离跟车行驶\",\n        \"answer\": \"答案：开启近光灯\",\n        \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/9.mp3\",\n        \"DoubleJumpLamp\": 0,\n        \"ClearAnceLamp\": 1,\n        \"LowBeamLight\": 1,\n        \"HigBeamLight\": 0,\n        \"FrontFogLamp\": 0,\n        \"RearFogLamp\": 0,\n        \"LeftIndicator\": 0,\n        \"RightIndicator\": 0,\n        \"LowToHigLight\": 0\n      },\n      {\n        \"uid\": 10,\n        \"question\": \"夜间在窄路、窄桥与机动车会车\",\n        \"answer\": \"答案：开启近光灯\",\n        \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/10.mp3\",\n        \"DoubleJumpLamp\": 0,\n        \"ClearAnceLamp\": 1,\n        \"LowBeamLight\": 1,\n        \"HigBeamLight\": 0,\n        \"FrontFogLamp\": 0,\n        \"RearFogLamp\": 0,\n        \"LeftIndicator\": 0,\n        \"RightIndicator\": 0,\n        \"LowToHigLight\": 0\n      },\n      {\n        \"uid\": 11,\n        \"question\": \"夜间机动车会车\",\n        \"answer\": \"答案：开启近光灯\",\n        \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/11.mp3\",\n        \"DoubleJumpLamp\": 0,\n        \"ClearAnceLamp\": 1,\n        \"LowBeamLight\": 1,\n        \"HigBeamLight\": 0,\n        \"FrontFogLamp\": 0,\n        \"RearFogLamp\": 0,\n        \"LeftIndicator\": 0,\n        \"RightIndicator\": 0,\n        \"LowToHigLight\": 0\n      },\n      {\n        \"uid\": 15,\n        \"question\": \"夜间在道路上发生事故，妨碍交通又难以移动\",\n        \"answer\": \"答案：开启示宽灯和危险警示灯\",\n        \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/15.mp3\",\n        \"DoubleJumpLamp\": 1,\n        \"ClearAnceLamp\": 1,\n        \"LowBeamLight\": 0,\n        \"HigBeamLight\": 0,\n        \"FrontFogLamp\": 0,\n        \"RearFogLamp\": 0,\n        \"LeftIndicator\": 0,\n        \"RightIndicator\": 0,\n        \"LowToHigLight\": 0\n      },\n      {\n        \"uid\": 0,\n        \"question\": \"模拟夜间考试完成请关闭所有灯光\",\n        \"answer\": \"答案：关闭所有灯光\",\n        \"audio\": \"http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/2.x/2.0/audio/end.mp3\",\n        \"DoubleJumpLamp\": 0,\n        \"ClearAnceLamp\": 0,\n        \"LowBeamLight\": 0,\n        \"HigBeamLight\": 0,\n        \"FrontFogLamp\": 0,\n        \"RearFogLamp\": 0,\n        \"LeftIndicator\": 0,\n        \"RightIndicator\": 0,\n        \"LowToHigLight\": 0\n      }\n    ],\n    \"suiji\": [\n      {\n        \"uid\": 2\n      },\n      {\n        \"uid\": 3\n      },\n      {\n        \"uid\": 4\n      },\n      {\n        \"uid\": 11\n      },\n      {\n        \"uid\": 15\n      }\n    ]\n  },\n  \"status\": \"200\"\n}";

 //                   Debug.Log(test);


 //ResponseData<ResponseCarInfo> retObject = LitJson.JsonMapper.ToObject<ResponseData<ResponseCarInfo>>(test);

        //Debug.Log(retObject);



        ttsString2Audio = new Tts(APIKey, SecretKey);

        uiLoginWindow = UIManager.Instance.OpenUI<UILoginWindow>();
        uiLoginWindow.SetState("正在检查账号...");
        //#if CHAPTER_ONE
        //        //没网跳过检测版本
        //        if (ConfigDataMgr.Instance.gameConfig == null || Application.internetReachability != NetworkReachability.NotReachable)
        //        {
        //            CheckConfigUpdate();
        //CheckAuthorizeData();
        //        }
        //        else
        //        {
        //            CheckLoginState();
        //        }
        //#elif CHAPTER_TWO
        //if (Application.internetReachability != NetworkReachability.NotReachable)
        //{
        //    CheckLoginState();
        //}
        //else
        //{
        //    UIPrompDialog.ShowPromp(UIPrompDialog.PrompType.Confirm, "网络连接失败", "请连接网络后重试？", confirm =>
        //    {
        //        if (confirm)
        //        {
        //            Application.Quit();
        //        }
        //    });
        //}
        CheckAuthorizeData();
        UINetworkDialog.CheckNetwork(CheckLoginState);
//#endif
    }

    void CheckLoginState()
    {
//#if CHAPTER_ONE
//        if (GameDataMgr.Instance.ResponseLogin != null)
//        {
//            //看是否更新数据进行网络交互
//            UIManager.Instance.OpenUI<UIMainWindow>();
//        }
//        else
//        {
//            uiLoginWindow.SetLoginList();
//        }
//#elif CHAPTER_TWO
            uiLoginWindow.SetLoginList();
//#endif
    }

    /// <summary>
    /// Checks the gameConfig update.
    /// </summary>
    void CheckConfigUpdate()
    {
        //检查配置更新
        //string questionUrl = "http://localhost/LightExam/gameConfig.json";
        string questionUrl = "http://app.jiakaojingling.com/jkjl/static/dengguang/LightExam/gameConfig.json";
        //string questionUrl = "http://loongx.gz01.bdysite.com/LightExam/gameConfig.json";
        StartCoroutine(RequestNetworkFile(questionUrl, (result, content, data) =>
        {
            if (result)
            {
                GameVersion gameVersion = LitJson.JsonMapper.ToObject<GameVersion>(content);
                if (ConfigDataMgr.Instance.gameConfig != null && gameVersion.AppVersion != ConfigDataMgr.Instance.gameConfig.AppVersion)
                {
                    UIPrompDialog.ShowPromp(UIPrompDialog.PrompType.Confirm, "版本更新", "应用功能版本更新，请退出前往更新？", (confirm) =>
                    {
                        if (confirm)
                        {
                            Application.Quit();
                        }
                    });
                }
                else
                {
                    GameConfig gameConfig = LitJson.JsonMapper.ToObject<GameConfig>(content);
                    if (ConfigDataMgr.Instance.gameConfig == null || gameVersion.ResVersion != ConfigDataMgr.Instance.gameConfig.ResVersion)
                    {
                        StartCoroutine(UpdateResource(gameConfig));
                    }
                    else
                    {
                        CheckLoginState();
                    }
                }
            }
            else if (ConfigDataMgr.Instance.gameConfig != null)
            {
                CheckLoginState();
            }
            else
            {
                UITipsDialog.ShowTips("题库缺失，请链接网络后重新进入", true);
            }
        }));
    }

    void CheckAuthorizeData()
    {
        StartCoroutine(RequestNetworkFile(GlobalManager.Instance.loongAuthUrl, (result, content, data) =>
        {
            if (result)
            {
                try
                {
                    ConfigDataMgr.Instance.authorizeData = LitJson.JsonMapper.ToObject<AuthorizeData>(content);
                }
                catch
                {
                    ConfigDataMgr.Instance.authorizeData = new AuthorizeData();
                }
            }
        }));
    }

    /// <summary>
    /// 更新流程  1.转换语音  2.更新Video图片
    /// </summary>
    /// <returns>The resource.</returns>
    /// <param name="gameConfig">Game config.</param>
    IEnumerator UpdateResource(GameConfig gameConfig)
    {
        //yield return StartCoroutine(LoadQuestionAudio(gameConfig));
        //yield return StartCoroutine(TurnString2Audio(gameConfig));
        //yield return StartCoroutine(LoadVideoTexture(gameConfig));
        yield return null;
        //记录文件映射表
        //ConfigDataMgr.Instance.WriteResourceDictData();
        //更新题库数据
        ConfigDataMgr.Instance.gameConfig = gameConfig;
        ConfigDataMgr.Instance.WriteGameConfigData();

        //更新结束检测登录
        CheckLoginState();
    }
    /// <summary>
    /// 下载试题的语音
    /// </summary>
    /// <returns>The question audio.</returns>
    /// <param name="gameConfig">Game config.</param>
    IEnumerator LoadQuestionAudio(GameConfig gameConfig)
    {
        List<string> updateList = new List<string>();
        //检查开始灯光考试的语音需要更新
        if (!ConfigDataMgr.Instance.resourceDict.ContainsKey(gameConfig.examtip_old.exam_audio))
        {
            updateList.Add(gameConfig.examtip_old.exam_audio);
        }
        //检查开始灯光考试的语音需要更新
        if (!ConfigDataMgr.Instance.resourceDict.ContainsKey(gameConfig.examtip_new.exam_audio))
        {
            updateList.Add(gameConfig.examtip_new.exam_audio);
        }
        //检查试题的语音是否需要更新
        foreach (var item in gameConfig.questions)
        {
            cQuestionData questionData = item.Value;
            if (!ConfigDataMgr.Instance.resourceDict.ContainsKey(questionData.audio))
            {
                updateList.Add(questionData.audio);
            }
        }
        for (int i = 0; i < updateList.Count; i++)
        {
            //设置转换进度
            uiLoginWindow.SetProgress((float)i / updateList.Count);
            yield return RequestNetworkFile(updateList[i], (result, content, data) =>
            {
                if (result)
                {
                    string fileName = System.Guid.NewGuid().ToString("N");
                    ResourcesMgr.Instance.WriteAudioFile(fileName, data);
                    ConfigDataMgr.Instance.resourceDict.Add(updateList[i], fileName);
                }
            });
        }
        uiLoginWindow.SetProgress(1.0f);
    }


    /// <summary>
    /// 转换语音文件
    /// </summary>
    /// <returns>The string2 audio.</returns>
    /// <param name="gameConfig">Game config.</param>
    IEnumerator TurnString2Audio(GameConfig gameConfig)
    {
        List<string> updateList = new List<string>();

        //通用语音检测部分
        //CheckStringToAudio(ConfigDataMgr.ExamStartTip, updateList);
        //CheckStringToAudio(ConfigDataMgr.ExamEnd.question, updateList);
        //试题语音检测部分
        //for (int i = 0; i < gameConfig.questions.Count; i++)
        //{
        //    QuestionData questionData = gameConfig.questions[i];
        //    CheckStringToAudio(questionData.question, updateList);
        //}
        foreach (var item in gameConfig.questions)
        {
            cQuestionData questionData = item.Value;
            CheckStringToAudio(questionData.question, updateList);
        }

        for (int i = 0; i < updateList.Count; i++)
        {
            //设置转换进度
            uiLoginWindow.SetProgress((float)i / updateList.Count);
            yield return StartCoroutine(ttsString2Audio.Synthesis(updateList[i], result =>
            {
                if (result.Success)
                {
                    Debug.LogFormat("Trun Success：{0}", updateList[i]);
                    string fileName = System.Guid.NewGuid().ToString("N");
                    ResourcesMgr.Instance.WriteAudioFile(fileName, result.data);
                    ConfigDataMgr.Instance.resourceDict.Add(updateList[i], fileName);
                }
                else
                {
                    Debug.LogErrorFormat("Error:Str2Audio errorno<{0}> errormsg<{1}>", result.err_no, result.err_msg);
                }
            }));
        }
        uiLoginWindow.SetProgress(1.0f);
    }

    /// <summary>
    /// 下载视频缓冲图
    /// </summary>
    /// <returns>The video texture.</returns>
    /// <param name="gameConfig">Game config.</param>
    IEnumerator LoadVideoTexture(GameConfig gameConfig)
    {
        yield return null;

        List<string> updateList = new List<string>();
        foreach (var item in gameConfig.video)
        {
            for (int i = 0; i < item.Value.Count; i++)
            {
                if (!ConfigDataMgr.Instance.resourceDict.ContainsKey(item.Value[i].imgurl))
                {
                    updateList.Add(item.Value[i].imgurl);
                }
            }
        }

        for (int i = 0; i < updateList.Count; i++)
        {
            //设置转换进度
            uiLoginWindow.SetProgress((float)i / updateList.Count);
            yield return RequestNetworkFile(updateList[i], (result, content, data) =>
            {
                if (result)
                {
                    string fileName = System.Guid.NewGuid().ToString("N");
                    ResourcesMgr.Instance.WriteTextureFile(fileName, data);
                    ConfigDataMgr.Instance.resourceDict.Add(updateList[i], fileName);
                }
            });
        }
        uiLoginWindow.SetProgress(1.0f);
    }

    void CheckStringToAudio(string str2audio, List<string> turnList)
    {
        if (!ConfigDataMgr.Instance.resourceDict.ContainsKey(str2audio))
        {
            turnList.Add(str2audio);
        }
    }

    /// <summary>
    /// Requests the network file.
    /// </summary>
    /// <returns>The network file.</returns>
    /// <param name="url">URL.</param>
    /// <param name="callback">Callback.</param>
    IEnumerator RequestNetworkFile(string url, Action<bool, string, byte[]> callback)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        bool isError = false;

        if (request.isNetworkError)
        {
            Debug.LogErrorFormat("isNetworkError [{0}] [{1}]", url, request.error);
            isError = true;
        }
        else if (request.isHttpError)
        {
            Debug.LogErrorFormat("isHttpError [{0}] [{1}]", url, request.responseCode);
            isError = true;
        }
        else
        {
            if (request.responseCode == 200 || request.responseCode == 0)
            {
                callback(true, request.downloadHandler.text, request.downloadHandler.data);
            }
            else
            {
                Debug.LogErrorFormat("response code error [{0}]", request.responseCode);
                isError = true;
            }
        }

        if (isError)
        {
            callback(false, null, null);
        }
    }

}

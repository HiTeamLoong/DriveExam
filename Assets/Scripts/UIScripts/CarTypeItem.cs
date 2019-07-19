using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarTypeItem : MonoBehaviour {

    public Image image;
    public Image isNew;
    public Button button;
    private CarTypeData typeData;
    private Callback<CarTypeData> callback;


    private void Start()
    {
        button.onClick.AddListener(OnClickButton);
    }

    public void InitWith(CarTypeData typeData,Callback<CarTypeData> callback)
    {
        this.typeData = typeData;
        this.callback = callback;

        if (typeData.image.Contains("http://"))
        {
            ResourcesMgr.Instance.AsyncLoadTextureWithName(typeData.image, (texture2D) =>
            {
                if (image != null && texture2D != null)
                {
                    image.sprite = Sprite.Create(texture2D, new Rect(0.0f, 0.0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
                    isNew.gameObject.SetActive(false);
                }
            });
        }
        else if(!string.IsNullOrEmpty(Enum.GetName(typeof(CarUID),typeData.uid)))
        {
            string imageName = "";
            switch ((CarUID)typeData.uid)
            {
                case CarUID.SangTaNa_Old:
                case CarUID.SangTaNa_New:
                    imageName = "sangtana";
                    break;
                case CarUID.AiLiShe_Old:
                case CarUID.AiLiShe_New:
                    imageName = "ailishe";
                    break;
                case CarUID.BenTengB30_Old:
                case CarUID.BenTengB30_New:
                    imageName = "bentengb30";
                    break;
                case CarUID.AiLiShe2_Old:
                case CarUID.AiLiShe2_New:
                    imageName = "ailishe2";
                    break;
            }
            Texture2D texture = ResourcesMgr.Instance.LoadTexture("CarType", imageName);
            image.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            isNew.gameObject.SetActive(typeData.type == 1);
        }
    }

    void OnClickButton()
    {
        if (callback!=null)
        {
            callback(typeData);
        }
    }
}

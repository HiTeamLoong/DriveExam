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
        Texture2D texture = ResourcesMgr.Instance.LoadTexture("CarType", typeData.image);
        image.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        isNew.gameObject.SetActive(typeData.type == 1);
    }

    void OnClickButton()
    {
        if (callback!=null)
        {
            callback(typeData);
        }
    }
}

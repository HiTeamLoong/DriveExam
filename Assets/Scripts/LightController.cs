using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Camera camera;

    private void Awake()
    {
        Debug.Log(UIManager.Instance.defaultResolution);
        Debug.Log(UIManager.Instance.realResolution);
        Vector2 defaultRatio = UIManager.Instance.defaultResolution;
        Vector2 realRatio = UIManager.Instance.realResolution;

        if ((defaultRatio.y / defaultRatio.x) < (realRatio.y / realRatio.x))
        {
            float ratio = defaultRatio.y/realRatio.y;
            Debug.Log("settting"+ratio);
            camera.rect = new Rect(0, (1 - ratio) / 2, 1, ratio);
        }
    }

    public GameObject light_Clearance;  //示廓灯
    public GameObject light_HeadNear;   //近光灯
    public GameObject light_HeadFar;    //远光灯
    public GameObject light_Fog;        //前雾灯


    public void SetClearanc(bool show)
    {
        light_Clearance.SetActive(show);
    }

    public void SetFogLight(bool show)
    {
        light_Fog.SetActive(show);
    }

    public void SetHeadFar(bool show)
    {
        light_HeadFar.SetActive(show);
    }

    public void SetHeadNear(bool show)
    {
        light_HeadNear.SetActive(show);
    }
}

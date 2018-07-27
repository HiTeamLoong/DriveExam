using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{

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

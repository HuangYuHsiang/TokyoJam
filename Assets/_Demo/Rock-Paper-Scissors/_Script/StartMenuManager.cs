using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StartMenuManager: MonoBehaviour
{
    //取得手勢狀態
    public static bool IsHandStay = false;

    public Material StartHandMaterial;

    public Color Color_red;
    public Color Color_white;    

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other) {
        Debug.Log("OnTriggerEnter");

        //如果物件是手
        if (other.gameObject.name == "OculusHand_R")
        {
            //if (HandPoseManager.myHandPose == CustomHandPose.Cloth)
            //{
            //    //模型變色            
            //    StartHandMaterial.SetColor("_ColorTop", Color_red); //名稱是前面的名稱，而非括號內字串_ColorTop
            //}
        }      


        //開始倒數計時

    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("OnTriggerStay");
        IsHandStay = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit");
        IsHandStay = false;

        //如果物件是手
        if (other.gameObject.name == "OculusHand_R")
        {
            //模型變色
            //StartHandMaterial.color = Color_white;
            StartHandMaterial.SetColor("_ColorTop", Color_white);
        }
    }
}

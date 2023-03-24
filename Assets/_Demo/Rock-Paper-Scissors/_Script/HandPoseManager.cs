using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

//public enum CustomObjectType
//{
//    ClothHand,
//    ScissorsHand,
//    RockHand
//}

public class HandPoseManager : MonoBehaviour
{

    #region Unity Event
    [HideInInspector]
    public class OnTriggerEnterEvent : UnityEvent<CustomPose ,GameObject, CustomHandType> { }
    
    [HideInInspector]
    public class OnTriggerExitEvent : UnityEvent<CustomPose, GameObject, CustomHandType> { }

    public OnTriggerEnterEvent TriggerEnter;
    public OnTriggerExitEvent TriggerExit;
    #endregion

    #region Debug UI
    public TextMeshProUGUI UIText_Debug;
    #endregion

    [SerializeField] 
    private HandTriggerDetector HandTriggerDetector_Right;

    [SerializeField]
    private HandTriggerDetector HandTriggerDetector_Left;

    private void Awake()
    {
        TriggerEnter = new OnTriggerEnterEvent();
        TriggerExit = new OnTriggerExitEvent();
    }

    private void Start()
    {
        HandTriggerDetector_Right.TriggerEnter.AddListener(OnCustomTriggerEnter);
        HandTriggerDetector_Right.TriggerExit.AddListener(OnCustomTriggerExit);

        HandTriggerDetector_Left.TriggerEnter.AddListener(OnCustomTriggerEnter);
        HandTriggerDetector_Left.TriggerExit.AddListener(OnCustomTriggerExit);
    }

    private void OnDestroy()
    {
        HandTriggerDetector_Right.TriggerEnter.RemoveListener(OnCustomTriggerEnter);
        HandTriggerDetector_Right.TriggerExit.RemoveListener(OnCustomTriggerExit);

        HandTriggerDetector_Left.TriggerEnter.RemoveListener(OnCustomTriggerEnter);
        HandTriggerDetector_Left.TriggerExit.RemoveListener(OnCustomTriggerExit);
    }

    private void OnCustomTriggerEnter(GameObject mGameObject, CustomHandType mCustomHandType) 
    {
        //UIText_Debug.text = "";
        //UIText_Debug.text += "\n Trigger Type: Enter";
        //UIText_Debug.text += "\n Hand Type: " + mCustomHandType;
        //UIText_Debug.text += "\n Game Object: " + mGameObject.name;
        //UIText_Debug.text += "\n RightHandPose: " + HandPoseDetector.instance.RightHandPose;
        //UIText_Debug.text += "\n LeftHandPose: " + HandPoseDetector.instance.LeftHandPose;

        //傳遞碰撞到的物件和當前的手勢給Game Manager
        if (TriggerEnter != null)
        {
            CustomPose mCustomHandPose = CustomPose.None;

            if (mCustomHandType == CustomHandType.Left)            
                mCustomHandPose = HandPoseDetector.instance.LeftHandPose;             
            else if (mCustomHandType == CustomHandType.Right)
                mCustomHandPose = HandPoseDetector.instance.RightHandPose;

            TriggerEnter.Invoke(mCustomHandPose, mGameObject, mCustomHandType);
        }
    }

    private void OnCustomTriggerExit(GameObject mGameObject, CustomHandType mCustomHandType)
    {
        //UIText_Debug.text = "";
        //UIText_Debug.text += "\n Trigger Type: Exit";
        //UIText_Debug.text += "\n Hand Type: " + mCustomHandType;
        //UIText_Debug.text += "\n Game Object: " + mGameObject.name;
        //UIText_Debug.text += "\n RightHandPose: " + HandPoseDetector.instance.RightHandPose;
        //UIText_Debug.text += "\n LeftHandPose: " + HandPoseDetector.instance.LeftHandPose;
        
        //傳遞碰撞到的物件和當前的手勢給Game Manager
        if (TriggerExit != null)
        {
            CustomPose mCustomHandPose = CustomPose.None;

            if (mCustomHandType == CustomHandType.Left)
                mCustomHandPose = HandPoseDetector.instance.LeftHandPose;
            else if (mCustomHandType == CustomHandType.Right)
                mCustomHandPose = HandPoseDetector.instance.RightHandPose;

            TriggerExit.Invoke(mCustomHandPose, mGameObject, mCustomHandType);
        }
    }

}

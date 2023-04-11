using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public enum CustomPose
{
    None = 0,
    Scissors = 1,
    Rock = 2,
    Cloth = 3,
    ThumbsUp = 4,
    Fire = 5, //Sequence 
    Thunder = 6, //Sequence 
    HandKnife = 7, //JointVelocityAndRotation
}
public class HandPoseDetector : MonoBehaviour
{
    // Date: 2023-04-05
    // Author: William
    // Description: Added HandPoseChanged Event
    #region Unity Event
    [HideInInspector]
    public class OnLeftHandPoseChanged : UnityEvent<CustomPose> { }
    [HideInInspector]
    public class OnRightHandPoseChanged : UnityEvent<CustomPose> { }
    
    public OnLeftHandPoseChanged LeftHandPoseChanged;
    public OnRightHandPoseChanged RightHandPoseChanged;
    #endregion

    // Date: 2023-04-05
    // Author: William
    // Description: Changed variable format
    #region Hand Pose variable
    [HideInInspector]
    private CustomPose _leftHandPose;
    public CustomPose LeftHandPose 
    { 
        set 
        { 
            _leftHandPose = value;
            LeftHandPoseChanged.Invoke(value);
        }
        get { return _leftHandPose; } 
    }

    [HideInInspector]
    private CustomPose _rightHandPose;
    public CustomPose RightHandPose
    {
        set
        {
            _rightHandPose = value;
            RightHandPoseChanged.Invoke(value);
        }
        get { return _rightHandPose; }
    }
    #endregion

    public static HandPoseDetector instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        LeftHandPoseChanged = new OnLeftHandPoseChanged();
        RightHandPoseChanged = new OnRightHandPoseChanged();
    }

    #region Rock Pose    
    public void RockPoseLeft()
    {
        LeftHandPose = CustomPose.Rock;
    }

    public void RockPoseRight()
    {
        RightHandPose = CustomPose.Rock;        
    }
    #endregion

    #region Cloth Pose    
    public void ClothPoseRight()
    {
        RightHandPose = CustomPose.Cloth;
    }

    public void ClothPoseLeft()
    {
        LeftHandPose = CustomPose.Cloth;
    }
    #endregion

    #region  Scissors Pose    
    public void ScissorsPoseRight()
    {
        RightHandPose = CustomPose.Scissors;
    }

    public void ScissorsPoseLeft()
    {
        LeftHandPose = CustomPose.Scissors;
    }
    #endregion

    #region  ThumbsUp Pose    
    public void ThumbsUpPoseRight()
    {
        RightHandPose = CustomPose.ThumbsUp;
    }

    public void ThumbsUpPoseLeft()
    {
        LeftHandPose = CustomPose.ThumbsUp;
    }
    #endregion

    #region  Fire Pose (Sequence)
    public void FirePoseRight()
    {
        RightHandPose = CustomPose.Fire;
    }

    public void FirePoseLeft()
    {
        LeftHandPose = CustomPose.Fire;
    }
    #endregion

    #region  Thunder Pose (Sequence)
    public void ThunderPoseRight()
    {
        RightHandPose = CustomPose.Thunder;
    }

    public void ThunderPoseLeft()
    {
        LeftHandPose = CustomPose.Thunder;
    }
    #endregion

    #region  HandKnife Pose (JointVelocityAndRotation)
    public void HandKnifePoseRight()
    {
        RightHandPose = CustomPose.HandKnife;
    }

    public void HandKnifePoseLeft()
    {
        LeftHandPose = CustomPose.HandKnife;
    }
    #endregion

    #region Pose UnSelected
    public void PoseUnSelectedRight()
    {
        RightHandPose = CustomPose.None;
    }

    public void PoseUnSelectedLeft()
    {
        LeftHandPose = CustomPose.None;
    }
    #endregion

    private void Update()
    {
        //Debug.Log("Pose Left:" + LeftHandPose);
        //Debug.Log("Pose Right:" + RightHandPose);
    }   

}

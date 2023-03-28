using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [HideInInspector]
    public CustomPose LeftHandPose;

    [HideInInspector]
    public CustomPose RightHandPose;

    public static HandPoseDetector instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
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
        Debug.Log("Pose Left:" + LeftHandPose);
        Debug.Log("Pose Right:" + RightHandPose);
    }

}

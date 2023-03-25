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

    #region Pose UnSelected
    public void PoseUnSelectedRight()
    {
        //RightHandPose = CustomHandPose.None;
    }

    public void PoseUnSelectedLeft()
    {
        //LeftHandPose = CustomHandPose.None;
    }
    #endregion

    private void Update()
    {
        Debug.Log("Pose Left:" + LeftHandPose);
        Debug.Log("Pose Right:" + RightHandPose);
    }

}

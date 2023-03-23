using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum CustomHandPose
{
    None = 0,
    Scissors = 1,
    Rock = 2,
    Cloth = 3
}
public class HandPoseDetector : MonoBehaviour
{
    [HideInInspector]
    public CustomHandPose LeftHandPose;

    [HideInInspector]
    public CustomHandPose RightHandPose;

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
        RightHandPose = CustomHandPose.Rock;
    }

    public void RockPoseRight()
    {
        LeftHandPose = CustomHandPose.Rock;
    }
    #endregion

    #region Cloth Pose    
    public void ClothPoseRight()
    {
        RightHandPose = CustomHandPose.Cloth;
    }

    public void ClothPoseLeft()
    {
        LeftHandPose = CustomHandPose.Cloth;
    }
    #endregion

    #region  Scissors Pose    
    public void ScissorsPoseRight()
    {
        RightHandPose = CustomHandPose.Scissors;
    }

    public void ScissorsPoseLeft()
    {
        LeftHandPose = CustomHandPose.Scissors;
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JointVelocityAndRotationDemoManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshPro UIText_RightHeadPose;

    void Update()
    {
        UIText_RightHeadPose.text = HandPoseDetector.instance.RightHandPose.ToString();
    }
}

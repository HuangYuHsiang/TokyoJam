using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshPro UIText_RightHead;
    

    void Update()
    {
        if (HandPoseDetector.instance.RightHandPose != CustomPose.None)
        {
            UIText_RightHead.gameObject.SetActive(true);
            UIText_RightHead.text = HandPoseDetector.instance.RightHandPose.ToString();
        }
        else        
            UIText_RightHead.gameObject.SetActive(false);        
    }
}

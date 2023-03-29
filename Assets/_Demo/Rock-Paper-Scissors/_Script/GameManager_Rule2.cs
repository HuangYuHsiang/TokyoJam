using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager_Rule2 : MonoBehaviour
{
    #region Debug UI
    public TextMeshProUGUI UIText_Debug;
    #endregion

    [Header("Manager")]
    public HandPoseManager HandPoseManager;

    [Header("UI")]
    public GameObject Button_Start;
    public TextMeshPro Button_Start_Text;

    public TextMeshPro UIText_Score;    
    public TextMeshPro UIText_BestScore;
    public TextMeshPro UIText_HowToPlay;    

    public TextMeshPro UIText_RightHeadPose;
    public TextMeshPro UIText_LeftHeadPose;

    [Header("Prefab")]
    public GameObject prefab_ClothHand;
    public GameObject prefab_ScissorsHand;
    public GameObject prefab_RockHand;


    [Header("Setting")]
    bool IsGameStart = false;
    int Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    #region Debug UI
    public TextMeshProUGUI UIText_Debug;
    #endregion

    [Header("Manager")]
    public HandPoseManager HandPoseManager;

    [Header("UI")]
    public TextMeshPro UIText_Score;
    public GameObject Button_Start;
    public TextMeshPro Button_Start_Text;
    public TextMeshPro UIText_BestScore;

    public TextMeshPro UIText_RightHeadPose;
    public TextMeshPro UIText_LeftHeadPose;

    [Header("Prefab")]
    public GameObject prefab_ClothHand;
    public GameObject prefab_ScissorsHand;
    public GameObject prefab_RockHand;

    [HideInInspector]
    public int spawnAreaXMin = -2;
    [HideInInspector]
    public int spawnAreaXMax = 2;

    [HideInInspector]
    public float spawnIntervalMin = 3.15f;
    [HideInInspector]
    public float spawnIntervalMax = 4.25f;

    bool IsGameStart = false;
    int Score = 0;

    private void Start()
    {
        HandPoseManager.TriggerEnter.AddListener(OnCustomTriggerEnter);
        HandPoseManager.TriggerExit.AddListener(OnCustomTriggerExit);

        int BestScore = PlayerPrefs.GetInt("BestScore", 0);
        UIText_BestScore.text = BestScore.ToString();
    }

    private void OnDestroy()
    {
        HandPoseManager.TriggerEnter.RemoveListener(OnCustomTriggerEnter);
        HandPoseManager.TriggerExit.RemoveListener(OnCustomTriggerExit);
    }

    public void GameStart() {

        //處理VR按鈕瞬間消失會出現錯誤的解法
        this.transform.DOScaleZ(1, 0.15f).OnComplete(() =>
        {
            IsGameStart = true;
            Button_Start.SetActive(false);
            StartCoroutine(SpawnRandomPrefabs());            
        });

        Score = 0;

        timer = countdownTime;
    }

    #region CountDown

    public TextMeshPro UIText_Timer;
    private float countdownTime = 15.0f;
    private float timer;

    void Countdown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 0;
            // 在這裡添加倒計時結束後的操作，例如遊戲結束或重新啟動等。
            IsGameStart = false;
            Button_Start_Text.text = "REPLAY";
            Button_Start.SetActive(true);

            int BestScore = PlayerPrefs.GetInt("BestScore", 0);
            if (Score > BestScore)
            {
                BestScore = Score;
                PlayerPrefs.SetInt("BestScore", BestScore);
                UIText_BestScore.text = BestScore.ToString();
            }
        }

        int minutes = (int)(timer / 60);
        int seconds = (int)(timer % 60);
        int milliseconds = (int)((timer * 100) % 100);

        UIText_Timer.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    #endregion

    private void Update()
    {
        UIText_RightHeadPose.text = HandPoseDetector.instance.RightHandPose.ToString();
        UIText_LeftHeadPose.text = HandPoseDetector.instance.LeftHandPose.ToString();

        if (!IsGameStart)
            return;

        UIText_Score.text = Score.ToString();
        Countdown();
    }

    private void OnCustomTriggerEnter(CustomPose mCustomHandPose, GameObject mGameObject, CustomHandType mCustomHandType)
    {
        UIText_Debug.text = "";
        UIText_Debug.text += "\n Trigger Type: Enter";
        UIText_Debug.text += "\n Hand Type: " + mCustomHandType;
        UIText_Debug.text += "\n Game Object: " + mGameObject.name;
        UIText_Debug.text += "\n RightHandPose: " + HandPoseDetector.instance.RightHandPose;
        UIText_Debug.text += "\n LeftHandPose: " + HandPoseDetector.instance.LeftHandPose;

        CustomPose GameObjectHandPose = mGameObject.GetComponent<ObjectInfoHelper>().mHandPose;
        GameObjectController mGameObjectController = mGameObject.GetComponent<GameObjectController>();

        #region 遊戲邏輯

        //石頭
        if (GameObjectHandPose == CustomPose.Rock)
        {
            if (mCustomHandPose == CustomPose.Cloth)
            {
                mGameObjectController.DestroyItem();

                if (IsGameStart)
                    Score++;
            }

            if (mCustomHandPose == CustomPose.Scissors)
            {
                if (IsGameStart)
                    Score--;
            }
        }

        //剪刀
        if (GameObjectHandPose == CustomPose.Scissors)
        {
            if (mCustomHandPose == CustomPose.Rock)
            {
                mGameObjectController.DestroyItem();

                if (IsGameStart)
                    Score++;
            }

            if (mCustomHandPose == CustomPose.Cloth)
            {
                if (IsGameStart)
                    Score--;
            }
        }

        //布
        if (GameObjectHandPose == CustomPose.Cloth)
        {
            if (mCustomHandPose == CustomPose.Scissors)
            {
                mGameObjectController.DestroyItem();

                if (IsGameStart)
                    Score++;
            }

            if (mCustomHandPose == CustomPose.Rock)
            {
                if (IsGameStart)
                    Score--;
            }
        }

        #endregion

        //if (mCustomHandPose == GameObjectHandPose)
        //{
        //    mGameObjectController.DestroyItem();

        //    if (IsGameStart)
        //        Score++;
        //}
    }

    private void OnCustomTriggerExit(CustomPose mCustomHandPose, GameObject mGameObject, CustomHandType mCustomHandType)
    {
        //UIText_Debug.text = "";
        //UIText_Debug.text += "\n Trigger Type: Exit";
        //UIText_Debug.text += "\n Hand Type: " + mCustomHandType;
        //UIText_Debug.text += "\n Game Object: " + mGameObject.name;
        //UIText_Debug.text += "\n RightHandPose: " + HandPoseDetector.instance.RightHandPose;
        //UIText_Debug.text += "\n LeftHandPose: " + HandPoseDetector.instance.LeftHandPose;

        //CustomHandPose GameObjectHandPose = mGameObject.GetComponent<ObjectInfoHelper>().mHandPose;
        //GameObjectController mGameObjectController = mGameObject.GetComponent<GameObjectController>();

        //if (mCustomHandPose == GameObjectHandPose)
        //    mGameObjectController.DestroyItem();
    }

    private IEnumerator SpawnRandomPrefabs()
    {        
        while (IsGameStart)
        {

            //GameObject prefabToSpawn = Random.Range(0, 2) == 0 ? prefab_ClothHand : prefab_ScissorsHand;

            int randomIndex = Random.Range(0, 3); // 生成0（包括）到3（不包括）之間的隨機數字
            GameObject prefabToSpawn;

            switch (randomIndex)
            {
                case 0:
                    prefabToSpawn = prefab_ClothHand;
                    break;
                case 1:
                    prefabToSpawn = prefab_ScissorsHand;
                    break;
                case 2:
                    prefabToSpawn = prefab_RockHand;
                    break;
                default:
                    prefabToSpawn = prefab_ClothHand;
                    break;
            }


            Vector3 randomPosition = new Vector3(
                Random.Range(spawnAreaXMin, spawnAreaXMax),
                0.95f,
                2
            );

            prefabToSpawn = Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);

            prefabToSpawn.GetComponent<GameObjectController>().Move();

            yield return new WaitForSeconds(Random.Range(spawnIntervalMin, spawnIntervalMax));
        }
    }
}

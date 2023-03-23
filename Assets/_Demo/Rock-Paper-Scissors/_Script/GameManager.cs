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

    [Header("Prefab")]
    public GameObject prefab_ClothHand;
    public GameObject prefab_ScissorsHand;
    public GameObject prefab_RockHand;

    [HideInInspector]
    public int spawnAreaXMin = -2;
    [HideInInspector]
    public int spawnAreaXMax = 2;

    [HideInInspector]
    public float spawnIntervalMin = 1.75f;
    [HideInInspector]
    public float spawnIntervalMax = 2.55f;

    bool IsGameStart = false;
    int Score = 0;

    private void Start()
    {
        HandPoseManager.TriggerEnter.AddListener(OnCustomTriggerEnter);
        HandPoseManager.TriggerExit.AddListener(OnCustomTriggerExit);


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
            Button_Start.SetActive(false);
            StartCoroutine(SpawnRandomPrefabs());
            
        });
        
    }

    private void Update()
    {
        UIText_Score.text = Score.ToString();
    }

    private void OnCustomTriggerEnter(CustomHandPose mCustomHandPose, GameObject mGameObject, CustomHandType mCustomHandType)
    {
        UIText_Debug.text = "";
        UIText_Debug.text += "\n Trigger Type: Enter";
        UIText_Debug.text += "\n Hand Type: " + mCustomHandType;
        UIText_Debug.text += "\n Game Object: " + mGameObject.name;
        UIText_Debug.text += "\n RightHandPose: " + HandPoseDetector.instance.RightHandPose;
        UIText_Debug.text += "\n LeftHandPose: " + HandPoseDetector.instance.LeftHandPose;

        CustomHandPose GameObjectHandPose = mGameObject.GetComponent<ObjectInfoHelper>().mHandPose;
        GameObjectController mGameObjectController = mGameObject.GetComponent<GameObjectController>();

        if (mCustomHandPose == GameObjectHandPose)
        {
            mGameObjectController.DestroyItem();
            Score++;
        }
    }

    private void OnCustomTriggerExit(CustomHandPose mCustomHandPose, GameObject mGameObject, CustomHandType mCustomHandType)
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
        while (true)
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

            Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(spawnIntervalMin, spawnIntervalMax));
        }
    }
}

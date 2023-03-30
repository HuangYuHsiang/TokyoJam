using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;


public class GameManager_Rule2 : MonoBehaviour
{
    #region Debug UI
    public TextMeshProUGUI UIText_Debug;
    #endregion


    [Header("SkinnedMeshRenderer")]
    public SkinnedMeshRenderer SMR_RightHand;
    public SkinnedMeshRenderer SMR_LeftHand;

    [Header("Material")]
    public Material Material_Hand_Red;
    public Material Material_Hand_White;

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
    public GameObject[] prefabs;


    [Header("Setting")]
    bool IsGameStart = true;
    bool IsRightHeadLock = false;
    bool IsLeftHeadLock = false;
    int Score = 0;
    int Wave = 0;
    int HeadLockTime = 3;

    [Header("Other")]
    public List<GameObject> WaveObjectList = new List<GameObject>();

    public void GameStart()
    {

        //處理VR按鈕瞬間消失會出現錯誤的解法
        this.transform.DOScaleZ(1, 0.15f).OnComplete(() =>
        {
            IsGameStart = true;
            Button_Start.SetActive(false);
            CheckWave();
        });

    }

    public void GameOver()
    {
        IsGameStart = false;        
        Button_Start.SetActive(true);
        Button_Start_Text.text = "再玩一次";
    }

    // Start is called before the first frame update
    void Start()
    {
        int seed = System.DateTime.Now.GetHashCode();
        Random.InitState(seed);

        HandPoseManager.TriggerEnter.AddListener(OnCustomTriggerEnter);
        HandPoseManager.TriggerExit.AddListener(OnCustomTriggerExit);        
    }

    private void OnDestroy()
    {
        HandPoseManager.TriggerEnter.RemoveListener(OnCustomTriggerEnter);
        HandPoseManager.TriggerExit.RemoveListener(OnCustomTriggerExit);
    }

    void RightHeadLock()
    {
        SMR_RightHand.material = Material_Hand_Red;
        IsRightHeadLock = true;

        this.transform.DOScaleZ(1, HeadLockTime).OnComplete(() =>
        {
            SMR_RightHand.material = Material_Hand_White;
            IsRightHeadLock = false;
        });
    }

    void LeftHeadLock()
    {
        SMR_LeftHand.material = Material_Hand_Red;
        IsLeftHeadLock = true;

        this.transform.DOScaleZ(1, HeadLockTime).OnComplete(() =>
        {
            SMR_LeftHand.material = Material_Hand_White;
            IsLeftHeadLock = false;
        });
    }

    private void SetHandLock(CustomHandType mCustomHandType)
    {
        switch (mCustomHandType)
        {
            case CustomHandType.Left:
                LeftHeadLock();
                break;
            case CustomHandType.Right:
                RightHeadLock();
                break;
            default:
                RightHeadLock();
                break;
        }
    }

    private void OnCustomTriggerEnter(CustomPose mCustomHandPose, GameObject mGameObject, CustomHandType mCustomHandType)
    {
        #region 檢查手部目前是否是凍結狀態
        if (mCustomHandType == CustomHandType.Right)
        {
            if (IsRightHeadLock)
                return;
        }

        if (mCustomHandType == CustomHandType.Left)
        {
            if (IsLeftHeadLock)
                return;
        }
        #endregion

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
                WaveObjectList.Remove(mGameObject);

                if (IsGameStart)
                    NextWave();
            }

            if (mCustomHandPose == CustomPose.Scissors)
            {
                if (IsGameStart)                
                    SetHandLock(mCustomHandType);                
            }
        }

        //剪刀
        if (GameObjectHandPose == CustomPose.Scissors)
        {
            if (mCustomHandPose == CustomPose.Rock)
            {
                mGameObjectController.DestroyItem();
                WaveObjectList.Remove(mGameObject);

                if (IsGameStart)
                    NextWave();
            }

            if (mCustomHandPose == CustomPose.Cloth)
            {
                if (IsGameStart)
                    SetHandLock(mCustomHandType);
            }
        }

        //布
        if (GameObjectHandPose == CustomPose.Cloth)
        {
            if (mCustomHandPose == CustomPose.Scissors)
            {
                mGameObjectController.DestroyItem();
                WaveObjectList.Remove(mGameObject);

                if (IsGameStart)
                    NextWave();
            }

            if (mCustomHandPose == CustomPose.Rock)
            {
                if (IsGameStart)
                    SetHandLock(mCustomHandType);
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


    // Update is called once per frame
    void Update()
    {
        
    }

    #region Wave

    void NextWave() {



        if (WaveObjectList.Count != 0)
            return;
        else
        {
            if (Wave >= 2)
            {
                GameOver();
                return;
            }

            Wave++;
            CheckWave();
        }
    }

    void CheckWave() {      

        switch (Wave)
        {
            case 0:
                SpawnWave_1();
                break;
            case 1:
                SpawnWave_2();
                break;
            case 2:
                SpawnWave_3();
                break;
        }
    }

    private void SpawnWave_1()
    {        
        SpawnObjects(3);
    }

    private void SpawnWave_2()
    {     
        SpawnObjects(5);
    }

    private void SpawnWave_3()
    {        
        SpawnObjects(8);
    }
    #endregion

    private void SpawnObjects(int count)
    {
        List<Vector3> vectors = GenerateRandomVectors(count);

        for (int i = 0; i < count; i++)
        {
            Vector3 position = vectors[i];
            int randomIndex = Random.Range(0, prefabs.Length);
            GameObject prefabToSpawn = prefabs[randomIndex] ;
            GameObject spawnedObject = Instantiate(prefabToSpawn, position, Quaternion.identity);

            spawnedObject.GetComponent<GameObjectController>().PopUp();

            WaveObjectList.Add(spawnedObject);
        }
    }


    List<Vector3> GenerateRandomVectors(int numberOfVectors)
    {
        float[] possibleXValues = new float[] { -0.2f, 0f, 0.2f };
        float[] possibleYValues = new float[] {  0.8f, 1f, 1.2f};

        List<Vector3> vectors = new List<Vector3>();

        while (vectors.Count < numberOfVectors)
        {
            float x = possibleXValues[Random.Range(0, possibleXValues.Length)];
            float y = possibleYValues[Random.Range(0, possibleYValues.Length)];

            Vector3 vector = new Vector3(x, y, 0.5f);

            if (!vectors.Contains(vector))
            {
                vectors.Add(vector);
            }
        }

        //System.Random random = new System.Random();
        //vectors = vectors.OrderBy(x => random.Next()).ToList();

        return vectors;
    }
}

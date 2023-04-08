using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;
using TMPro;
using BBAPE.Toolkit;

public class GameManager_Mode3 : MonoBehaviour
{
    [Header("UI")]
    public GameObject Button_Start;
    public GameObject UIText_GameOver;
    public TextMeshPro UIText_Score;

    [Header("GazeControlledArea")]
    public GazeControlledArea mGazeControlledArea;

    [Header("Resource Pool")]
    public PrefabPool mPrefabPool;

    [Header("Sprite Renderer")]
    public SpriteRenderer LeftHandPoseIcon;
    public SpriteRenderer RightHandPoseIcon;

    public Sprite[] CustomPoseSprite; //請依照enum CustomPose放入對應的圖示

    [Header("Score")]
    private int Score = 0;

    [Header("Score")]
    bool IsGameStart = false;

    // Start is called before the first frame update
    void Start()
    {
        #region Random
        int seed = System.DateTime.Now.GetHashCode();
        Random.InitState(seed);
        #endregion

        HandPoseDetector.instance.LeftHandPoseChanged.AddListener(OnLeftHandPoseChanged);
        HandPoseDetector.instance.RightHandPoseChanged.AddListener(OnRightHandPoseChanged);

   
    }

    private void OnDestroy()
    {
        HandPoseDetector.instance.LeftHandPoseChanged.RemoveListener(OnLeftHandPoseChanged);
        HandPoseDetector.instance.RightHandPoseChanged.RemoveListener(OnRightHandPoseChanged);
    }

    private void OnLeftHandPoseChanged(CustomPose mCustomHandPose)
    {
        LeftHandPoseIcon.sprite = CustomPoseSprite[(int)mCustomHandPose];
    }

    private void OnRightHandPoseChanged(CustomPose mCustomHandPose)
    {
        RightHandPoseIcon.sprite = CustomPoseSprite[(int)mCustomHandPose];
    }

    public void GameStart() {
        //處理VR按鈕瞬間消失會出現錯誤的解法
        this.transform.DOScaleZ(1, 0.15f).OnComplete(() =>
        {
            Button_Start.SetActive(false);
            IsGameStart = true;
            StartCoroutine(SpawnPrefab());
        });
    }

    private void Update()
    {
        UIText_Score.text = Score + "";
    }

    public void GameOver()
    {
        IsGameStart = false;
        Button_Start.SetActive(true);
        UIText_GameOver.SetActive(true);
    }

    private IEnumerator SpawnPrefab()
    {
        while (IsGameStart)
        {
            float waitTime = Random.Range(1f, 4f);
            yield return new WaitForSeconds(waitTime);

            Vector3 randomPosition = GetRandomPosition();
            GameObject mGo = mPrefabPool.GetUnusedPrefab();
            if (mGo != null)
            {
                mGo.transform.position = randomPosition;
                mGazeControlledArea.targetObjects.Add(mGo);
            }
        }
    }

    public Vector3 GetRandomPosition()
    {
        float x = Random.Range(0, 2) < 1 ? Random.Range(-3f, -1.5f) : Random.Range(1.5f, 3f);
        float y = Random.Range(0.5f, 2.5f);
        float z = Random.Range(0, 2) < 1 ? Random.Range(-3f, -1.5f) : Random.Range(1.5f, 3f);

        return new Vector3(x, y, z);
    }


    public  void RemovePrefab(GameObject mGO) {
        mGazeControlledArea.targetObjects.Remove(mGO);
        Score++;
    }
}

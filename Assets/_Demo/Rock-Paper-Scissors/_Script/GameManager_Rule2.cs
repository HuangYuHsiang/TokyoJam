using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;



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
    public GameObject[] prefabs;


    [Header("Setting")]
    bool IsGameStart = false;
    int Score = 0;
    int Wave = 0;

    // Start is called before the first frame update
    void Start()
    {
        int seed = System.DateTime.Now.GetHashCode();
        Random.InitState(seed);

   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Wave
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
        }
    }


    List<Vector3> GenerateRandomVectors(int numberOfVectors)
    {
        float[] possibleXValues = new float[] { -0.6f, 0f, 0.4f };
        float[] possibleYValues = new float[] {  0.6f, 1f, 1.4f};

        List<Vector3> vectors = new List<Vector3>();

        while (vectors.Count < numberOfVectors)
        {
            float x = possibleXValues[Random.Range(0, possibleXValues.Length)];
            float y = possibleYValues[Random.Range(0, possibleYValues.Length)];

            Vector3 vector = new Vector3(x, y, 1f);

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

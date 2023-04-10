using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// PrefabPool 用於管理場景中預設物件的資源池
public class PrefabPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabsInScene;

    private List<PrefabInstance> pool;

    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        pool = new List<PrefabInstance>(prefabsInScene.Count);

        foreach (GameObject prefab in prefabsInScene)
        {
            PrefabInstance prefabInstance = prefab.GetComponent<PrefabInstance>();
            pool.Add(prefabInstance);
            prefabInstance.SetDisable();
            //prefab.SetActive(false);
        }
    }

    public GameObject GetUnusedPrefab()
    {
        System.Random rand = new System.Random();
        pool = pool.OrderBy(n => rand.Next()).ToList();

        foreach (PrefabInstance instance in pool)
        {
            if (!instance.InUse)
            {
                //instance.SetInUse(true);
                //instance.gameObject.SetActive(true);
                instance.SetEnable();
                return instance.gameObject;
            }
        }

        // 如果所有物件都在使用中，則返回null
        return null;
    }

    public GameObject SetAllPrefabDisable()
    {        
        foreach (PrefabInstance instance in pool)
        {
            instance.SetDisable();
        }

        // 如果所有物件都在使用中，則返回null
        return null;
    }

    public void ReturnPrefabToPool(GameObject instance)
    {
        //PrefabInstance prefabInstance = instance.GetComponent<PrefabInstance>();
        //if (prefabInstance != null)
        //{
        //    prefabInstance.SetInUse(false);
        //    instance.SetActive(false);
        //}
    }
}

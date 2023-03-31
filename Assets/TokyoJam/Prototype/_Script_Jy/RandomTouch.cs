using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTouch : MonoBehaviour
{
    [SerializeField]
    private GameObject RandomGroup;
    GNLToolkit.SpawnerDOTSet.GNL_SpawnerDOTween GetSpawnerDOT;
    // Start is called before the first frame update
    void Start()
    {
        GetSpawnerDOT = RandomGroup.GetComponent<GNLToolkit.SpawnerDOTSet.GNL_SpawnerDOTween>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Interac"))
        {
            GetSpawnerDOT.SpawnerDestroy();
        }
            
    }
}

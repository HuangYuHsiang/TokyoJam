using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShow : MonoBehaviour
{
    public GameObject[] GetHandPrefab;
    [SerializeField]
    float RandomDuring;
    int HandPrefabAmount;
    int TotalAmount=0;
    bool IsRandom;
    // Start is called before the first frame update
    private void OnEnable()
    {
        HandPrefabAmount = GetHandPrefab.Length;
        
        GetHandPrefab[Random.Range(0, HandPrefabAmount)].SetActive(true);
        if (IsRandom)
        {
            InvokeRepeating("RandomSet", RandomDuring, RandomDuring);
        }
        else
        {
            TotalAmount = 0;
            GetHandPrefab[0].SetActive(true);
            TotalAmount += 1;
            InvokeRepeating("SortSet", RandomDuring, RandomDuring);
        }
        
    }

    // Update is called once per frame
    
    void RandomSet()
    {
        int GetRandomNum = Random.Range(0, HandPrefabAmount);
        
        foreach (GameObject  UniltHand in GetHandPrefab)
        {
            if(UniltHand != GetHandPrefab[GetRandomNum])
            {
                UniltHand.SetActive(false);
            }
            else
            {
                UniltHand.SetActive(true);
            }
        }
    }
    void SortSet()
    {   
        GetHandPrefab[TotalAmount].SetActive(true);
        GetHandPrefab[TotalAmount-1].SetActive(false);
        TotalAmount += 1;
        if (TotalAmount >= HandPrefabAmount)
        {
            CancelInvoke("SortSet");
        }

    }
}

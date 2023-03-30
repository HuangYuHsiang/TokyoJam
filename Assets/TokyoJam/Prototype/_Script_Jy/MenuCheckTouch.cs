using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuCheckTouch : MonoBehaviour
{
    
    [SerializeField]
    private GameObject GetAnimation;
    [SerializeField]
    GameObject SetGameTimeLine;
    public UnityEvent SetCommand;
    Animator GetAnimator;
    private void Start()
    {
        GetAnimator = GetAnimation.GetComponent<Animator>();
    }

    public void OnTriggerEnter(Collider other)
    {
        GetAnimator.SetTrigger("Hit");
        if(SetGameTimeLine != null)
        {
            SetGameTimeLine.SetActive(true);
        }
    }
}

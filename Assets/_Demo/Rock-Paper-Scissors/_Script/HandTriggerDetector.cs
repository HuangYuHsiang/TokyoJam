using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CustomHandType
{
    Left,
    Right
}

public class HandTriggerDetector : MonoBehaviour
{
    
    //[Serializable]
    [HideInInspector]
    public class OnTriggerEnterEvent : UnityEvent<GameObject, CustomHandType> { }
    
    //[Serializable]
    [HideInInspector]
    public class OnTriggerExitEvent : UnityEvent<GameObject, CustomHandType> { }

    //[SerializeField]
    //private OnTriggerEnterEvent _triggerEnter;
    //public OnTriggerEnterEvent TriggerEnter => _triggerEnter;

    //[SerializeField]
    //private OnTriggerExitEvent _triggerExit;
    //public OnTriggerExitEvent TriggerExit => _triggerExit;

    public OnTriggerEnterEvent TriggerEnter;
    public OnTriggerExitEvent TriggerExit;

    [SerializeField]
    private CustomHandType HandType;

    private void Awake()
    {
        TriggerEnter = new OnTriggerEnterEvent();
        TriggerExit = new OnTriggerExitEvent();
    }

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter:" +  other.name);
        if (TriggerEnter != null)        
            TriggerEnter.Invoke(other.gameObject, HandType);        
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("OnTriggerStay:" + other.name);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit:" + other.name);
        if (TriggerExit != null)
            TriggerExit.Invoke(other.gameObject, HandType); 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PrefabInstance 用於追踪Prefab的使用狀態
public class PrefabInstance : MonoBehaviour
{
    public bool InUse { get; private set; }

    private void SetInUse(bool inUse)
    {
        InUse = inUse;
    }

    public void SetDisable() {

        this.gameObject.SetActive(false);        
        SetInUse(false);
    }

    public void SetEnable()
    {
        this.gameObject.SetActive(true);
        SetInUse(true);
    }
}

using Sophon.TokyoJam.Level;
using Sophon.TokyoJam.ScriptableObjects;
using UnityEngine;

public abstract class BaseSpawner : MonoBehaviour
{    
    public abstract void Init(Transform container);
    public abstract void SpawnEnemies(EnemiesData.Data enemy, float speed, float lifetime);
    public abstract void UntrackSpawnedObject(SpawnObjectMover objMover);
}

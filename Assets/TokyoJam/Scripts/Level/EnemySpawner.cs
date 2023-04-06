using Sirenix.OdinInspector;
using Sophon.TokyoJam.Level;
using Sophon.TokyoJam.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : BaseSpawner
{
    [ReadOnly][SerializeField]private List<SpawnObjectMover> _SpawnedObjects = new List<SpawnObjectMover>();
    private Transform _Container;
    public override void Init(Transform container) {
        _Container = container;
    }
    public override void SpawnEnemies(EnemiesData.Data enemy, float speed, float lifetime) {
        var clone = Instantiate(enemy.Prefab, _Container);
        clone.transform.position = transform.position;
        clone.transform.forward = -transform.forward;
        var cloneMover = clone.AddComponent<SpawnObjectMover>();
        cloneMover.Init(transform.forward, speed, lifetime);
        cloneMover.WhenDestroy += UntrackSpawnedObject;
        
        _SpawnedObjects.Add(cloneMover);
    }
    public override void UntrackSpawnedObject(SpawnObjectMover objMover) {
        _SpawnedObjects.Remove(objMover);
    }
}

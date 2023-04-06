using Sirenix.OdinInspector;
using Sophon.TokyoJam.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    public enum EnemyType { paper, rock, scissors }
    public EnemyType _CurrentEnemyType = EnemyType.paper;
    public int _SpawnerIndex = 0;
    public EnemiesData _EnemiesData;
    public List<BaseSpawner> _EnemySpawners = new List<BaseSpawner>();
    public Transform _EnemyContainer;
    public float _Speed;
    public float _Lifetime;

    private EnemiesData.Data spawnPrefab;
    private void Start()
    {
        foreach (var spawner in _EnemySpawners)
        {
            spawner.Init(_EnemyContainer);
        }
    }
    public void SpawnObj(int spawnerIndex)
    {
        switch (_CurrentEnemyType)
        {
            case EnemyType.paper:
                SpawnPaper(spawnerIndex);
                break;
            case EnemyType.rock:
                SpawnRock(spawnerIndex);
                break;
            case EnemyType.scissors:
                SpawnScissors(spawnerIndex);
                break;
            default:
                break;
        }
    }
    public void SpawnObj(string spawnObjID, int spawnerIndex)
    {
        spawnPrefab = _EnemiesData.GetEnemy(spawnObjID);
        _EnemySpawners[spawnerIndex].SpawnEnemies(spawnPrefab, _Speed, _Lifetime);
    }
    public void SpawnRock(int spawnerIndex) {
        SpawnObj("Rock_Single", spawnerIndex);
    }
    public void SpawnPaper(int spawnerIndex)
    {
        SpawnObj("Paper_Single", spawnerIndex);
    }
    public void SpawnScissors(int spawnerIndex)
    {
        SpawnObj("Scissors_Single", spawnerIndex);
    }
}

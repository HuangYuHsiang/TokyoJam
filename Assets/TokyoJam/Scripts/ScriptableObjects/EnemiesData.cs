using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sophon.TokyoJam.ScriptableObjects
{
    [CreateAssetMenu(menuName = "TokyoJam/ ScriptableObject/ EnemiesData")]
    public class EnemiesData : ScriptableObject
    {
        [System.Serializable]
        public struct Data {
            public string Name;
            public GameObject Prefab;
        }
        public List<Data> _EnemiesDataList = new List<Data>();

        public Data GetEnemy(string enemyID) {
            var data = _EnemiesDataList.Find((item) => item.Name == enemyID);

            if (data.Name == null)
                throw new System.Exception($"Couldn't find enemy {enemyID}");

            return data;
        }
    }
}

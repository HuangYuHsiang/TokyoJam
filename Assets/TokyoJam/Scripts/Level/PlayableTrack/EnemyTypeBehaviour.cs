using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Playables;

namespace Sophon.TokyoJam.Timeline
{    
    public class EnemyTypeBehaviour : PlayableBehaviour
    {
        public EnemySpawnerController.EnemyType _EnemyType;
        public int _SpawnerIndex;
        private EnemySpawnerController spawner;
        private bool _ReadyToSpawn = false;
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            spawner = (EnemySpawnerController)playerData;
            spawner._CurrentEnemyType = _EnemyType;
            spawner._SpawnerIndex = _SpawnerIndex;

            if (!Application.isPlaying) return;

            if (_ReadyToSpawn)
            { 
                _ReadyToSpawn= false;
                spawner.SpawnObj(_SpawnerIndex);
            }            
        }
        public void MarkReadyToSpawn() { 
            _ReadyToSpawn = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Sophon.TokyoJam.Timeline
{
    public class SpawnEnemyClip : PlayableAsset
    {
        public int _SpawnerIndex;
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<EnemyTypeBehaviour>.Create(graph);

            EnemyTypeBehaviour enemyTypeBehaviour = playable.GetBehaviour();
            enemyTypeBehaviour._SpawnerIndex = _SpawnerIndex;

            enemyTypeBehaviour.MarkReadyToSpawn();

            return playable;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Playables;

namespace Sophon.TokyoJam.Timeline
{
    public class EnemyTypeClip : PlayableAsset
    {
        public int _SpawnerIndex;
        public EnemySpawnerController.EnemyType _EnemyType;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<EnemyTypeBehaviour>.Create(graph);

            EnemyTypeBehaviour enemyTypeBehaviour = playable.GetBehaviour();
            enemyTypeBehaviour._EnemyType = _EnemyType;
            enemyTypeBehaviour._SpawnerIndex = _SpawnerIndex;

            enemyTypeBehaviour.MarkReadyToSpawn();
            return playable;
        }
        
    }
}

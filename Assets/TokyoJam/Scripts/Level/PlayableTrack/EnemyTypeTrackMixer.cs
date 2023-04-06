using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Windows;

namespace Sophon.TokyoJam.Timeline
{
    public class EnemyTypeTrackMixer : PlayableBehaviour
    {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            EnemySpawnerController spawnerController = playerData as EnemySpawnerController;
            EnemySpawnerController.EnemyType enemyType = EnemySpawnerController.EnemyType.paper;
            int spawnerIndex = 0;

            if (!spawnerController) return;

            int inputCount = playable.GetInputCount();
            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                
                if (inputWeight > 0f) {
                    ScriptPlayable<EnemyTypeBehaviour> inputPlayable = (ScriptPlayable<EnemyTypeBehaviour>)playable.GetInput(i);
                    EnemyTypeBehaviour input =  inputPlayable.GetBehaviour();
                    enemyType = input._EnemyType;
                    spawnerIndex = input._SpawnerIndex;
                }
            }
            spawnerController._CurrentEnemyType = enemyType;
            spawnerController._SpawnerIndex = spawnerIndex;

            
        }
        public override void OnGraphStart(Playable playable)
        {
            // Reset spawn flag, so when user scrolling the timeline it will still spawn
            for (int i = 0; i < playable.GetInputCount(); i++)
            {
                ScriptPlayable<EnemyTypeBehaviour> scriptPlayable = (ScriptPlayable<EnemyTypeBehaviour>)playable.GetInput(i);
                EnemyTypeBehaviour myPlayableBehaviour = scriptPlayable.GetBehaviour();
                myPlayableBehaviour.MarkReadyToSpawn();
            }
        }
    }
}

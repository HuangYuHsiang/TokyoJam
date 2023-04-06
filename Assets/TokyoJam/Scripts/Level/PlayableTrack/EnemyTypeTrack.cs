using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Sophon.TokyoJam.Timeline
{
    [TrackBindingType(typeof(EnemySpawnerController))]
    [TrackClipType(typeof(EnemyTypeClip))]
    [TrackClipType(typeof(SpawnEnemyClip))]
    public class EnemyTypeTrack : TrackAsset
    {
        public int _SpawnerIndex;
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<EnemyTypeTrackMixer>.Create(graph, inputCount);
        }
        protected override void OnCreateClip(TimelineClip clip)
        {
            AssignSpawnerIndex(clip);
        }
        private void OnValidate()
        {
            var clips = GetClips();

            foreach (var clip in clips)
            {
                OnCreateClip(clip);
            }
        }
        protected void AssignSpawnerIndex(TimelineClip clip)
        {
            if (clip.asset.GetType().IsAssignableFrom(typeof(EnemyTypeClip)))
            {
                EnemyTypeClip enemyTypeClip = (EnemyTypeClip)clip.asset;
                enemyTypeClip._SpawnerIndex = _SpawnerIndex;
            }
        }
    }
}

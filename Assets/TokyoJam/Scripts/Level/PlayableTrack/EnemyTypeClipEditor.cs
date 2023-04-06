using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Timeline;

namespace Sophon.TokyoJam.Timeline
{
    [CustomTimelineEditor(typeof(EnemyTypeClip))]
    public class EnemyTypeClipEditor : ClipEditor
    {
        public override void OnCreate(TimelineClip clip, TrackAsset track, TimelineClip clonedFrom)
        {
            
        }
        public override void DrawBackground(TimelineClip clip, ClipBackgroundRegion region)
        {
            EnemyTypeClip enemyTypeClip = (EnemyTypeClip)clip.asset;
            clip.displayName = enemyTypeClip._EnemyType.ToString();
        }
        public override ClipDrawOptions GetClipOptions(TimelineClip clip)
        {
            EnemyTypeClip enemyTypeClip = (EnemyTypeClip)clip.asset;

            var clipOption = base.GetClipOptions(clip);
            switch (enemyTypeClip._EnemyType)
            {
                case EnemySpawnerController.EnemyType.paper:
                    clipOption.highlightColor = Color.blue;
                    break;
                case EnemySpawnerController.EnemyType.rock:
                    clipOption.highlightColor = Color.yellow;
                    break;
                case EnemySpawnerController.EnemyType.scissors:
                    clipOption.highlightColor = Color.red;
                    break;
                default:
                    clipOption.highlightColor = Color.white;
                    break;
            }
            return clipOption;
        }
    }
}

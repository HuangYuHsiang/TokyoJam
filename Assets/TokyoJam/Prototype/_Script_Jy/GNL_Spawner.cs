using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GNLToolkit;
using Sirenix.OdinInspector;
using Sophon.TokyoJam.Level;
using Sophon.TokyoJam.ScriptableObjects;

namespace GNLToolkit.SpawnerSet
{
    public class GNL_Spawner : BaseSpawner
    {
        SpawnerDOTSet.GNL_SpawnerDOTween GetSpawnerCompoent;

        [Title("Spawn Method")]
        public bool SpawnOnEnable = true;

        //Set SpawnPrefab
        [Title("Set Prefab")]
        [SerializeField]
        GameObject SetSpawnPrefab;
        GameObject SpawnIns;
        
        [SerializeField]
        GameObject SetSpawnGroup;
        [SerializeField]
        float IntervalsTime;
        [EnumToggleButtons]
        public _SetPosEnum SetPosEnum;
        [ShowIf("SetPosEnum",_SetPosEnum.TargetPos)]
        [SerializeField]
        Transform SetEndPos;
        [ShowIf("SetPosEnum", _SetPosEnum.VectorPos)]
        [SerializeField]
        Vector3 EndVectorPos;
        [SerializeField]
        Vector3 OffsetPos;

        [Title("LookAt")]
        public bool IsLookAt;
        [EnableIf("IsLookAt")]
        [SerializeField]
        float LookDuring;


        //Set Start Scale
        [Title("StartScale")]
        [SerializeField]
        [MinMaxSlider(0.1f,5,true)]
        Vector2 PrefabRange = new Vector2(0.8f,1.5f);
        
        [SerializeField]
        float StartDuring;
        [SerializeField]
        float DelayTime;
        [EnumToggleButtons]
        public SetCurveEnum StartCurveEnum;
        [ShowIf("StartCurveEnum", SetCurveEnum.Custom)]
        [SerializeField]
        AnimationCurve StartScaleCurve;
        [ShowIf("StartCurveEnum", SetCurveEnum.DOT)]
        public Ease StartEase;
        //設定Duration時間 Ease
        [Title("Move")]
        [SerializeField]
        float MoveDuringTime;
        [EnumToggleButtons]
        public SetCurveEnum MoveCurveEnum;
        [ShowIf("MoveCurveEnum", SetCurveEnum.Custom)]
        [SerializeField]
        AnimationCurve MoveCurve;
        [ShowIf("MoveCurveEnum", SetCurveEnum.DOT)]
        public Ease MoveEase;
        

        //Set MoveToEnd
        [Title("EndScale")]
        [SerializeField]
        float EndDuring;
        [EnumToggleButtons]
        public SetCurveEnum EndCurveEnum;
        [ShowIf("EndCurveEnum", SetCurveEnum.Custom)]
        [SerializeField]
        AnimationCurve EndCurve;
        [ShowIf("EndCurveEnum", SetCurveEnum.DOT)]
        public Ease EndEase;

        int CheckCurveType;
        int CheckEndType;
        int CheckEnd;
        public enum _SetPosEnum { TargetPos, VectorPos };
        public enum SetCurveEnum { Custom,DOT };
        #region BaseSpawner
        public override void Init(Transform container) { }
        public override void SpawnEnemies(EnemiesData.Data enemy, float speed, float lifetime) 
        {
            SetSpawnPrefab = enemy.Prefab;
            SetStartInsScale();
        }
        public override void UntrackSpawnedObject(SpawnObjectMover objMover) { }
        #endregion
        private void OnEnable()
        {
            if(SpawnOnEnable)
                InvokeRepeating("SetStartInsScale", 0, IntervalsTime);
        }
        private void OnDisable()
        {
            if (SpawnOnEnable)
                CancelInvoke("SetStartInsScale");
        }

        private void SetStartInsScale()
        {
            //Instantiate Setting
            SpawnIns = Instantiate(SetSpawnPrefab, transform.position, transform.rotation, SetSpawnGroup.transform);
            SpawnIns.transform.localScale = Vector3.zero;

            SpawnIns.AddComponent<SpawnerDOTSet.GNL_SpawnerDOTween>();
            GetSpawnerCompoent = SpawnIns.GetComponent<SpawnerDOTSet.GNL_SpawnerDOTween>();

            //IsLookAt
            if (IsLookAt)
            {
                GetSpawnerCompoent.LookDOT(SpawnIns, LookDuring, SetEndPos);
            }

            
            //Set StartValue
            CheckCurveType = StartCurveEnum == SetCurveEnum.Custom ? 0 : 1;

            GetSpawnerCompoent.StartDOT(CheckCurveType, SpawnIns.gameObject, PrefabRange, StartDuring, StartScaleCurve, StartEase);
            
            SetMoveValue(GetSpawnerCompoent);
            
        }
        void SetMoveValue(SpawnerDOTSet.GNL_SpawnerDOTween GetSpawnerCompoent)
        {
            CheckEndType = SetPosEnum == _SetPosEnum.TargetPos ? 0 : 1;
            //GetSpawnerCompoent.MoveDOT(SpawnIns, SetEndPos, MoveDuringTime, StartDuring+DelayTime, EndDuring);
            if (CheckEndType == 0)
            {
                GetSpawnerCompoent.MoveDOT(CheckCurveType, SpawnIns, SetEndPos.position+OffsetPos, MoveDuringTime, StartDuring + DelayTime, MoveCurve, MoveEase, CheckEnd, EndCurve, EndEase, EndDuring);
            }
            else
            {
                GetSpawnerCompoent.MoveDOT(CheckCurveType, SpawnIns, EndVectorPos + OffsetPos, MoveDuringTime, StartDuring + DelayTime, MoveCurve, MoveEase, CheckEnd, EndCurve, EndEase, EndDuring);
            }
        }
    }

}

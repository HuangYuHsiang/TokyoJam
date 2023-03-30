using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GNLToolkit.SpawnerDOTSet
{
    
    public class GNL_SpawnerDOTween : MonoBehaviour
    {   
        [SerializeField]
        public struct SetEndValue
        {
            public GameObject _GetSpawnObj;
            public float _EndDuring;
            public int _CheckEnd;
            public AnimationCurve _EndCurve;
            public Ease _EndEase;
        }
        SetEndValue _SetEndValue;

        [SerializeField]
        bool IsDestroyTest;
        
        private Tweener CompleteTweener;


        public void LookDOT(GameObject SpawnIns,float LookDuring,Transform SetEndPos)
        {
            SpawnIns.transform.DOLookAt(SetEndPos.localPosition, LookDuring);
        }
        //public void StartDOT<T>(GameObject SpawnIns, Vector2 PrefabRange , float StartDuring, T StartCurveType)
        public void StartDOT(int CheckCurveType, GameObject SpawnIns, Vector2 PrefabRange, float StartDuring, AnimationCurve StartScaleCurve, Ease StartEase)
        {
            
            float RandomScale = Random.Range(PrefabRange.x, PrefabRange.y);
            if (CheckCurveType == 0)
            {
                SpawnIns.transform.DOScale(RandomScale, StartDuring).SetEase(StartScaleCurve);
            }
            else
            {
                SpawnIns.transform.DOScale(RandomScale, StartDuring).SetEase(StartEase);
            }

            //SpawnIns.transform.DOScale(RandomScale, StartDuring).SetEase(StartCurveType);


        }
        /// <summary>
        /// Curve控制往目標軸向
        /// </summary>
        /// <param name="CheckCurveType"></param>
        /// <param name="SpawnIns"></param>
        /// <param name="SetEndPos"></param>
        /// <param name="MoveDuringTime"></param>
        /// <param name="DelayTime"></param>
        /// <param name="StartScaleCurve"></param>
        /// <param name="MoveEase"></param>
        /// <param name="CheckEnd"></param>
        /// <param name="EndCurve"></param>
        /// <param name="EndEase"></param>
        /// <param name="EndDuring"></param>
        public void MoveDOT(int CheckCurveType,GameObject SpawnIns,Vector3 SetEndPos, float MoveDuringTime, float DelayTime, AnimationCurve StartScaleCurve, Ease MoveEase, int CheckEnd, AnimationCurve EndCurve, Ease EndEase,float EndDuring)
        {
            
            _SetEndValue._GetSpawnObj = SpawnIns;
            _SetEndValue._EndDuring = EndDuring;
            _SetEndValue._CheckEnd = CheckEnd;
            _SetEndValue._EndCurve = EndCurve;
            _SetEndValue._EndEase = EndEase;

            if (CheckCurveType == 0)
            {
                
                CompleteTweener = SpawnIns.transform.DOLocalMove(SetEndPos, MoveDuringTime).SetEase(StartScaleCurve).SetDelay(DelayTime).OnKill(EndDot);//.SetId<Tween>("PrefabMov"+ count);
            }
            else
            {
                CompleteTweener = SpawnIns.transform.DOLocalMove(SetEndPos, MoveDuringTime).SetEase(MoveEase).SetDelay(DelayTime).OnKill(EndDot);
            }
        }
        public void EndDot()
        {
            if (_SetEndValue._CheckEnd == 0)
            {
                _SetEndValue._GetSpawnObj.transform.DOScale(Vector3.zero, _SetEndValue._EndDuring).SetEase(_SetEndValue._EndCurve).OnKill(()=> { Destroy(gameObject); });
                //_SetEndValue._GetSpawnObj.transform.DOScale(Vector3.zero, _SetEndValue._EndDuring).SetEase(_SetEndValue._EndCurve).OnKill(SpawnerDestroy);
            }
            else
            {
                _SetEndValue._GetSpawnObj.transform.DOScale(Vector3.zero, _SetEndValue._EndDuring).SetEase(_SetEndValue._EndEase).OnKill(() => { Destroy(gameObject); });
            }
            
            
        }
        
        public void SpawnerDestroy()
        {
            this.transform.DOPause();

            this.transform.DOScale(new Vector3(0f, 0f, 0f), 0.65f).OnComplete(() =>
            {
                Destroy(this.gameObject);
            });
        }
        private void Update()
        {
            if (IsDestroyTest)
            {
                SpawnerDestroy();
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Interac"))
            {
                SpawnerDestroy();
            }
            
        }


    }

}


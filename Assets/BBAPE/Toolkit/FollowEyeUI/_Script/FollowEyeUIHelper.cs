using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BBAPE.Toolkit
{
    public class FollowEyeUIHelper : MonoBehaviour
    {
        [SerializeField]
        private Transform _toRotate;

        [SerializeField]
        private Transform _target;

        [SerializeField]
        private bool _lockXAxis;

        [SerializeField]
        private bool _lockYAxis;

        [SerializeField]
        private float rotationSmoothTime = 0.1f;

        protected virtual void Start()
        {
            this.AssertField(_toRotate, nameof(_toRotate));
            this.AssertField(_target, nameof(_target));
        }

        protected virtual void Update()
        {
            Vector3 dirToTarget = (_target.position - _toRotate.position).normalized;

            Quaternion targetRotation;

            if (!_lockYAxis && !_lockXAxis)
            {
                //_toRotate.LookAt(_toRotate.position - dirToTarget, Vector3.up); //不平滑

                targetRotation = Quaternion.LookRotation(-dirToTarget, Vector3.up);
                _toRotate.rotation = Quaternion.Slerp(_toRotate.rotation, targetRotation, rotationSmoothTime * Time.deltaTime); //平滑
                return;
            }

            if (_lockYAxis)            
                dirToTarget.y = 0; // 將 Y 設為 0，保持 Y 軸不轉

            if (_lockXAxis)
                dirToTarget.x = 0; // 將 X 設為 0，以保持 X 軸不轉

            targetRotation = Quaternion.LookRotation(-dirToTarget, Vector3.up); // 計算目標旋轉四元數

            _toRotate.rotation = targetRotation; // 將 _toRotate 的旋轉設置為目標旋轉 //不平滑
            //_toRotate.rotation = Quaternion.Slerp(_toRotate.rotation, targetRotation, rotationSmoothTime * Time.deltaTime); // 使用 Quaternion.Slerp 進行旋轉插值 //平滑

        }
    }
}
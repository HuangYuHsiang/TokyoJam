using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Sophon.TokyoJam.Level
{
    public class SpawnObjectMover : MonoBehaviour
    {
        private float _Speed;
        private float _Lifetime;
        private Vector3 _Dir;
        public Action<SpawnObjectMover> WhenDestroy;
        public void Init(Vector3 dir, float speed, float lifetime)
        {
            _Dir = dir;
            _Speed = speed;
            _Lifetime = lifetime;
            Destroy(gameObject, lifetime);
        }
        private void OnDestroy()
        {
            WhenDestroy?.Invoke(this);
        }
        private void Update()
        {
            transform.Translate(_Dir * _Speed * Time.deltaTime, Space.World);
        }
    }
}
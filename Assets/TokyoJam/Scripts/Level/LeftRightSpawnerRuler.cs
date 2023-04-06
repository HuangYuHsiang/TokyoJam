using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

namespace Sophon.TokyoJam.Level
{
    public class LeftRightSpawnerRuler : MonoBehaviour
    {
        public float Offset;
        [ReadOnly][SerializeField] private Transform L;
        [ReadOnly][SerializeField] private Transform R;
        private Transform[] points;
        private void OnValidate()
        {
            if (L == null || R == null) {
                GetRef();
                return; 
            }

            L.position = new Vector3( 
                transform.position.x - Offset / 2,
                transform.position.y, 
                transform.position.z);
            R.position = new Vector3(
                transform.position.x + Offset / 2,
                transform.position.y,
                transform.position.z);
        }
        public void GetRef() {
            points = transform.GetComponentsInChildren<Transform>();
            foreach (Transform t in points)
            {
                if (t.name == "L")
                    L = t;
                if (t.name == "R")
                    R = t;
            }
        }
        private void OnDrawGizmos()
        {
            if (L == null || R == null) return;
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(L.position, 0.2f);
            Gizmos.DrawWireSphere(R.position, 0.2f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

namespace Sophon.TokyoJam.Beat
{
    public class BeatEvent : MonoBehaviour,IBeatNotifier
    {
        public UltEvent WhenBeatNotify;
        private void OnEnable()
        {
            AddToBeatSubject();
        }
        private void OnDisable()
        {
            RemoveFromBeatSubject();
        }
        public void AddToBeatSubject()
        {
            BeatSubject.Instance.AddListener(this);
        }
        public void RemoveFromBeatSubject()
        {
            BeatSubject.Instance.RemoveListener(this);
        }

        public void OnBeatNotify()
        {
            WhenBeatNotify?.Invoke();
        }

        
    }
}

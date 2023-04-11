using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelloPico2.InteractableObjects;
using DG.Tweening.Core.Easing;
using static HelloPico2.InteractableObjects.ObjectScaler;
using UnityEngine.Rendering.Universal;

namespace Sophon.TokyoJam.Beat
{
    public class BeatScaler : ObjectScaler, IBeatNotifier
    {
        public BeatScaler(ControlMode controlMode, Vector3 from, Vector3 to, float duration, int loop, AnimationCurve easecurve) : base(controlMode, from, to, duration, loop, easecurve)
        {
        }
        #region BeatInterface
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
            StartScaling();
        }
        #endregion
        private void OnEnable()
        {
            AddToBeatSubject();
        }
        private void OnDisable()
        {
            RemoveFromBeatSubject();
        }
    }
}

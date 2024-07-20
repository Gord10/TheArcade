using System;
using DG.Tweening;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace ArcadeShared
{
    public class CameraShake : MonoBehaviour
    {
        public float duration = 0.3f;
        public float strength = 1;
        public void Shake()
        {
            transform.DOShakePosition(duration, strength);
        }

        private void Update()
        {
            #if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Shake();
            }
            #endif
        }
    }
}

using System;
using DG.Tweening;
using UnityEngine;

namespace ArcadeShared
{
    public class RotatingObject : MonoBehaviour
    {
        public Vector3 rotateSpeed;


        private void Update()
        {
            transform.Rotate(rotateSpeed * Time.deltaTime);
        }
    }
}

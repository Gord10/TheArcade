using System;
using UnityEngine;

namespace Arena
{
    public class Trap : MonoBehaviour
    {
        public bool CanHarm => canHarm;
        private bool canHarm = true;
        private Animator animator;
        private static readonly int Open1 = Animator.StringToHash("Open");

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Open()
        {
            animator.SetTrigger(Open1);
            canHarm = false;
        }
        
    }
}

using System;
using UnityEngine;

namespace Arena
{
    public class BaseCharacter : MonoBehaviour
    {
        public float movementSpeed = 1;
        public float rotateSpeed = 400;
        
        protected CharacterController characterController;
        protected Animator animator; //Character can attack/move when this counter reaches 0
        protected GameManager gameManager;
        
        private static readonly int Attack1 = Animator.StringToHash("Attack");
        protected static readonly int Walking = Animator.StringToHash("Walking");
        protected static readonly int Hit = Animator.StringToHash("Hit");

        protected void Awake()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            gameManager = FindAnyObjectByType<GameManager>();
        }
        public void Fall()
        {
            
        }

        public void StopMovement()
        {
            animator.SetBool(Walking, false);
        }


    }
}

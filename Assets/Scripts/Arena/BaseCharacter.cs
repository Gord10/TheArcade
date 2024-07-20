using System;
using UnityEngine;

namespace Arena
{
    public class BaseCharacter : MonoBehaviour
    {
        public float movementSpeed = 1;
        public float rotateSpeed = 400;
        public float attackCooldown = 0.2f;
        
        protected CharacterController characterController;
        protected Animator animator;
        protected float attackCooldownCounter = 0; //Character can attack/move when this counter reaches 0
        protected GameManager gameManager;
        
        private static readonly int Attack1 = Animator.StringToHash("Attack");
        protected static readonly int Walking = Animator.StringToHash("Walking");
        private static readonly int Hit = Animator.StringToHash("Hit");

        protected void Awake()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            gameManager = FindAnyObjectByType<GameManager>();
        }

        protected void Attack()
        {
            animator.SetTrigger(Attack1);
            //animator.SetBool(Walking, false);
            attackCooldownCounter = attackCooldown;
        }

        protected virtual void Update()
        {
            if (attackCooldownCounter > 0)
            {
                attackCooldownCounter -= Time.deltaTime;
                //print(attackCooldownCounter);
            }
        }

        public void OnAttackEnd()
        {
            print("Attack end");
        }

        public void Fall()
        {
            
        }

        public void OnHit()
        {
            animator.SetTrigger(Hit);
        }
    }
}

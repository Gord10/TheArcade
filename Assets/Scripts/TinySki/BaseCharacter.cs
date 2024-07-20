using System;
using UnityEngine;

namespace TinySki
{
    public class BaseCharacter : MonoBehaviour
    {
        public float speed = 2;
        protected Rigidbody2D rigidbody2D;
        protected Animator animator;
        protected AudioSource audio;

        protected Vector2 desiredMovement;
        protected GameManager gameManager;
        protected bool didReachFinish = false;
    
        protected virtual void Awake()
        {
            desiredMovement = Vector2.zero;
            rigidbody2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            gameManager = FindAnyObjectByType<GameManager>();
            audio = GetComponent<AudioSource>();
        }

        private void FixedUpdate()
        {
            if (didReachFinish)
            {
                rigidbody2D.velocity *= 0.96f; //Gradually slow down
            }

            if (gameManager && gameManager.gameState == GameManager.GameState.InGame)
            {
                rigidbody2D.AddForce(desiredMovement, ForceMode2D.Force);
            }
            /*if (gameManager)
            {
                switch (gameManager.gameState)
                {
                    case GameManager.GameState.Success:
                        rigidbody2D.velocity *= 0.96f; //Gradually slow down
                        break;
                    
                    case GameManager.GameState.InGame:
                        rigidbody2D.AddForce(desiredMovement, ForceMode2D.Force);
                        break;
                }
            }*/
        }
    }
}


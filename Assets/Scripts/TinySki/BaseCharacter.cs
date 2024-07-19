using System;
using UnityEngine;

namespace TinySki
{
    public class BaseCharacter : MonoBehaviour
    {
        public float speed = 2;
        protected Rigidbody2D rigidbody2D;

        protected Vector2 desiredMovement;
        protected GameManager gameManager;
    
        protected virtual void Awake()
        {
            desiredMovement = Vector2.zero;
            rigidbody2D = GetComponent<Rigidbody2D>();
            gameManager = FindAnyObjectByType<GameManager>();
        }

        private void FixedUpdate()
        {
            if (gameManager && gameManager.gameState != GameManager.GameState.InGame)
            {
                return;
            }
            
            rigidbody2D.AddForce(desiredMovement, ForceMode2D.Force);
        }
    }
}


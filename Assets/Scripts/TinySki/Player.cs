using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using ArcadeShared;

namespace TinySki
{
    public class Player : BaseCharacter
    {
        public bool isNpc = false;

        public float npcObstacleAvoidance = 0.2f;
        public float desiredMinDistanceFromPlayer = 0.2f; //NPC will use this value to know when to avoid the player
        public float yCatchUpThreshold = 1f; //If y difference between the friend is higher than this, then NPC will try to catch up with the friend
        public AudioSource skiAudio;
        private CameraShake cameraShake;
        
        private Player friend;

        protected override void Awake()
        {
            base.Awake();
            cameraShake = FindAnyObjectByType<CameraShake>();
            desiredMovement = new Vector2();
            
            Player[] players = FindObjectsByType<Player>(FindObjectsSortMode.None);
            int i;
            for (i = 0; i < players.Length; i++)
            {
                if (players[i] != this)
                {
                    friend = players[i];
                }
            }

            if (isNpc)
            {
                StartCoroutine(NpcDecisions(0.05f));
            }
        }

        public void Say(string speech, float appearanceTime)
        {
            speechText.Say(speech, appearanceTime);
        }
        
        void DecideMovement()
        {
            desiredMovement = new Vector2();
            //print("Decide movement");
            GameObject nextObstacle = gameManager.GetNextObstacleForPlayer(this);
            if (nextObstacle)
            {
                float x = transform.position.x;
                float diffXFromNextObstacle = nextObstacle.transform.position.x - x;
                if (Math.Abs(diffXFromNextObstacle) < npcObstacleAvoidance)
                {
                    float safeZoneHalfLength = 1f;
                    if (Mathf.Abs(x) < safeZoneHalfLength) //Skier is not too close to the game edge 
                    {
                        desiredMovement.x = -Math.Sign(diffXFromNextObstacle);
                    }
                    else //Skier is too close to the game edge
                    {
                        desiredMovement.x = -Mathf.Sign(x); //Move towards the center of the screen
                    }
                        
                    //But don't get too close to your sibling
                    Vector3 diffFromFriend = friend.transform.position - transform.position;
                    if (diffFromFriend.magnitude < desiredMinDistanceFromPlayer)
                    {
                        if(Mathf.Approximately(Mathf.Sign(diffFromFriend.x),  Mathf.Sign(desiredMovement.x)))
                        {
                            print("Avoiding player");
                            desiredMovement.x = 0;
                        }
                    }
                    
                    //print($"Avoid obstacle! Desired movement x: {desiredMovement.x}");
                }

                float yDiffWithFriend = friend.transform.position.y - transform.position.y;
                if (Mathf.Abs(yDiffWithFriend) > yCatchUpThreshold)
                {
                    desiredMovement.y = Mathf.Sign(yDiffWithFriend);
                }
            }

            desiredMovement = Vector3.ClampMagnitude(desiredMovement, 1);
            desiredMovement *= speed;

            desiredMovement += gameManager.gravity;
            //print(desiredMovement);
        }

        IEnumerator NpcDecisions(float interval)
        {
            WaitForSeconds wait = new WaitForSeconds(interval);

            while (true)
            {
                DecideMovement();
                yield return wait;
            }
        }

        // Update is called once per frame
        private void Update()
        {
            if (!isNpc)
            {
                desiredMovement = new Vector2();
                
                desiredMovement.x = Input.GetAxis("Horizontal");
                desiredMovement.y = Input.GetAxis("Vertical");
                desiredMovement = Vector2.ClampMagnitude(desiredMovement, 1);
                desiredMovement *= speed;

                desiredMovement += gameManager.gravity;
            }

            if (transform.position.y < gameManager.finishLineY)
            {
                didReachFinish = true;
                gameManager.OnSuccess();
            }

            if (rigidbody2D.velocity.y < 0)
            {
                animator.speed = 1f * -rigidbody2D.velocity.y;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Obstacle"))
            {
                audio.volume = other.relativeVelocity.magnitude;
                audio.pitch = Random.Range(0.95f, 1.05f);
                audio.Play();
                if (!isNpc)
                {
                    cameraShake.Shake();
                }
            }
        }

        public void PlaySkiSound()
        {
            skiAudio.pitch = Random.Range(0.9f, 1.1f);
            skiAudio.Play();
            print("Ski sound");
        }
    }

}

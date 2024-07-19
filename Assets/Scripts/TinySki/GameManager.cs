using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using ArcadeShared;

namespace TinySki
{
    public class GameManager : MonoBehaviour
    {
        public Vector2 gravity; //This value will be added to desiredMovement.y of skiers
        public float finishLineY = -30;
        public int timeLimit = 60;

        public enum GameState
        {
            Title,
            InGame,
            Success,
            Fail
        }

        public GameState gameState = GameState.Title;
        
        [Header("Obstacles")]
        public GameObject[] obstaclePrefabs;

        public int obstacleAmount = 30;
        public int maxObstacleX = 100; //This will be divided to 100 later
        public float obstacleYSpace = 1f;

        [Header("Flags")]
        public GameObject blueFlagPrefab;

        public GameObject redFlagPrefab;
        public float flagSpawnX = 2f;
        public float flagYSpace = 0.32f;

        public Player player;
        public Player npc;

        private GameObject[] obstacles;
        private GameUi gameUi;
        private DialogueUi dialogueUi;
        
        private void Awake()
        {
            int i;

            float flagY = 0;
            Vector3 flagPosition = new Vector3();
            
            while (flagY > finishLineY)
            {
                flagPosition.x = flagSpawnX;
                flagPosition.y = flagY;
                Instantiate(blueFlagPrefab, flagPosition, Quaternion.identity, transform);

                flagPosition.x *= -1;
                Instantiate(blueFlagPrefab, flagPosition, Quaternion.identity, transform);
                flagY -= flagYSpace;
            }

            flagPosition.x = flagSpawnX;
            flagPosition.y = flagY;
            Instantiate(redFlagPrefab, flagPosition, Quaternion.identity, transform);
            
            flagPosition.x = -flagSpawnX;
            Instantiate(redFlagPrefab, flagPosition, Quaternion.identity, transform);
            

            obstacles = new GameObject[obstacleAmount];

            for (i = 0; i < obstacleAmount; i++)
            {
                Vector3 obstaclePosition = new Vector3();
                obstaclePosition.x = Random.Range(-maxObstacleX, maxObstacleX) / 100f;
                obstaclePosition.y = (-i - 3) * obstacleYSpace;
                GameObject randomObstacle = obstaclePrefabs[0];
                obstacles[i] = Instantiate(randomObstacle, obstaclePosition, Quaternion.identity, transform);
            }

            gameUi = FindAnyObjectByType<GameUi>();
            dialogueUi = FindAnyObjectByType<DialogueUi>();
            dialogueUi.gameObject.SetActive(false);
            SetGameState(GameState.Title);
        }

        //NPC will try to avoid the next obstacle. They need to know the position of the next one for that.
        public GameObject GetNextObstacleForPlayer(Player player)
        {
            if (obstacles == null)
            {
                return null;
            }
            
            float playerY = player.transform.position.y;
            int i;
            
            for (i = 0; i < obstacles.Length; i++)
            {
                if (obstacles[i].transform.position.y < playerY)
                {
                    return obstacles[i];
                }
            }

            return null;
        }

        private void Update()
        {
            if (gameState == GameState.Title && Input.anyKeyDown)
            {
                SetGameState(GameState.InGame);
                StartCoroutine(StartCountDown());
            }
        }

        void SetGameState(GameState newGameState)
        {
            gameState = newGameState;
            gameUi.SetScreen(gameState);
        }

        IEnumerator StartCountDown()
        {
            WaitForSeconds wait = new WaitForSeconds(1);
            while (gameState == GameState.InGame)
            {
                timeLimit--;
                gameUi.ShowCountdown(timeLimit);
                
                if (timeLimit == 58)
                {
                    player.Say("Here we go!", 3);
                }
                
                if (timeLimit == 50)
                {
                    npc.Say("I guess I remember this game.", 3);
                }

                if (timeLimit == 40)
                {
                    npc.Say("Wasn't something supposed to chase us?", 3);
                }
                if (timeLimit == 35)
                {
                    npc.Say("Like Barefoot?", 2);
                }
                if (timeLimit == 32)
                {
                    player.Say("No, that was another game!", 3);
                }
                
                if (timeLimit == 22)
                {
                    player.Say("Also, it's Bigfoot.", 3);
                }
                
                if (timeLimit == 10)
                {
                    player.Say("We are almost there!", 3);
                }
                
                if (timeLimit <= 0)
                {
                    OnFail();
                }
                print(timeLimit);
                yield return wait;
                
            }
        }

        public void OnSuccess()
        {
            if (gameState == GameState.InGame)
            {
                SetGameState(GameState.Success);
                print("Success!");
                StopCoroutine(StartCountDown());
            }
        }

        public void OnFail()
        {
            if (gameState == GameState.InGame)
            {
                SetGameState(GameState.Fail);
                print("Success!");
                StopCoroutine(StartCountDown());
            }
        }
    }
}


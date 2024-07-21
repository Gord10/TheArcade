using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using ArcadeShared;

namespace Arena
{
    public class GameManager : MonoBehaviour
    {
        public Gladiator[] gladiators;

        public float throwInterval = 2f;
        public Throwable[] throwablePrefabs;
        public Transform[] throwableSpawnPoints;
        public float throwSpeed = 10;
        public int timeLimit = 60;
        public int lives = 3;

        public Throwable latestThrowable;

        public GameState gameState;
        private ArcadeGameUi gameUi;

        private CharacterSpeech girlSpeech, boySpeech;

        private void Awake()
        {
            gameUi = FindAnyObjectByType<ArcadeGameUi>();
            gameUi.ShowLives(lives);
            SetGameState(GameState.Title);
            girlSpeech = gladiators[0].speech;
            boySpeech = gladiators[1].speech;
            //StartCoroutine(ThrowItemsToGladiators());

        }

        void SetGameState(GameState newGameState)
        {
            gameState = newGameState;
            gameUi.SetScreen(newGameState);
        }

        public Vector3 GetRandomPointInCenter()
        {
            Vector3 pos = new Vector3();
            pos.x = Random.Range(-2f, 2f);
            pos.z =Random.Range(-2f, 2f);
            return pos;
        }

        IEnumerator ThrowItemsToGladiators()
        {
            WaitForSeconds wait = new WaitForSeconds(throwInterval);
            yield return wait;
            
            while (gameState == GameState.InGame && timeLimit > 4)
            {   
                int spawnPointIndex = Random.Range(0, throwableSpawnPoints.Length);
                Transform spawnTransform = throwableSpawnPoints[spawnPointIndex];
                Vector3 spawnPoint = spawnTransform.position;

                int randomThrowableIndex = Random.Range(0, throwablePrefabs.Length);
                Throwable throwablePrefab = throwablePrefabs[randomThrowableIndex];
                Throwable throwable = Instantiate(throwablePrefab, spawnPoint, Quaternion.identity);

                int targetGladiatorIndex = Random.Range(0, gladiators.Length);
                Gladiator targetGladiator = gladiators[targetGladiatorIndex];
                Vector3 targetPos = targetGladiator.targetPoint.position;
                targetPos.x += Random.Range(-0.05f, 0.05f);
                
                throwable.Throw(targetPos, throwSpeed);
                latestThrowable = throwable;
                
                yield return wait;
            }
        }

        IEnumerator Countdown()
        {
            WaitForSeconds wait = new WaitForSeconds(1);
            while (timeLimit > 0 && gameState == GameState.InGame)
            {
                gameUi.ShowCountdown(timeLimit);
                yield return wait;
                timeLimit--;

                switch (timeLimit)
                {
                    case 56:
                        boySpeech.Say("No idea.", 3f);
                        break;
                        
                    case 52:
                        boySpeech.Say("This doesn't look like an arcade game, anyway.", 5f);
                        break;
                    
                    case 46:
                        girlSpeech.Say("Technology advanced so far.", 4f);
                        break;
                    
                    case 26:
                        girlSpeech.Say("How's dad holding?", 3f);
                        break;
                    
                    case 22:
                        boySpeech.Say("As usual.", 3f);
                        break;
                    
                    case 18:
                        boySpeech.Say("How about mom?", 3f);
                        break;
                    
                    case 14:
                        girlSpeech.Say("Same. But she will get better.", 3f);
                        break;
                    
                    case 8:
                        girlSpeech.Say("Maybe this was for the best.", 4f);
                        break;
                }
            }

            if (timeLimit <= 0 && gameState == GameState.InGame)
            {
                SetGameState(GameState.Success);
                gladiators[0].StopMovement();
                gladiators[1].StopMovement();
                ArcadeSceneManager.OnGameSuccess();
            }
        }

        private void Update()
        {
            if (gameState == GameState.Title && Time.timeSinceLevelLoad > 0.3f && Input.anyKeyDown)
            {
                SetGameState(GameState.InGame);
                StartCoroutine(ThrowItemsToGladiators());
                StartCoroutine(Countdown());
                girlSpeech.Say("Why do they always use \"V\" as \"U\" in Roman texts?", 3f);
            }
            
            #if CHEAT_ENABLED

            #endif
        }

        public void OnPlayerHit(Gladiator hitGladiator)
        {
            if (gameState == GameState.InGame)
            {
                lives--;
                gameUi.ShowLives(lives);
                if (lives <= 0)
                {
                    StopCoroutine(ThrowItemsToGladiators());
                    StopCoroutine(Countdown());
                    SetGameState(GameState.Fail);
                    hitGladiator.Fall();
                    gladiators[0].StopMovement();
                    gladiators[1].StopMovement();
                }
            }
        }
    }
}

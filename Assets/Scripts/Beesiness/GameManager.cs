using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using ArcadeShared;
public class GameManager : MonoBehaviour
{
    public GameObject[] spawnObjectPrefabs;
    public int spawnObjectNum = 50;
    public float spawnObjectMinX = 1.5f;
    public float spawnObjectMaxX = 15f;
    
    public Transform spawnedObjectsParent;
    public float gravity = 0.1f;

    public float worldMovementBaseSpeed = 0.3f;
    public int countdown = 60;
    
    public float pollen = 0;
    public float targetPollen = 10;

    public CharacterSpeech playerSpeech;
    public CharacterSpeech npcSpeech;
    
    private ArcadeGameUi gameUi;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameState gameState;
    
    private void Awake()
    {
        int i;
        for (i = 0; i < spawnObjectNum; i++)
        {
            Vector3 position = new Vector3();
            position.x = Random.Range(spawnObjectMinX, spawnObjectMaxX);

            int objectIndex = Random.Range(0, spawnObjectPrefabs.Length);
            GameObject prefabToSpawn = spawnObjectPrefabs[objectIndex];
            position.y = prefabToSpawn.transform.position.y;
            Instantiate(prefabToSpawn, position, Quaternion.identity, spawnedObjectsParent);
        }

        gameUi = FindAnyObjectByType<ArcadeGameUi>();
        gameUi.UpdateProgressBar(pollen, targetPollen);

        SetGameState(GameState.Title);
        audioSource = GetComponent<AudioSource>();
        Music.Instance.PlayBeesiness();
    }

    void StartGame()
    {
        SetGameState(GameState.InGame);
        StartCoroutine(StartCountdown());
    }

    void SetGameState(GameState newGameState)
    {
        gameState = newGameState;
        gameUi.SetScreen(gameState);
    }

    void Success()
    {
        SetGameState(GameState.Success);
        StopCoroutine(StartCountdown());
        audioSource.Play();
        ArcadeSceneManager.OnGameSuccess();
    }

    private void Update()
    {
        if (gameState == GameState.Title)
        {
            if (Input.anyKeyDown && Time.timeSinceLevelLoad > 1)
            {
                StartGame();
            }
        }
        else if(gameState == GameState.InGame)
        {
            float worldMovementSpeed = worldMovementBaseSpeed;
            worldMovementSpeed += Input.GetAxis("Horizontal") * 0.1f;
            spawnedObjectsParent.transform.position +=
                Vector3.left * (Time.deltaTime * worldMovementSpeed);
        }
    }

    public void Fail()
    {
        SetGameState(GameState.Fail);
    }

    public void IncreasePollen(float increace)
    {
        pollen += increace;
        if (pollen >= targetPollen && gameState == GameState.InGame)
        {
            Success();
        }
        
        pollen = Mathf.Clamp(pollen, 0, targetPollen);
        gameUi.UpdateProgressBar(pollen, targetPollen);
    }

    IEnumerator StartCountdown()
    {
        gameUi.ShowCountdown(countdown);
        WaitForSeconds wait = new WaitForSeconds(1);
        while (countdown > 0)
        {
            yield return wait;
            countdown--;

            switch (countdown)
            {
                case 58:
                    npcSpeech.Say("We collect pollen.", 3f);
                    break;
                    
                case 55:
                    playerSpeech.Say("Hmm...", 1);
                    break;
                
                case 47:
                    npcSpeech.Say("Don't touch the mushrooms.", 2f);
                    break;
                
                case 45:
                    npcSpeech.Say("We lose pollen then.", 2f);
                    break;
                
                case 43:
                    playerSpeech.Say("Why?", 1.5f);
                    break;
                
                case 41:
                    playerSpeech.Say("That's dumb.", 2f);
                    break;
                
                case 36:
                    npcSpeech.Say("Maybe to make the game *actually* hard?", 3f);
                    break;
                
                case 32:
                    playerSpeech.Say("Makes sense.", 2f);
                    break;
                
                case 28:
                    playerSpeech.Say("How's dad holding?", 3f);
                    break;
                    
                case 25:
                    npcSpeech.Say("As usual.", 3f);
                    break;
                    
                case 22:
                    npcSpeech.Say("How about mom?", 3f);
                    break;
                    
                case 19:
                    playerSpeech.Say("Same. But she will get better.", 3f);
                    break;
                    
                case 15:
                    playerSpeech.Say("Maybe this was for the best.", 4f);
                    break;
            }
            
            gameUi.ShowCountdown(countdown);
        }

        if (gameState == GameState.InGame)
        {
            Fail();
        }
    }
}

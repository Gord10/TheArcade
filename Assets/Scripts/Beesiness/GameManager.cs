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
    private ArcadeGameUi gameUi;

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
            gameUi.ShowCountdown(countdown);
        }

        if (gameState == GameState.InGame)
        {
            Fail();
        }
    }
}

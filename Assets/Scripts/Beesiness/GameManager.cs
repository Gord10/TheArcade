using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public GameObject[] spawnObjectPrefabs;
    public int spawnObjectNum = 50;
    public float spawnObjectMinX = 1.5f;
    public float spawnObjectMaxX = 15f;
    
    public Transform spawnedObjectsParent;
    public float gravity = 0.1f;

    public float worldMovementBaseSpeed = 0.3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

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
    }

    private void Update()
    {
        float worldMovementSpeed = worldMovementBaseSpeed;
        worldMovementSpeed += Input.GetAxis("Horizontal") * 0.1f;
        spawnedObjectsParent.transform.position +=
            Vector3.left * (Time.deltaTime * worldMovementSpeed);
        
        
    }
}

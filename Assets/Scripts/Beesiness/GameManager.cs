using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform spawnedObjectsParent;
    public float spawnedObjectsMovementInterval = 0.1f;
    public float gravity = 0.1f;

    public float spawnedObjectsMovementSpeed = 0.01f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        //StartCoroutine(MoveSpawnedObjects());
    }

    IEnumerator MoveSpawnedObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnedObjectsMovementInterval);
            spawnedObjectsParent.transform.position += Vector3.left * 0.01f;
        }
    }

    private void Update()
    {
        spawnedObjectsParent.transform.position +=
            Vector3.left * (Time.deltaTime * spawnedObjectsMovementSpeed);
    }
}

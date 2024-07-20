using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBug : MonoBehaviour
{
    public float maxY = 0;
    public float sinSpeed = 3;
    public float xSpeed = 0.4f;
    private float startY = 0;

    private void Awake()
    {
        startY = transform.position.y;
        maxY -= Random.Range(0f, 0.1f);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.Sin(Time.timeSinceLevelLoad * sinSpeed);
        t = Mathf.Abs(t);
        float y = Mathf.Lerp(startY, maxY, t);

        Vector3 localPosition = transform.localPosition;
        localPosition.x -= xSpeed * Time.deltaTime;
        localPosition.y = y;
        transform.localPosition = localPosition;
    }
}

using System;
using UnityEngine;

public class Bee : MonoBehaviour
{
    public float upSpeed = 0.4f;
    private GameManager gameManager;
    private float y;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        y = transform.position.y;
    }

    private float yVelocity = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float yMovement = -gameManager.gravity;
        yMovement += (Input.GetAxis("Vertical")) * upSpeed;
        y += yMovement * Time.deltaTime;

        Vector3 position = transform.position;
        position.y = y;
        //position.y = (float) (((int)(y * 100)) / 100.0f);
        transform.position = position;
        //transform.position += Vector3.up * (yMovement * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("PollenEfector") && other.TryGetComponent(out PollenEfector pollenEfector))
        {
            gameManager.IncreasePollen(pollenEfector.pollenPerSecond * Time.fixedDeltaTime);
        }
    }
}

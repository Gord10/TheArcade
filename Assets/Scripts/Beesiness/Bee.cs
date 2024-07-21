using System;
using ArcadeShared;
using UnityEngine;

public class Bee : MonoBehaviour
{
    public float upSpeed = 0.4f;
    public float yFailThreshold = -0.5f; //Player loses the game if they are below this
    public bool isNpc = false;
    public AudioSource pickUpAudio;
    public AudioSource losePollenAudio;
    
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
        if (gameManager.gameState == GameState.InGame)
        {
            float yMovement = -gameManager.gravity;

            if (isNpc)
            {
                yMovement = 0;
            }
            else
            {
                yMovement += (Input.GetAxis("Vertical")) * upSpeed;
            }
            
            
            transform.position += Vector3.up * (yMovement * Time.deltaTime);

            if (transform.position.y < yFailThreshold )
            {
                gameManager.Fail();
            }
            
            /*y += yMovement * Time.deltaTime;

            Vector3 position = transform.position;
            position.y = y;
            transform.position = position;*/
        }
        else if (gameManager.gameState == GameState.Success)
        {
            Vector3 up = new Vector3(0.2f, 1, 0);
            transform.position += up * (upSpeed * Time.deltaTime);
        }
        else if (gameManager.gameState == GameState.Fail)
        {
            Vector3 down = new Vector3(0.2f, -1, 0);
            transform.position += down * (upSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("PollenEfector") && other.TryGetComponent(out PollenEfector pollenEfector))
        {
            float pollenPerSecond = pollenEfector.pollenPerSecond;
            gameManager.IncreasePollen(pollenPerSecond * Time.fixedDeltaTime);
            if (pollenPerSecond> 0 && pickUpAudio && !pickUpAudio.isPlaying)
            {
                pickUpAudio.Play();
            }
            else if (pollenPerSecond < 0 && losePollenAudio && !losePollenAudio.isPlaying)
            {
                losePollenAudio.Play();
            }
        }
    }
}

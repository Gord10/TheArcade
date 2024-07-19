using System;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public float rotationSpeed = 10;
    public bool isNpc = false;

    private NavMeshAgent navMeshAgent;
    private CharacterController characterController;
    private Animator animator;
    private static readonly int Walking = Animator.StringToHash("Walking");
    private Player friendPlayer;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        Player[] players = FindObjectsByType<Player>(FindObjectsSortMode.None);
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != this)
            {
                friendPlayer = players[i];
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = false;
        if (!isNpc)
        {
            Vector3 movement = new Vector3();
            movement.x = Input.GetAxis("Horizontal");
            movement.z = Input.GetAxis("Vertical");
            movement = Vector3.ClampMagnitude(movement, 1);

            isWalking = movement.sqrMagnitude > 0;
            
            characterController.Move(movement * Time.deltaTime);
        
            if (movement.sqrMagnitude > 0.2f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(movement, Vector3.up),
                    Time.deltaTime * rotationSpeed);
            }
        }
        else if(navMeshAgent && friendPlayer)
        {
            navMeshAgent.SetDestination(friendPlayer.transform.position);
            isWalking = navMeshAgent.velocity.sqrMagnitude > 0;
        }
        
        animator.SetBool(Walking, isWalking);

    }
}

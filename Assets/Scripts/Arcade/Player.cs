using System;
using UnityEngine;
using UnityEngine.AI;

namespace ArcadeHouse
{
    public class Player : MonoBehaviour
{
    public float rotationSpeed = 10;
    public bool isNpc = false;
    public float movementSpeed = 1.5f;

    private NavMeshAgent navMeshAgent;
    private CharacterController characterController;
    private Animator animator;
    private static readonly int Walking = Animator.StringToHash("Walking");
    private Player friendPlayer;

    private ArcadeMachine chosenArcadeMachine = null; //if null, none wasn't chosen
    
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
        bool willWalkingAnimationPlay = false;
        Quaternion targetRotation = transform.rotation;
        
        if (!isNpc)
        {
            Vector3 movement = new Vector3();

            if (!chosenArcadeMachine)
            {
                movement.x = Input.GetAxis("Horizontal");
                movement.z = Input.GetAxis("Vertical");
                movement = Vector3.ClampMagnitude(movement, 1);

                willWalkingAnimationPlay = movement.sqrMagnitude > 0.2f;
                
            }
            else
            {
                Vector3 lookDirection = chosenArcadeMachine.transform.position - transform.position;
                lookDirection.y = 0;
                targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            }
            
            if (movement.sqrMagnitude > 0.2f)
            {
                targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            }
            
            movement.y = -1;
            characterController.Move(movement * (Time.deltaTime * movementSpeed));

        }
        else if(navMeshAgent && friendPlayer)
        {
            Vector3 targetPos = (friendPlayer.chosenArcadeMachine)
                ? friendPlayer.chosenArcadeMachine.transform.position
                : friendPlayer.transform.position;
            
            navMeshAgent.SetDestination(targetPos);
            willWalkingAnimationPlay = navMeshAgent.velocity.sqrMagnitude > 0;
        }
        
        animator.SetBool(Walking, willWalkingAnimationPlay);
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation,
            Time.deltaTime * rotationSpeed);

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!isNpc && hit.collider.CompareTag("ArcadeMachine"))
        {
            if(hit.collider.TryGetComponent(out ArcadeMachine arcadeMachine))
            {
                chosenArcadeMachine = arcadeMachine;
                chosenArcadeMachine.Choose();
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        print(other.collider.ToString());
        if (other.collider.CompareTag("ArcadeMachine"))
        {
            if(other.collider.TryGetComponent(out ArcadeMachine arcadeMachine))
            {
                arcadeMachine.Choose();
            }
        }
    }
}

}

using ArcadeShared;
using UnityEngine;

namespace Arena
{
    public class Player : Gladiator
    {
        // Update is called once per frame
        protected void Update()
        {
            if (gameManager.gameState != GameState.InGame)
            {
                return;
            }
            
            Vector3 movement = new Vector3();
            movement.x = Input.GetAxis("Horizontal");
            movement.z = Input.GetAxis("Vertical");
            movement = Vector3.ClampMagnitude(movement, 1);

            if (movement.magnitude > 0.3f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movement);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            }
            animator.SetBool(Walking, movement.sqrMagnitude > 0);
            characterController.Move(movement * (movementSpeed * Time.deltaTime));
            movement.y = -10f;
        }
    }
}

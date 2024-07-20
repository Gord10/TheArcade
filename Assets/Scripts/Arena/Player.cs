using UnityEngine;

namespace Arena
{
    public class Player : Gladiator
    {
        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
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
            movement.y = -10f;
            if (attackCooldownCounter <= 0)
            {
                //transform.forward = movement;

                characterController.Move(movement * (movementSpeed * Time.deltaTime));

                if (Input.GetKeyDown(KeyCode.Z))
                {
                    Attack();
                }
            }

        }
    }
}
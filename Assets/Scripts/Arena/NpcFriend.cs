using UnityEngine;

namespace Arena
{
    public class NpcFriend : Gladiator
    {
        private bool isRunningFromThrown = false;
        private Vector3 randomPointInArena;
        
        protected override void Update()
        {
            base.Update();
            Vector3 movementDirection = new Vector3();
            if (gameManager)
            {
                if (gameManager.latestThrowable && gameManager.latestThrowable.CanHurtPlayer())
                {
                    movementDirection = transform.position - gameManager.latestThrowable.transform.position;

                    isRunningFromThrown = true;
                }
                else
                {
                    if (isRunningFromThrown)
                    {
                        randomPointInArena = gameManager.GetRandomPointInCenter();
                        
                        isRunningFromThrown = false;
                    }

                    movementDirection = randomPointInArena - transform.position;
                }
            }
            
            movementDirection.y = 0;
            movementDirection = Vector3.ClampMagnitude(movementDirection, 1);
            Vector3 movement = movementDirection * (Time.deltaTime * movementSpeed);
            
            characterController.Move(movement);
            animator.SetBool(Walking, movementDirection.sqrMagnitude > 0.02f);

            if (movement.sqrMagnitude > 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movement);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            }
        }
    }
}

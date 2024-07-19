using UnityEngine;

namespace TinySki
{
    public class Player : BaseCharacter
    {
        // Update is called once per frame
        private void Update()
        {
            desiredMovement.x = Input.GetAxis("Horizontal");
            desiredMovement.y = Input.GetAxis("Vertical");
            desiredMovement = Vector2.ClampMagnitude(desiredMovement, 1);
            desiredMovement *= speed;

            desiredMovement += new Vector2(0, -0.1f);
        }
    }

}

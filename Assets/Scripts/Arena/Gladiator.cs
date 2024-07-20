using ArcadeShared;
using UnityEngine;

namespace Arena
{
    public class Gladiator : BaseCharacter
    {
        public CharacterSpeech speech;
        public Transform targetPoint;
        
        public void OnHit()
        {
            animator.SetTrigger(Hit);
            gameManager.OnPlayerHit(this);
            speech.Say("Ouch!", 1f);
        }
    }
}

using ArcadeShared;
using UnityEngine;

namespace Arena
{
    public class Gladiator : BaseCharacter
    {
        public CharacterSpeech speech;
        public Transform targetPoint;
        private CameraShake cameraShake;

        protected override void Awake()
        {
            base.Awake();
            cameraShake = FindAnyObjectByType<CameraShake>();
        }

        public void OnHit()
        {
            cameraShake.Shake();
            animator.SetTrigger(Hit);
            gameManager.OnPlayerHit(this);
            speech.Say("Ouch!", 1f);
        }

        public void OnFallEnd()
        {
            cameraShake.Shake();
        }
    }
}

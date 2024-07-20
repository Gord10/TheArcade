using System;
using System.Collections;
using UnityEngine;

namespace Arena
{
    public class Throwable : MonoBehaviour
    {
        public float disappearSpeed = 0.2f;
        private Rigidbody rigidbody;
        private bool didTouchAnything = false;
        private bool isDisappearing = false;
        
        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        public void Throw(Vector3 targetPos, float baseSpeed)
        {
            float reachTime = CalculateReachTime(transform.position, targetPos, baseSpeed);
            Vector3 velocity = CalculateSpeedForReaching(reachTime, transform.position, targetPos);
            rigidbody.linearVelocity = velocity;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Player") && CanHurtPlayer())
            {
                other.collider.GetComponent<Gladiator>().OnHit();
                print("Throwable item touched player");
            }

            if (!didTouchAnything)
            {
                float fallDownWaitTime = 7;
                StartCoroutine(FallDownAfterWait(fallDownWaitTime));
                didTouchAnything = true;
            }
        }
        
        float CalculateReachTime(Vector3 startPos, Vector3 targetPos, float baseSpeed)
        {
            Vector3 vDiff = targetPos - startPos;
            return vDiff.magnitude / baseSpeed;
        }

        Vector3 CalculateSpeedForReaching(float landTime, Vector3 startPos, Vector3 targetPos)
        {
            Vector3 vDiff = targetPos - startPos;
            return (vDiff / landTime) - ((Physics.gravity * landTime) / 2f); 
        }

        IEnumerator FallDownAfterWait(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            rigidbody.useGravity = false;
            GetComponent<Collider>().enabled = false;
            rigidbody.linearVelocity = Vector3.down * disappearSpeed;
            isDisappearing = true;
        }

        public bool CanHurtPlayer()
        {
            return !didTouchAnything;
        }

        private void FixedUpdate()
        {
            if (isDisappearing)
            {
                //transform.position += Vector3.down * (Time.deltaTime * disappearSpeed);
                if (transform.position.y < -3)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}

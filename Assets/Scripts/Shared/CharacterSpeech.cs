using System;
using System.Collections;
using UnityEngine;
using TMPro;
namespace ArcadeShared
{
    public class CharacterSpeech : MonoBehaviour
    {
        public TextMeshPro text;

        private Transform cam;
        
        private void Awake()
        {
            text.enabled = false;
            cam = Camera.main.transform;
        }

        public void Say(string speech, float showTime)
        {
            text.text = speech;
            text.enabled = true;
            StopAllCoroutines();
            StartCoroutine(HideAfterWait(showTime));
        }

        IEnumerator HideAfterWait(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            text.enabled = false;
        }

        private void LateUpdate()
        {
            transform.rotation = cam.transform.rotation;
        }
    }
}

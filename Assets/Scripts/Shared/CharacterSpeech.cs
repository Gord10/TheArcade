using System;
using System.Collections;
using UnityEngine;
using TMPro;
namespace ArcadeShared
{
    public class CharacterSpeech : MonoBehaviour
    {
        public TextMeshPro text;

        private void Awake()
        {
            text.enabled = false;
        }

        public void Say(string speech, float showTime)
        {
            text.text = speech;
            text.enabled = true;
            StartCoroutine(HideAfterWait(showTime));
        }

        IEnumerator HideAfterWait(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            text.enabled = false;
        }
    }
}

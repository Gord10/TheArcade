using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using DG.Tweening;

namespace ArcadeShared
{
    public class Music : MonoBehaviour
    {
        public static Music Instance
        {
            get
            {
                if (!instance)
                {
                    instance = FindAnyObjectByType<Music>();
                }

                return instance;
            }
        }

        public AudioSource audioSource;
        public AudioClip arcadeHouseClip;
        public AudioClip tinySkiClip;
        public AudioClip beesinessClip;
        [FormerlySerializedAs("volume")] public float baseVolume = 0.5f;
        private static Music instance;

        private void Awake()
        {
            if (instance)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void PlayArcadeHouse()
        {
            PlayClip(arcadeHouseClip);
        }

        public void FadeOut(float fadeTime)
        {
            StartCoroutine(SetVolume(0, 1f / fadeTime));
        }

        public void PlayClip(AudioClip clip)
        {
            if (audioSource.clip != clip)
            {
                audioSource.clip = clip;
                audioSource.volume = baseVolume;
                audioSource.Play();
            }
        }

        public void PlayTinySki()
        {
            PlayClip(tinySkiClip);
        }

        public void PlayBeesiness()
        {
            PlayClip(beesinessClip);
        }
        

        IEnumerator SetVolume(float targetVolume, float changeSpeed)
        {
            WaitForEndOfFrame wait = new WaitForEndOfFrame();
            while (!Mathf.Approximately(audioSource.volume, targetVolume))
            {
                audioSource.volume = Mathf.MoveTowards(audioSource.volume, targetVolume, Time.deltaTime * changeSpeed);
                yield return wait;
            }
        }
    }
}

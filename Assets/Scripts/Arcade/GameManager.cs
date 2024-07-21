using System;
using System.Collections;
using ArcadeShared;
using DG.Tweening;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArcadeHouse
{
    public class GameManager : MonoBehaviour
    {
        public Light directionalLight;
        public float lightFadeOutTime = 3;
        public TextMeshProUGUI titleText;
        public TextMeshProUGUI thanksForPlayingText;
        
        private Door door;
        private void Awake()
        {
            door = FindAnyObjectByType<Door>();
            int completedGameNum = ArcadeShared.ArcadeSceneManager.GetCompletedGameNum();
            print($"Player finished {completedGameNum} games");
            if (completedGameNum == 2)
            {
                door.Open();
            }
            
            //door.Open();
            
            titleText.gameObject.SetActive(false);
            thanksForPlayingText.gameObject.SetActive(false);
            
        }

        private void Start()
        {
            Music.Instance.PlayArcadeHouse();
        }

        public void OnGameComplete()
        {
            StartCoroutine(GameCompleteCutscene());
        }

        public IEnumerator GameCompleteCutscene()
        {
            yield return new WaitForSeconds(8);
            directionalLight.DOIntensity(0, lightFadeOutTime);

            titleText.gameObject.SetActive(true);
            titleText.DOFade(1, lightFadeOutTime);

            yield return new WaitForSeconds(2);
            thanksForPlayingText.gameObject.SetActive(true);
            thanksForPlayingText.DOFade(1, 2);
        }
    }
}

using System;
using ArcadeShared;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArcadeHouse
{
    public class ArcadeMachine : MonoBehaviour
    {
        public CinemachineCamera cinemachineCamera;
        public string sceneName;
        public Transform indicatorCoin;
        public Transform indicatorTrophy;

        private bool isGameSucceeded = false;
        private void Awake()
        {
            if (cinemachineCamera)
            {
                cinemachineCamera.gameObject.SetActive(false);
            }

            isGameSucceeded = ArcadeSceneManager.IsGameSucceeded(sceneName);
            indicatorCoin.gameObject.SetActive(!isGameSucceeded);
            indicatorTrophy.gameObject.SetActive(isGameSucceeded);
        }

        public void Choose()
        {
            if (cinemachineCamera)
            {
                cinemachineCamera.gameObject.SetActive(true);
                Invoke(nameof(StartArcadeGame), 2);
                Music.Instance.FadeOut(2);
            }
        }

        void StartArcadeGame()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}


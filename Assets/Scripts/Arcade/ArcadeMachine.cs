using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArcadeHouse
{
    public class ArcadeMachine : MonoBehaviour
    {
        public CinemachineCamera cinemachineCamera;
        public string sceneName;
        private void Awake()
        {
            if (cinemachineCamera)
            {
                cinemachineCamera.gameObject.SetActive(false);
            }
        }

        public void Choose()
        {
            if (cinemachineCamera)
            {
                cinemachineCamera.gameObject.SetActive(true);
                Invoke("StartArcadeGame", 2);
            }
        }

        void StartArcadeGame()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}


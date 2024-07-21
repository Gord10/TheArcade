using System;
using ArcadeShared;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArcadeHouse
{
    public class ArcadeMachine : MonoBehaviour
    {
        public static int lastChosenArcadeId = -1;
        
        public CinemachineCamera cinemachineCamera;
        public string sceneName;
        public Transform indicatorCoin;
        public Transform indicatorTrophy;
        public Transform player1StandPoint;
        public Transform player2StandPoint;
        
        public int arcadeId = 0;

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
                lastChosenArcadeId = arcadeId;
                cinemachineCamera.gameObject.SetActive(true);
                Invoke(nameof(StartArcadeGame), 2);
                Music.Instance.FadeOut(2);
            }
        }

        void StartArcadeGame()
        {
            SceneManager.LoadScene(sceneName);
        }

        public static ArcadeMachine GetMachineById(int id)
        {
            ArcadeMachine[] machines = FindObjectsByType<ArcadeMachine>(FindObjectsSortMode.None);
            int i;
            for (i = 0; i < machines.Length; i++)
            {
                if (machines[i].arcadeId == id)
                {
                    return machines[i];
                }
            }

            return null;
        }
    }
}


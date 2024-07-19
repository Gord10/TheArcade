using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArcadeHouse
{
    public class GameManager : MonoBehaviour
    {
        private CinemachineBrain cinemachineBrain;

        private void Awake()
        {
            cinemachineBrain = FindAnyObjectByType<CinemachineBrain>();
        }

        public void OnPlayerChooseArcadeMachine(ArcadeMachine arcadeMachine)
        {
            CinemachineCamera arcadeMachineCinemachine = arcadeMachine.GetComponentInChildren<CinemachineCamera>();
            if (arcadeMachine)
            {
                arcadeMachineCinemachine.enabled = true;
            }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }


    }

}

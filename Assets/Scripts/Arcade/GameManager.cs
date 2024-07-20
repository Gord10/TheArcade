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
            print($"Player finished {ArcadeShared.ArcadeSceneManager.completedGameNames.Count} games");
        }
    }
}

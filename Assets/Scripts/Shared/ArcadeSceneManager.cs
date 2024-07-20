using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArcadeShared
{
    public class ArcadeSceneManager : MonoBehaviour
    {
        public static List<string> completedGameNames = new List<string>();
        
        public static void RestartArcadeGame()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }

        public static void ReturnBackToArcadeHouse()
        {
            SceneManager.LoadScene("ArcadeHouse");
        }

        public static void OnGameSuccess()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            if (!completedGameNames.Contains(sceneName))
            {
                completedGameNames.Add(sceneName);
            }
        }

        public static int GetCompletedGameNum()
        {
            return completedGameNames.Count;
        }

        public static bool IsGameSucceeded(string sceneName)
        {
            return completedGameNames.Contains(sceneName);
        }
    }
}


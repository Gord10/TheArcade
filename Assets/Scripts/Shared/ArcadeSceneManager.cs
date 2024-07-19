using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArcadeShared
{
    public class ArcadeSceneManager : MonoBehaviour
    {
        public static void RestartArcadeGame()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }

        public static void ReturnBackToArcadeHouse()
        {
            SceneManager.LoadScene("ArcadeHouse");
        }
    }
}


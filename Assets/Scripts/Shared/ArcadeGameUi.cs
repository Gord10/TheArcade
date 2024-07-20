using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ArcadeShared;
namespace ArcadeShared
{
    public class ArcadeGameUi : MonoBehaviour
    {
        public GameObject inGameScreen;
        public GameObject titleScreen;
        public GameObject resultScreen;

        public TextMeshProUGUI countdownText;
        public GameObject successText;
        public GameObject tryAgainText;

        public Button replayButton;
        
        public void SetScreen(GameState gameState)
        {
            bool isResult = gameState is GameState.Success or GameState.Fail;
            inGameScreen.SetActive(gameState == GameState.InGame);
            titleScreen.SetActive(gameState == GameState.Title);
            resultScreen.SetActive(isResult);
            successText.SetActive(gameState == GameState.Success);
            tryAgainText.SetActive(gameState == GameState.Fail);

            if (isResult)
            {
                EventSystem.current.SetSelectedGameObject(replayButton.gameObject);
            }
        }

        public void ShowCountdown(int seconds)
        {
            countdownText.text = $"0:{seconds}";
        }

        public void RestartGame()
        {
            ArcadeSceneManager.RestartArcadeGame();
        }

        public void ReturnToArcadeHouse()
        {
            ArcadeSceneManager.ReturnBackToArcadeHouse();
        }
    }
}
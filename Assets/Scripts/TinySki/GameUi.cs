using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ArcadeShared;
namespace TinySki
{
    public class GameUi : MonoBehaviour
    {
        public GameObject inGameScreen;
        public GameObject titleScreen;
        public GameObject resultScreen;

        public TextMeshProUGUI countdownText;
        public GameObject successText;
        public GameObject tryAgainText;

        public Button replayButton;
        
        public void SetScreen(GameManager.GameState gameState)
        {
            bool isResult = gameState is GameManager.GameState.Success or GameManager.GameState.Fail;
            inGameScreen.SetActive(gameState == GameManager.GameState.InGame);
            titleScreen.SetActive(gameState == GameManager.GameState.Title);
            resultScreen.SetActive(isResult);
            successText.SetActive(gameState == GameManager.GameState.Success);
            tryAgainText.SetActive(gameState == GameManager.GameState.Fail);

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

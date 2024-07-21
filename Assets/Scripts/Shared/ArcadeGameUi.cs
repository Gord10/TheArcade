using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ArcadeShared;
using DG.Tweening;

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
        public Image[] hearts;
        public Image progressBar;
        
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
            countdownText.text = $"0:{seconds:00}";
        }

        public void ShowLives(int currentLives)
        {
            int i;
            for (i = 0; i < hearts.Length; i++)
            {
                if (i == currentLives)
                {
                    hearts[i].CrossFadeColor(Color.black, 0.25f, false, false);
                }
                else
                {
                    hearts[i].color = (currentLives <= i) ? Color.black : Color.white;
                }
                
            }
        }

        public void RestartGame()
        {
            ArcadeSceneManager.RestartArcadeGame();
        }

        public void ReturnToArcadeHouse()
        {
            ArcadeSceneManager.ReturnBackToArcadeHouse();
        }

        public void UpdateProgressBar(float currentValue, float maxValue)
        {
            float ratio = currentValue / maxValue;
            ratio *= 128f;
            ratio = (int)ratio;
            ratio /= 128f;
            progressBar.fillAmount = ratio;
        }
    }
}
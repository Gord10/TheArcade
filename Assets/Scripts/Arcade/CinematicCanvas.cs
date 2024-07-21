using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CinematicCanvas : MonoBehaviour
{
    public TextMeshProUGUI storyText;
    public Image topLine;
    public Image bottomLine;

    public float timePlayerHasMoved = 0;

    private static bool isStoryTextShown = false;
    
    private void Awake()
    {
        topLine.gameObject.SetActive(false);
        bottomLine.gameObject.SetActive(false);

        if (isStoryTextShown)
        {
            storyText.enabled = false;
        }
    }

    public void StartCinematic(float time = 2)
    {
        topLine.gameObject.SetActive(true);
        topLine.transform.localScale = new Vector3(1, 0, 1);
        topLine.transform.DOScaleY(1, time);
        
        bottomLine.gameObject.SetActive(true);
        bottomLine.transform.localScale = new Vector3(1, 0, 1);
        bottomLine.transform.DOScaleY(1, time);
    }

    private void Update()
    {
        #if CHEAT_ENABLED
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCinematic();
        }
        #endif

        if (!isStoryTextShown &&(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            timePlayerHasMoved += Time.deltaTime;
            if (timePlayerHasMoved >= 2)
            {
                storyText.alpha -= Time.deltaTime;
                if (storyText.alpha <= 0)
                {
                    storyText.enabled = false;
                    isStoryTextShown = true;
                }
            }
        }


    }
}

using TMPro;
using UnityEngine;

namespace ArcadeShared
{
    public class DialogueUi : MonoBehaviour
    {
        public TextMeshProUGUI dialogueLabel;
        public TextMeshProUGUI characterNameLabel;

        public void ShowDialogue(string dialogueText, string characterName)
        {
            dialogueLabel.text = dialogueText;
            characterNameLabel.text = characterName;
            gameObject.SetActive(true);
        }
    }
}

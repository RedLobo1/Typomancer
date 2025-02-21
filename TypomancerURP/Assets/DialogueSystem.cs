using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public TMP_Text dialogueText;
    public float textSpeed = 0.05f;
    public float autoAdvanceTime = 2f;
    public string[] dialogueLines;

    private int currentLineIndex = 0;
    private bool isTyping = false;

    public bool IsEnd;
    public GameObject Tab;
    void Start()
    {
        if (dialogueLines.Length > 0)
        {
            StartCoroutine(TypeText(dialogueLines[currentLineIndex]));
        }
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        isTyping = false;
        yield return new WaitForSeconds(autoAdvanceTime);
        AdvanceDialogue();
    }

    void AdvanceDialogue()
    {
        if (currentLineIndex < dialogueLines.Length - 1)
        {
            currentLineIndex++;
            StartCoroutine(TypeText(dialogueLines[currentLineIndex]));
        }
        else
        {
            dialogueText.text = ""; // End of dialogue
            if(IsEnd)
            {
                Tab.SetActive(true);
            }

        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueHandler : MonoBehaviour
{
    [SerializeField]
    // The text box dialogue will be displayed in
    TMP_Text textBox;

    // This contains all loaded dialogue
    public List<Dialogue> dialogueList = new List<Dialogue>();

    int dialogueIndex = 0;

    Dialogue CurrentDialogue
    {
        get
        {
            return dialogueList[dialogueIndex];
        }
    }

    // This will reset the dialogue increment
    public void Reset()
    {
        dialogueIndex = 0;
        CurrentDialogue.currentIndex = -1;
    }

    // This will move on from one conversation, to the next
    public void LoadNextDialogue()
    {
        if(dialogueIndex + 1 < dialogueList.Count)
        {
            dialogueIndex++;
            CurrentDialogue.currentIndex = -1; // LoadNextMessage increments this to 0 for the first message
            LoadNextMessage();
        }
    }

    // This will display the next message to the text box, and run events where necessary
    public void LoadNextMessage()
    {
        // Check if there is a next message
        if(CurrentDialogue.currentIndex + 1 < CurrentDialogue.conversation.Count)
        {
            CurrentDialogue.currentIndex++;
            StopAllCoroutines();
            StartCoroutine(PushText(CurrentDialogue.conversation[CurrentDialogue.currentIndex]));
        }
        else
        {
            CurrentDialogue.onDialogueEnded.Invoke();
            LoadNextDialogue();
        }
    }

    IEnumerator PushText(string input)
    {
        textBox.text = ""; // Empty the text box
        int currentIndex = 0;
        while(currentIndex < input.Length)
        {
            textBox.text += input[currentIndex];

            currentIndex++;
            yield return new WaitForSeconds(1/30.0f);
        }
    }
}

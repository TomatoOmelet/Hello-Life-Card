using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public Dialogue dialogue;
    
    void Start()
    {
        ShowSentence(new Dialogue("", "adqd"));
    }

    //all showSentence will start the DisplayMultipleSentencesRoutine
    public void ShowSentence(Dialogue dialogue)
    {
        ShowDialogueBox();
        //convert single dialogue to an array contain only one dialogue
        StartCoroutine(DisplayMultipleSentencesRoutine(new Dialogue[]{dialogue}));
    }

    public void ShowSentence(Dialogue[] dialogues)
    {
        ShowDialogueBox();
        StartCoroutine(DisplayMultipleSentencesRoutine(dialogues));
    }

    public void ShowSentence(List<Dialogue> dialogues)
    {
        ShowDialogueBox();
        StartCoroutine(DisplayMultipleSentencesRoutine(dialogues.ToArray()));
    }

    private IEnumerator DisplayMultipleSentencesRoutine(Dialogue[] dialogues)
    {
        //after player press space, show the next sentence
        foreach(Dialogue dialogue in dialogues)
        {
            yield return DisplayOneSentenceRoutine(dialogue);
            yield return new WaitUntil(()=>Input.GetButtonDown("Submit"));
        }
        HideDialogueBox();
    }

    private IEnumerator DisplayOneSentenceRoutine(Dialogue dialogue)
    {
        string head = "";
        //if has speaker, add their name at the start
        if(dialogue.speaker != null && dialogue.speaker.Length > 0)
            head = dialogue.speaker + ":\n";
        for(int x = 1; x<= dialogue.content.Length;++x)
        {
            yield return null;
            dialogueText.text = head + dialogue.content.Substring(0, x)
                                + "<color=#ffffff00>" + dialogue.content.Substring(x) + "</color>";
        }
        dialogueText.text = head + dialogue.content;
    }

    private void ShowDialogueBox()
    {
        dialogueBox.SetActive(true);
    }

    private void HideDialogueBox()
    {
        dialogueBox.SetActive(false);
    }

}

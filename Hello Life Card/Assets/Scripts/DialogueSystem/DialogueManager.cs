using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public Dialogue dialogue;
    

    //all showSentence will start the DisplayMultipleSentencesRoutine
    public IEnumerator DisplaySentence(Dialogue dialogue)
    {
        //convert single dialogue to an array contain only one dialogue
        yield return DisplaySentence(new Dialogue[]{dialogue});
    }


    public IEnumerator DisplaySentence(List<Dialogue> dialogues)
    {
        yield return DisplaySentence(dialogues.ToArray());
    }

    public IEnumerator DisplaySentence(Dialogue[] dialogues)
    {
        dialogueText.text = "";
        ShowDialogueBox();
        //after player press space, show the next sentence
        foreach(Dialogue dialogue in dialogues)
        {
            yield return DisplayOneSentenceRoutine(dialogue);
            yield return new WaitUntil(()=>Input.GetButtonDown("Fire1"));
        }
        HideDialogueBox();
    }

    private IEnumerator DisplayOneSentenceRoutine(Dialogue dialogue)
    {
        string head = "";
        //if has speaker, add their name at the start
        if(dialogue.speaker != null && dialogue.speaker.Length > 0)
            head = dialogue.speaker + ":\n";
        //display sentences one by one
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

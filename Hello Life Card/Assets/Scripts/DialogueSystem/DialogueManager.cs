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
    public TextMeshProUGUI Option1;
    public TextMeshProUGUI Option2;
    public TextMeshProUGUI Option3;
    public int option = -1;
    public GameObject HideButtons;
    

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

    public IEnumerator DisplayQuestion(Dialogue[] dialogues, string[] options, Dialogue[] results)
    {
      
        yield return DisplaySentence(dialogues);

        ShowOptions(options);

        yield return GetOption();

        HideOptions();

        yield return DisplaySentence(results[option]);
        option = -1;
        
        

    }
    
    private IEnumerator GetOption()
    {
        while (option == -1)
        {
            yield return null;
        }
    }

    private void HideOptions()
    {
        HideButtons.SetActive(false);
    }

    private void ShowOptions(string[] options)
    {
        HideButtons.SetActive(true);
        Option1.text = options[0];
        Option2.text = options[1];
        Option3.text = options[2];

    }

    private void ShowDialogueBox()
    {
        dialogueBox.SetActive(true);
    }

    private void HideDialogueBox()
    {
        dialogueBox.SetActive(false);
    }

    public void SetOption(int i)
    {
        option = i;
    }

}

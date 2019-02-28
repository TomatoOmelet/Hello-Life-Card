using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncleDeathHints : MonoBehaviour
{
    [SerializeField] private string[] dialoguetext;
    [SerializeField] private string speaker = "Uncle Death";
    private int startpos=0;
    private int endpos=0;
    
    //Constructs an array of dialogues and plays them
    public IEnumerator PlayLines()
    {
        Dialogue[] dialogues = new Dialogue[endpos-startpos];
        for(int i=startpos;i<endpos;i++)
        {
            dialogues[i-startpos]=new Dialogue(speaker, dialoguetext[i]);
        }
        yield return SystemManager.instance.dialogueManager.DisplaySentence(dialogues);

    }

    //Call this from buttons to play the selection
    public void PlayButton()
    {
        StartCoroutine(PlayLines());
    }

    //Uses setters so it can be called from a button
    public void SetStart(int val)
    {
        startpos = val;
    }

    public void SetEnd(int val)
    {
        endpos = val;
    }

}

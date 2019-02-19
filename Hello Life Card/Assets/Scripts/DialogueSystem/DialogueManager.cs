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
        StartCoroutine(DisplayOneSentence(dialogue));
    }

    public IEnumerator DisplayOneSentence(Dialogue dialogue)
    {

        for(int x = 1; x<= dialogue.content.Length;++x)
        {
            yield return null;
            dialogueText.text = dialogue.name + "\n" + dialogue.content.Substring(0, x)
                                + "<color=#ffffff00>" + dialogue.content.Substring(x) + "</color>";
        }
        dialogueText.text = dialogue.name + "\n" + dialogue.content;
    }

}

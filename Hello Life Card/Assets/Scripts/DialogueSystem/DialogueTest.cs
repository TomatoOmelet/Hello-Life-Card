using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTest : MonoBehaviour
{
    // Start is called before the first frame update
    public Dialogue[] sentences;
    public string[] options;
    public Dialogue[] results;
    public void TestOptions()
    {
        print("here");
        StartCoroutine(SystemManager.instance.dialogueManager.DisplayQuestion(sentences, options, results));
        
    }
}

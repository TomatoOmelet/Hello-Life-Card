using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string speaker;
    public string content;

    public Dialogue(string speaker, string content)
    {
        this.speaker = speaker;
        this.content = content;
    }
}

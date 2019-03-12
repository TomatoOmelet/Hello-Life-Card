using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question
{
    public Dialogue[] sentences;
    public string[] options;
    public Dialogue[] results;

    public Question(Dialogue[] sents, string[] opt, Dialogue[] res)
    {
        sentences = sents;
        options = opt;
        results = res;
    }
}

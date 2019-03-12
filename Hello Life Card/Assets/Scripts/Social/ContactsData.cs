using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Contacts", menuName = "Hello/Contacts")]
public class ContactsData : ScriptableObject
{
    //Some General info about Questions in contacts, hardcoded to always use 3 lines, if there is less, just leave them empty and it will ignore it.
    //options are always in sets of 3, and correspond to results of the same index. resulting trust corresponds to options of the same index and
    //indicate the change in trust that results from an option.
    public string name;
    public Job job;
    public int trustForJob;
    
    [TextArea(2,5)]public string socializeSentence;
    [TextArea(2,5)]public string[] trustIncreaseSentences;
    [TextArea(2,5)]public string referSentence;
    [TextArea(2,5)]public string[] questions;
    [TextArea(2,5)] public string[] options;
    [TextArea(2,5)] public string[] results;
    public int[] resultingtrust;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Contacts", menuName = "Hello/Contacts")]
public class ContactsData : ScriptableObject
{
    public string name;
    public Job job;
    public int trustForJob;
    
    [TextArea(2,5)]public string socializeSentence;
    [TextArea(2,5)]public string trustIncreaseSentence;
    [TextArea(2,5)]public string referSentence;
}

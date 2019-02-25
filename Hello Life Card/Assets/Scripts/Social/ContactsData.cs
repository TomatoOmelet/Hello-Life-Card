using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Contacts", menuName = "Hello/Contacts")]
public class ContactsData : ScriptableObject
{
    public string name;
    public Job job;
}

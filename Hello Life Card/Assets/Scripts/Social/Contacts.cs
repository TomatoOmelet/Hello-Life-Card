using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contacts : MonoBehaviour
{
    public ContactsData data;
    public int trust;
    [System.NonSerialized]public bool hasOfferedJob = false;

    public Contacts(ContactsData data){
        this.data = data;
        this.trust = 10;
    }
}

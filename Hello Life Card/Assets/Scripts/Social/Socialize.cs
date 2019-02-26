using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socialize : MonoBehaviour
{
    public float GetContactsChance = 0.75f; //a number from 0 to 1 

    public void SocializeButton()
    {
        //if already have 5 contacts, fail
        if(SystemManager.instance.contactsManager.contactsList.Count >= 5)
        {
            Dialogue dialogue = new Dialogue("", "You already have too many friends. Your old cell phone cannot store more contacts.");
            StartCoroutine(SystemManager.instance.dialogueManager.DisplaySentence(dialogue));
        }else{
            //do socialize
            if(Random.Range(0,1f) > GetContactsChance || SystemManager.instance.contactsManager.unusedContacts.Count == 0)
            {
                Dialogue dialogue = new Dialogue("", "You didn't meet anyone.");
                StartCoroutine(SocializeEnd(new Dialogue[]{dialogue}));
            }else{
                ContactsData newContact = SystemManager.instance.contactsManager.SocializeNewContacts();
                Dialogue dialogue1= new Dialogue(newContact.name, newContact.socializeSentence);
                Dialogue dialogue2= new Dialogue("", "You got a new contacts. Check your friends in the Contacts Page.");
                StartCoroutine(SocializeEnd(new Dialogue[]{dialogue1, dialogue2}));
            }
        }
    }

    //display socialize result and end day
    public IEnumerator SocializeEnd(Dialogue[] dialogue)
    {
        yield return SystemManager.instance.dialogueManager.DisplaySentence(dialogue);
        SystemManager.instance.DayEnd();
    }

}

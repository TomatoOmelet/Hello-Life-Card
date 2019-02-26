using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactsManager : MonoBehaviour
{
    public List<Contacts> contactsList{get;private set;} = new List<Contacts>();
    public ContactsData[] allContacts;
    public List<ContactsData> unusedContacts;
    public int trustToSacrifice = 100;
    public float contactSucceedRate = 0.75f;
    public int trustIncreasedEachContact = 15;
    private int contactsSacrificed = 0;
    public JobHunt jobHunt;
    // Start is called before the first frame update

    void Awake()
    {
        //initialize 
        unusedContacts = new List<ContactsData>(allContacts);
        contactsList.Clear();
    }
    
    //add new contact to list
    public ContactsData SocializeNewContacts()
    {
        ContactsData data = unusedContacts[Random.Range(0, unusedContacts.Count)];
        contactsList.Add(new Contacts(data));
        unusedContacts.Remove(data);
        return data;
    }

    public void DeleteContacts(int index)
    {
        //delete
        if(contactsList[index].trust < trustToSacrifice)
        {
            unusedContacts.Add(contactsList[index].data);
            contactsList.RemoveAt(index);
        }else{//sacrifice
            contactsList.RemoveAt(index);
            //++contactsSacrificed;
            SystemManager.instance.playerLifeCardFragment++;
        }
    }

    public IEnumerator ContactContacts(int index)
    {
        //success
        if(Random.Range(0, 1f) < contactSucceedRate)
        {
            Dialogue successDialogue = new Dialogue(contactsList[index].data.name, contactsList[index].data.trustIncreaseSentence);
            yield return SystemManager.instance.dialogueManager.DisplaySentence(successDialogue);
            //increase trust
            contactsList[index].trust += trustIncreasedEachContact;
            //if trust is enough, offer job
            if(contactsList[index].trust >= contactsList[index].data.trustForJob && !contactsList[index].hasOfferedJob
                && contactsList[index].data.job != SystemManager.instance.currentJob)
            {
                Dialogue jobDialogue = new Dialogue(contactsList[index].data.name, contactsList[index].data.referSentence);
                yield return SystemManager.instance.dialogueManager.DisplaySentence(jobDialogue);
                jobHunt.ReferJob(contactsList[index].data.job);
                contactsList[index].hasOfferedJob = true;
            }else{
                SystemManager.instance.DayEnd();
            }    
        }else{//fail
            Dialogue failDialogue = new Dialogue("", contactsList[index].data.name + " does not seem to enjoy hanging out with you.");
            yield return SystemManager.instance.dialogueManager.DisplaySentence(failDialogue);
            SystemManager.instance.DayEnd();
        }
    }

}

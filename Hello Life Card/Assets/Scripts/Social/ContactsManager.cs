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
    public Work workManager;
    public Job unemployed;
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
            
            StartCoroutine(Sacrifice(index));
        }
    }

    public IEnumerator Sacrifice(int index)
    {
        ContactsData contactData = contactsList[index].data;
        contactsList.RemoveAt(index);
        ++contactsSacrificed;
        //close panel
        GameObject.FindObjectOfType<ContactsUIManager>().CloseContactPanel();
        //display sentence
        Dialogue dialogue = new Dialogue("", "After Sacrificing " + contactData.name + " to Uncle Dead, you get a life card fragment. You used to be best friends...");
        yield return SystemManager.instance.dialogueManager.DisplaySentence(dialogue);
        //lose job if you get your job from this contacts
        if(SystemManager.instance.currentJob == contactData.job){
            Dialogue loseJobDialogue = new Dialogue("", "You work at the same place as "+ contactData.name +". Your coworkers feel suspicious that " + contactData.name +" disappeared. You have no choice but left the company. You are unemployed now.");
            workManager.ChangeJob(unemployed);
            yield return SystemManager.instance.dialogueManager.DisplaySentence(loseJobDialogue);
        }
        SystemManager.instance.playerLifeCardFragment++;
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

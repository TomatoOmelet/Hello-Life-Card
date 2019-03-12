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
        unusedContacts.Add(contactsList[index].data);
        contactsList.RemoveAt(index);      
    }

    public void SacrificeContacts(int index)
    {
        StartCoroutine(Sacrifice(index));
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
        if(SystemManager.instance.currentJob == contactData.job && SystemManager.instance.jobGotByReference){
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
            if (Random.Range(0, 1f) > .5f)
            {
                string[] successDialogueList = contactsList[index].data.trustIncreaseSentences;
                Dialogue successDialogue = new Dialogue(contactsList[index].data.name, successDialogueList[Random.Range(0, successDialogueList.Length)]);
                yield return SystemManager.instance.dialogueManager.DisplaySentence(successDialogue);
                //increase trust
                contactsList[index].trust += trustIncreasedEachContact;
            }
            else
            {
                //Handles questions
                string[] questions = contactsList[index].data.questions;
                int questionindex = questions.Length % 3;
                questionindex = Random.Range(0, questionindex);
                string[] results = contactsList[index].data.results;
                List<Dialogue> questiondialogue = new List<Dialogue>();
                Dialogue[] resultsdialogue = new Dialogue[3];
                for(int i = 3*questionindex; i < 3*(questionindex+1); i++)
                {

                    resultsdialogue[i] = new Dialogue(contactsList[index].data.name, results[i]);
                    if (questions[i] != "")
                    {
                        questiondialogue.Add(new Dialogue(contactsList[index].data.name, questions[i]));
                    }
                }
                yield return SystemManager.instance.dialogueManager.DisplayQuestion(questiondialogue, contactsList[index].data.options, resultsdialogue);
                contactsList[index].trust += contactsList[index].data.resultingtrust[SystemManager.instance.dialogueManager.option+(questionindex*3)];

            }

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

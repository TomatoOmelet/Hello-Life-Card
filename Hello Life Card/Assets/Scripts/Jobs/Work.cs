using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Work : MonoBehaviour
{
    void Start()
    {
        UpdateJobUI();
    }

    public void ChangeJob(Job newjob)
    {
        SystemManager.instance.currentJob = newjob;
        UpdateJobUI();
    }
    
    public void WorkButton()
    {
        StartCoroutine("GoToWork");
    }

    public IEnumerator GoToWork()
    {
        //Continue as normal if they do not have 0 income
        if (SystemManager.instance.currentJob.jobincome != 0)
        {
            Dialogue d = new Dialogue("", string.Format(SystemManager.instance.currentJob.workmessage[Random.Range(0,SystemManager.instance.currentJob.workmessage.Count)], Income()));
            yield return SystemManager.instance.dialogueManager.DisplaySentence(d);
            int income = Income();
            StartCoroutine(SystemManager.instance.uiManager.AddValueToUI(SystemManager.instance.uiManager.moneyText, income));
            SystemManager.instance.DayEnd();
        }
        //Otherwise dont allow them to work
        else
        {
            List<Dialogue> dialogues = new List<Dialogue>();
            dialogues.Add(new Dialogue("", "You are unemployed, and cannot work having no job."));
            dialogues.Add( new Dialogue("", "Try improving your intelligence and hunting for a job."));
            
            yield return SystemManager.instance.dialogueManager.DisplaySentence(dialogues);
        }
    }

    private int Income()
    {
        return SystemManager.instance.currentJob.jobincome;
    }

    private void UpdateJobUI()
    {
        SystemManager.instance.uiManager.UpdateJobInfoUI(SystemManager.instance.currentJob.jobname, SystemManager.instance.currentJob.jobincome);
    }
}

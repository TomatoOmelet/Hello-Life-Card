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
        
        Dialogue d = new Dialogue("", string.Format(SystemManager.instance.currentJob.workmessage, Income()));
        yield return SystemManager.instance.dialogueManager.DisplaySentence(d);
        SystemManager.instance.playerMoney += Income();
        SystemManager.instance.uiManager.UpdateMoneyUI(SystemManager.instance.playerMoney);
        SystemManager.instance.DayEnd();
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

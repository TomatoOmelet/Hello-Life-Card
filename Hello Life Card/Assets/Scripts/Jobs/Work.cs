using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Work : MonoBehaviour
{
    public Job currentjob;


    void Start()
    {
        UpdateJobUI();
    }

    public void ChangeJob(Job newjob)
    {
        currentjob = newjob;
        UpdateJobUI();
    }
    
    public void WorkButton()
    {
        StartCoroutine("GoToWork");
    }

    public IEnumerator GoToWork()
    {
        
        Dialogue d = new Dialogue("", string.Format(currentjob.workmessage, Income()));
        yield return SystemManager.instance.dialogueManager.DisplaySentence(d);
        SystemManager.instance.playerMoney += Income();
        SystemManager.instance.uiManager.UpdateMoneyUI(SystemManager.instance.playerMoney);
        SystemManager.instance.DayEnd();
    }

    private int Income()
    {
        return currentjob.jobincome;
    }

    private void UpdateJobUI()
    {
        SystemManager.instance.uiManager.UpdateJobInfoUI(currentjob.jobname, currentjob.jobincome);
    }
}

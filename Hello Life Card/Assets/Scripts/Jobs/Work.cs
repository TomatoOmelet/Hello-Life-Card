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

    public void GoToWork()
    {
        SystemManager.instance.playerMoney += currentjob.jobincome;
        SystemManager.instance.uiManager.UpdateMoneyUI(SystemManager.instance.playerMoney);
        SystemManager.instance.DayEnd();
    }

    private void UpdateJobUI()
    {
        SystemManager.instance.uiManager.UpdateJobInfoUI(currentjob.jobname, currentjob.jobincome);
    }
}

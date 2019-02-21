using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Work : MonoBehaviour
{
    public Job currentjob;
    [SerializeField] private TextMeshProUGUI jobinfotext;


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
        jobinfotext.text = string.Format("Current Job: {0}\nCurrent Pay: ${1}", currentjob.jobname, currentjob.jobincome);
    }
}

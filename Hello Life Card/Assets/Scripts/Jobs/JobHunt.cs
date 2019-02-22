using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JobHunt : MonoBehaviour
{
    [SerializeField] private List<Job> jobls;
    [SerializeField] private float intelratio = .5f;
    [SerializeField] private Work w;
    [SerializeField] private TextMeshProUGUI jobinfotext;
    private int newjob=-1;
    [SerializeField] private GameObject jobswapwindow;
   
    public void Hunt()
    {
        int intel = SystemManager.instance.playerIntelligence;
        newjob = -1;
        //Constructs a list of chances for each job
        List<float> chances = new List<float>();
        for(int i=0; i<jobls.Count;i++)
        {
            if (i==0) {
                chances.Add((intel * intelratio) * jobls[i].jobhuntchance);
            }
            else
            {
                chances.Add(((intel * intelratio) * jobls[i].jobhuntchance)+chances[i-1]);
            }
        }

        float randval = Random.value;
        for (int i=0; i<chances.Count;i++)
        {
            if (randval < chances[i] && jobls[i]!=w.currentjob)
            {
                newjob = i;
                SetupWindow();
                break;
            } 
        }

        if (newjob == -1)
        {
            SystemManager.instance.DayEnd();
        }
        
    }

    private void SetupWindow()
    {
        jobswapwindow.SetActive(true);
        jobinfotext.text = string.Format("You got a new job offer to be a {0} that has an income of {1}. Or you can keep your current job, {2} that has an income of {3}.", jobls[newjob].jobname, jobls[newjob].jobincome, w.currentjob.jobname, w.currentjob.jobincome);
    }

    public void ConfirmNewJob()
    {
        jobswapwindow.SetActive(false);
        w.ChangeJob(jobls[newjob]);
        SystemManager.instance.DayEnd();
        
    }

    public void CancelNewJob()
    {
        jobswapwindow.SetActive(false);
        SystemManager.instance.DayEnd();
    }
}

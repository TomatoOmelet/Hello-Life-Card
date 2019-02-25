using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JobHunt : MonoBehaviour
{
    [SerializeField] private List<Job> jobls;
    [SerializeField] private float intelratio = .5f;
    [SerializeField] private Work w;
    private int newjob=-1;

   
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
                SetupJobOffer(jobls[i],i);
                break;
            } 
        }

        if (newjob == -1)
        {
            StartCoroutine("Rejection");
        }
        
    }

    private IEnumerator Rejection()
    {
        yield return SystemManager.instance.dialogueManager.DisplaySentence(new Dialogue("", "Sorry, Nobody wanted you..."));
        SystemManager.instance.DayEnd();
    }

    public void SetupJobOffer(Job j, int? index =null)
    {
        newjob = index ?? GetJobIndex(j);
        SystemManager.instance.uiManager.SetupJobOfferWindow(j.jobname, j.jobincome, w.currentjob.jobname, w.currentjob.jobincome);
    }


    public void ConfirmNewJob()
    {
        SystemManager.instance.uiManager.CloseJobOfferWindow();
        w.ChangeJob(jobls[newjob]);
        SystemManager.instance.DayEnd();
        
    }

    public void CancelNewJob()
    {
        SystemManager.instance.uiManager.CloseJobOfferWindow();
        SystemManager.instance.DayEnd();
    }

    //be refered a job instead of hunting it, that's cheating
    public void ReferJob(int newJob)
    {
        SetupJobOffer(jobls[newJob]);
    }

    public int GetJobIndex(Job job)
    {
        return jobls.IndexOf(job);
    }
}

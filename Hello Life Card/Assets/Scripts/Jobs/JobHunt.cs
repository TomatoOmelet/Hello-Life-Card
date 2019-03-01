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

    [Header("UI")]
    public GameObject jobofferwind;
    public TextMeshProUGUI joboffertext;
    public TextMeshProUGUI jobWindTitle;
    public GameObject confirmButton;
    public GameObject refuseButton;
    public GameObject okButton;
   
    public Job Hunt()
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
            if (randval < chances[i] && jobls[i]!=SystemManager.instance.currentJob)
            {
                //SetupJobOffer(jobls[i],i);
                //break;
                return jobls[i];
            } 
        }

        /* 
        if (newjob == -1)
        {
            //StartCoroutine("Rejection");
        }
        */
        return null;
    }

    //=======================================================================================
    //UI
    //=======================================================================================

    private IEnumerator Rejection()
    {
        yield return SystemManager.instance.dialogueManager.DisplaySentence(new Dialogue("", "Sorry, Nobody wanted you..."));
        SystemManager.instance.DayEnd();
    }

    public void SetupJobOffer(Job j, int? index =null)
    {
        newjob = index ?? GetJobIndex(j);
        SetupJobOfferWindow(j.jobname, j.jobincome, SystemManager.instance.currentJob.jobname, SystemManager.instance.currentJob.jobincome);
    }

    public void SetupJobOfferWindow(string newjobname, int newjobincome, string oldjobname, int oldjobincome)
    {
        jobofferwind.SetActive(true);
        //set buttons
        confirmButton.SetActive(true);
        refuseButton.SetActive(true);
        okButton.SetActive(false);
        jobWindTitle.text = "New Job";
        joboffertext.text = string.Format("You got a new job offer to be a {0} that has an income of {1}$. Or you can keep your current job, {2} that has an income of {3}$.", newjobname, newjobincome, oldjobname, oldjobincome);
    }

    public void SetupNoResponse()
    {
        jobofferwind.SetActive(true);
        //set buttons
        confirmButton.SetActive(false);
        refuseButton.SetActive(false);
        okButton.SetActive(true);
        
        jobWindTitle.text = "Hmmmmm....";
        joboffertext.text = string.Format("You never hear from the company you applied to.");
    }

    public void SetupRejection()
    {
        jobofferwind.SetActive(true);
        //set buttons
        confirmButton.SetActive(false);
        refuseButton.SetActive(false);
        okButton.SetActive(true);
        
        jobWindTitle.text = "Important information about your application";
        joboffertext.text = string.Format(@"Thank you for applying to our company. Unfortunately, we won't move forward with your application this time.
                                            We had many talented candidates this year. However, we will keep your information in our system. 
                                            In the future if we had positions fit your background open, we will keep you in mind.");
    }

    public void ConfirmNewJob()
    {
        jobofferwind.SetActive(false);
        w.ChangeJob(jobls[newjob]);
        SystemManager.instance.DayEnd();
        
    }

    public void CancelNewJob()
    {
        jobofferwind.SetActive(false);
        SystemManager.instance.DayEnd();
    }

    //be refered a job instead of hunting it, that's cheating
    public void ReferJob(Job newJob)
    {
        SetupJobOffer(newJob);
    }

    public int GetJobIndex(Job job)
    {
        return jobls.IndexOf(job);
    }
}

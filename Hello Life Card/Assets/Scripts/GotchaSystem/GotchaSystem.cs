using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GotchaSystem : MonoBehaviour
{
    public JobHunt jobHunt;
    public GameObject resume;
    public GameObject result;
    public Transform resumeLocation;
    public Transform recruiterLocation;

    private Job job;

    void Start()
    {
        Reset();
    }

    //====================================================================================
    //System
    //====================================================================================
    public void Reset()
    {
        job = null;
        //reset resume
        resume.transform.position = resumeLocation.position;
        resume.GetComponent<Button>().interactable = true;
        //reset location of result    
        result.transform.position = recruiterLocation.position; 
        result.GetComponent<Button>().interactable = false;
    }

    public void Gotcha()
    {
        //set button to not interactable
        resume.GetComponent<Button>().interactable = false;
        job = jobHunt.Hunt();

        StartCoroutine(GotchaAnimation(job));
        //if get no job
        if(job == null)
        {
            SystemManager.instance.DayEnd();
        }else{
            jobHunt.SetupJobOffer(job);
        }
    }

    public IEnumerator GotchaAnimation(Job job)
    {
        float speed = 1300f;
        speed *= Time.deltaTime;
        //move resume up
        for(float y = resumeLocation.position.y; y < recruiterLocation.position.y; y += speed)
        {
            Vector3 newPosition = resumeLocation.position;
            newPosition.y = y;
            resume.transform.position = newPosition;
            yield return new WaitForSeconds(0.02f);
        }
        resume.transform.position = recruiterLocation.position;
        //wait for a while 
        //yield return new WaitForSeconds(2f);
        //move result down
        for(float y = recruiterLocation.position.y; y > resumeLocation.position.y ; y -= speed)
        {
            Vector3 newPosition = recruiterLocation.position;
            newPosition.y = y;
            result.transform.position = newPosition;
            yield return new WaitForSeconds(0.02f);
        }
        result.transform.position = resumeLocation.position;
        result.GetComponent<Button>().interactable = true;
        
    }

    //====================================================================================
    //UI 
    //====================================================================================

    public void OpenPage()
    {
        gameObject.SetActive(true);
        Reset();
    }

    public void ClosePage()
    {
        gameObject.SetActive(false);
    }

    public void OpenResult()
    {
        if(job != null)
        {
            jobHunt.SetupJobOffer(job);
        }
        Reset();
    }

}

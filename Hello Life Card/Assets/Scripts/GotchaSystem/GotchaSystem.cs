using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GotchaSystem : MonoBehaviour
{
    public JobHunt jobHunt;
    public GameObject resume;

    //====================================================================================
    //System
    //====================================================================================
    public void Reset()
    {
        //reset resume
        resume.transform.position = new Vector3(0, 0, resume.transform.position.z);
        resume.GetComponent<Button>().interactable = true;
        //reset location of result    
    }

    public void Gotcha()
    {
        //set button to not interactable
        resume.GetComponent<Button>().interactable = false;
        Job job = jobHunt.Hunt();

        //StartCoroutine(GotchaAnimation(job));
        //if get no job
        if(job == null)
        {
            SystemManager.instance.DayEnd();
        }else{
            jobHunt.SetupJobOffer(job);
        }
    }

    //public IEnumerator GotchaAnimation(Job job)
    //{
        //move resume up
        
        //move things down

        
    //}

    //====================================================================================
    //UI 
    //====================================================================================

    public void OpenPage()
    {
        gameObject.SetActive(true);
    }

    public void ClosePage()
    {
        gameObject.SetActive(false);
    }

}

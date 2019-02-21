using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobHunt : MonoBehaviour
{
    [SerializeField] private List<Job> jobls;
    [SerializeField] private float intelratio = .5f;
    [SerializeField] private Work w;
   
    public void Hunt()
    {
        int intel = SystemManager.instance.playerIntelligence;

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
            if (randval < chances[i])
            {
                w.ChangeJob(jobls[i]);
                    break;
            } 
        }

        SystemManager.instance.DayEnd();
    }
}

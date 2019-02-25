using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Job.asset", menuName = "Hello/Job")]
public class Job : ScriptableObject
{
    public string jobname;
    public int jobincome;
    public float jobhuntchance;
    public string workmessage = "You worked hard at your job and were paid {0}$";

}

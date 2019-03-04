using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Job.asset", menuName = "Hello/Job")]
public class Job : ScriptableObject
{
    public string jobname;
    public int jobincome;
    public float jobhuntchance;
    [Range(3,5)]public int star;
    public List<string> workmessage;

    void Start()
    {
        workmessage= new List<string>();
    }

}

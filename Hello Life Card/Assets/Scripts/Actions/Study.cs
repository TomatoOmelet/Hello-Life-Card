using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Study : MonoBehaviour
{
    public void StudyButton()
    {
        SystemManager.instance.playerIntelligence += IntelligenceIncrease();
        SystemManager.instance.DayEnd();
    }

    /*this function returns how much intelligence will increase by studying,
    considering the current situation (seanson, etc.)*/
    private int IntelligenceIncrease()
    {
        return 1;
    }
}

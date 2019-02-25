using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Study : MonoBehaviour
{
    private int uiIntelligence; //the current intelligence displayed on UI, instead of real intelligence

    public void StudyButton()
    {
        StartCoroutine(StudyAction()); 
    }

    public IEnumerator StudyAction()
    {
        int intelligence = IntelligenceIncrease();
        //construc the sentence displayed before 
        Dialogue dialogue = new Dialogue("", "After studying hard, your intelligence increases by " + intelligence + " .");
        yield return SystemManager.instance.dialogueManager.DisplaySentence(dialogue);
        StartCoroutine(AddScore(intelligence));
        SystemManager.instance.DayEnd();
    }

    public IEnumerator AddScore(int value)
    {
        SystemManager.instance.playerIntelligence += value;
        //SystemManager.instance.uiManager.PromptIntelligenceChange(value);
        //add numbers one by one
        for(int x = 0; x< value; ++x)
        {
            SystemManager.instance.uiManager.UpdateIntelligenceUI(++uiIntelligence);
            yield return null;
        }
    }

    /*this function returns how much intelligence will increase by studying,
    considering the current situation (seanson, etc.)*/
    private int IntelligenceIncrease()
    {
        return 10;
    }
}

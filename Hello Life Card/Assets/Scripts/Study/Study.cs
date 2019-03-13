using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Study : MonoBehaviour
{
    [SerializeField]private List<string> msgs;

    public void StudyButton()
    {
        StartCoroutine(StudyAction()); 
    }

    public IEnumerator StudyAction()
    {
        int intelligence = SystemManager.instance.playerStudyRate;
        //construc the sentence displayed before 
        Dialogue dialogue = new Dialogue("", string.Format(msgs[Random.Range(0,msgs.Count)],intelligence));
        yield return SystemManager.instance.dialogueManager.DisplaySentence(dialogue);
        StartCoroutine(SystemManager.instance.uiManager.AddValueToUI(SystemManager.instance.uiManager.intelligenceText, intelligence));
        SystemManager.instance.DayEnd();
    }

    /*this function returns how much intelligence will increase by studying,
    considering the current situation (seanson, etc.)*/
    private int IntelligenceIncrease()
    {
        return 10;
    }
}

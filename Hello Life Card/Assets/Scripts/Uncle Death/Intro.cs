using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    // Start is called before the first frame update
    public Dialogue[] introspeech;
    public GameObject buttons;
    public int tutorial = 0;
    public UncleDeathHints uc;

    void Start()
    {
        StartCoroutine(IntroScene());
    }

    IEnumerator IntroScene()
    {
        yield return SystemManager.instance.dialogueManager.DisplaySentence(introspeech);
        buttons.SetActive(true);
        while (tutorial == 0)
        {
            yield return null;
        }
        buttons.SetActive(false);
        if (tutorial == 1)
        {
            uc.SetStart(0);
            uc.SetEnd(uc.dialoguetext.Length);
            yield return uc.PlayLines();
        }

        yield return SystemManager.instance.dialogueManager.DisplaySentence(new Dialogue("Uncle Death", "Alright, if you need anything I'll be around."));
        this.gameObject.SetActive(false);
    }

    public void SetTutorial(int i)
    {
        tutorial = i;
    }
   
}

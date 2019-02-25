using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class SystemUIManager : MonoBehaviour
{
    public TextMeshProUGUI intelligenceText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI weekText;
    public TextMeshProUGUI seasonText;
    public TextMeshProUGUI intelligenceChangePromptText;
    public TextMeshProUGUI moneyChangePromptText;

    //update UI elements
    public void UpdateIntelligenceUI(int intelligence)
    {
        intelligenceText.text =  intelligence + " IQ";
    }

    public void UpdateMoneyUI(int money)
    {
        moneyText.text =  money + " $";
    }

    public void UpdateWeekUI(int week)
    {
        weekText.text =  "Week: " + week + "/9";
    }

    public void UpdateSeasonUI(Season season)
    {
        seasonText.text = "Season: " + season.ToString();
    }

    private Coroutine intelligencePromptRoutine;

    public void PromptInfoChange(TextMeshProUGUI infoText, int value, Color posColor)
    {
        //make sure only one routine can touch this text
        if(intelligencePromptRoutine != null)
            StopCoroutine(intelligencePromptRoutine);
        intelligencePromptRoutine = StartCoroutine(PromptInfoChangeRoutine(infoText, value, Color.green));
    }
    public IEnumerator PromptInfoChangeRoutine(TextMeshProUGUI infoText, int value, Color posColor)
    {
        string info;
        //set the text and color
        if(value >= 0)
        {
            info = "+" + value;
            infoText.color = posColor;
        }else{
            info = value.ToString();
            infoText.color = Color.red;
        }
        //update the UI
        infoText.text = info;
        infoText.gameObject.SetActive(true);
        //stay opaque for a while 
        yield return new WaitForSeconds(0.5f);
        Color currentColor = infoText.color;
        //fade 
        for(float x = 1; x >= 0; x -= 0.05f)
        {
            Color temp = currentColor;
            temp.a = x;
            infoText.color = temp;
            yield return new WaitForSeconds(0.02f);
        } 
        infoText.gameObject.SetActive(false);
    }

}

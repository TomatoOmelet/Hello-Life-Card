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

    public void PromptIntelligenceChange(int value)
    {
        //make sure only one routine can touch this text
        if(intelligencePromptRoutine != null)
            StopCoroutine(intelligencePromptRoutine);
        intelligencePromptRoutine = StartCoroutine(PromptIntelligenceChangeRoutine(value));
    }
    public IEnumerator PromptIntelligenceChangeRoutine(int value)
    {
        string info;
        //set the text and color
        if(value >= 0)
        {
            info = "+" + value;
            intelligenceChangePromptText.color = Color.green;
        }else{
            info = value.ToString();
            intelligenceChangePromptText.color = Color.red;
        }
        //update the UI
        intelligenceChangePromptText.text = info;
        intelligenceChangePromptText.gameObject.SetActive(true);
        //stay opaque for a while 
        yield return new WaitForSeconds(0.5f);
        Color currentColor = intelligenceChangePromptText.color;
        //fade 
        for(float x = 1; x >= 0; x -= 0.05f)
        {
            Color temp = currentColor;
            temp.a = x;
            intelligenceChangePromptText.color = temp;
            yield return new WaitForSeconds(0.02f);
        } 
        intelligenceChangePromptText.gameObject.SetActive(false);
    }

}

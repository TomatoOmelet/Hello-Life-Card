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
    public TextMeshProUGUI lifeCardFragmentText;
    public TextMeshProUGUI intelligenceChangePromptText;
    public TextMeshProUGUI moneyChangePromptText;
    public TextMeshProUGUI jobInfoText;
    public TextMeshProUGUI joboffertext;
    public GameObject jobofferwind;

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
    public void UpdateJobInfoUI(string jobname, int jobincome)
    {
        jobInfoText.text= string.Format("Current Job: {0}\nCurrent Pay: ${1}", jobname, jobincome);
    }

    public void UpdateLifeCardFragmentUI(int value)
    {
        lifeCardFragmentText.text = "Life Card Fragments: " + value;
    }

    //==============================================================================
    //Prompt the change of intelligence/money
    //==============================================================================
    private Coroutine intelligencePromptRoutine;
    private Coroutine moneyPromptRoutine;
    public void PromptIntelligenceChange(int value)
    {
        //make sure only one routine can touch this text
        if(intelligencePromptRoutine != null)
            StopCoroutine(intelligencePromptRoutine);
        intelligencePromptRoutine = StartCoroutine(PromptInfoChangeRoutine(intelligenceChangePromptText, value, Color.green));
    }
    public void PromptMoneyChange(int value)
    {
        //make sure only one routine can touch this text
        if(moneyPromptRoutine != null)
            StopCoroutine(moneyPromptRoutine);
        moneyPromptRoutine = StartCoroutine(PromptInfoChangeRoutine(moneyChangePromptText, value, Color.yellow));
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

    //==============================================================================
    //Increasing of Money Intelligence
    //==============================================================================
    private int uiIntelligence; //the current intelligence displayed on UI, instead of real intelligence
    private int uiMoney; 
    public IEnumerator AddValueToUI(TextMeshProUGUI text, int value)
    {
        if(text == moneyText)
            SystemManager.instance.playerMoney += value;
        else
            SystemManager.instance.playerIntelligence += value;
        //SystemManager.instance.uiManager.PromptIntelligenceChange(value);
        //add numbers one by one
        for(int x = 0; x< value; ++x)
        {
            if(text == moneyText)
                UpdateMoneyUI(++uiMoney);
            else
                UpdateIntelligenceUI(++uiIntelligence);
            yield return null;
        }
    }


}

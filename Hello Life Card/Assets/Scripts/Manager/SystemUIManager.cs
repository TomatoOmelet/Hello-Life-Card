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
}

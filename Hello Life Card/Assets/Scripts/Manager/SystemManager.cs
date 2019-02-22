using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Season
{
    Spring,
    Summer,
    Fall,
    Winter
}

public class SystemManager : MonoBehaviour
{
    public static SystemManager instance;
    //states
    private int intelligence = 0;
    private int money = 0;
    private Season season = Season.Spring;
    private int week = 1;

    //object needed
    public SystemUIManager uiManager;
    public DialogueManager dialogueManager;

    public int playerIntelligence{
        get{return intelligence;}
        set{
            intelligence = value;
            uiManager.UpdateIntelligenceUI(value);
            }
    }
    public int playerMoney{
        get{return money;}
        set{money = value;}
    }

    void Awake()
    {
        //singleton
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeUI();
    }

    //update all UIelements using current value
    private void InitializeUI()
    {
        uiManager.UpdateIntelligenceUI(intelligence);
        uiManager.UpdateMoneyUI(money);
        uiManager.UpdateWeekUI(week);
        uiManager.UpdateSeasonUI(season);
    }

    //===============================================================
    //Game Progress
    //===============================================================
    public void DayEnd()
    {
        //increase week and check if need to go to next season
        if(++week > 9)
        {
            week = 1;
            season = (Season)((int)season + 1);
        }
        //update ui
        uiManager.UpdateWeekUI(week);
        uiManager.UpdateSeasonUI(season);
    }
}

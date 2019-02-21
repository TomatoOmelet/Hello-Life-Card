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
    private int week = 0;

    //object needed
    public SystemUIManager uiManager;

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

}

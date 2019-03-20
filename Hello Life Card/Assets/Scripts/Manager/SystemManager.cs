using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Season
{
    Spring,
    Summer,
    Fall,
    Winter
}

public class SystemManager : MonoBehaviour
{
    public static SystemManager instance{get; private set;}
    //states
    private int intelligence = 0;
    private int studyRate = 10;
    private int money = 0;
    private int lifeCardFragment = 0;
    private Season season = Season.Spring;
    private int week = 1;
    public Job currentJob;
    public bool jobGotByReference = false;
    public int weeksremaining = 52;
    

    //object needed
    public SystemUIManager uiManager;
    public DialogueManager dialogueManager;
    public ContactsManager contactsManager;
    public AudioManager audiomanager;
    public int playerStudyRate
    {
        get { return studyRate; }
        set
        {
            studyRate = value;
        }
    }
    public int playerLifeCardFragment {
        get{return lifeCardFragment;} 
        set{
            lifeCardFragment = value;
            uiManager.UpdateLifeCardFragmentUI(value);
        }
    }
    public int playerIntelligence{
        get{return intelligence;}
        set{
            uiManager.PromptIntelligenceChange(value - intelligence);
            intelligence = value;
            }
    }
    public int playerMoney{
        get{return money;}
        set{
            uiManager.PromptMoneyChange(value - money);
            money = value;
            }
    }

    //seanson UI
    public Image windowImage;
    public Sprite[] seasonWindowSprites;
    public Image treeImage;
    public Sprite[] seasonTreeSprites;

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
        windowImage.sprite = seasonWindowSprites[0];
        treeImage.sprite = seasonTreeSprites[0];
    }

    //update all UIelements using current value
    private void InitializeUI()
    {
        uiManager.UpdateIntelligenceUI(intelligence);
        uiManager.UpdateMoneyUI(money);
        //uiManager.UpdateWeekUI(week);
        //uiManager.UpdateSeasonUI(season);
        uiManager.UpdateWeeksRemaining(weeksremaining);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Destroy(gameObject);
    }

    //===============================================================
    //Game Progress
    //===============================================================
    public void DayEnd()
    {
        //check if game end
        if(week == 13 && season == Season.Winter)
        {
            uiManager.UpdateWeeksRemaining(0);
            GameEnd();
            return;
        }
        weeksremaining--;
        //increase week and check if need to go to next season
        if(++week > 13)
        {
            week = 1;
            season = (Season)((int)season + 1);
            //change season sprite
            switch(season)
            {
                case Season.Summer:{
                    windowImage.sprite = seasonWindowSprites[1];
                    treeImage.sprite = seasonTreeSprites[1];
                    break;}
                case Season.Fall:{
                    windowImage.sprite = seasonWindowSprites[2];
                    treeImage.sprite = seasonTreeSprites[2];
                    break;}
                case Season.Winter:{
                    windowImage.sprite = seasonWindowSprites[3];
                    treeImage.sprite = seasonTreeSprites[3];
                    break;}
                default:
                    break;
            }
        }
        //update ui
        uiManager.UpdateWeeksRemaining(weeksremaining);
        //uiManager.UpdateWeekUI(week);
        //uiManager.UpdateSeasonUI(season);
    }

    //check the end of the game
    public void GameEnd()
    {
        //end1: death
        if(lifeCardFragment < 3)
        {
            StartCoroutine(EndDeath());
        }else{//end2: life
            if(contactsManager.GetContactSacrificedNum() > 0)
            {
                StartCoroutine(EndEvil());
            }else{
                StartCoroutine(EndLife());
            }
        }
    }

    public IEnumerator EndDeath()
    {
        Dialogue dialogue1 = new Dialogue("", "After four seasons, you still didn't get a life card. Uncle Dead took your life.");
        Dialogue dialogue2 = new Dialogue("", "Bad End.");
        yield return dialogueManager.DisplaySentence(new Dialogue[]{dialogue1, dialogue2});
        BackToMenu();
    }

    public IEnumerator EndEvil()
    {
        Dialogue dialogue1 = new Dialogue("", "To get a life card, you even sacrificed your best friend.");
        Dialogue dialogue2 = new Dialogue("", "In the end, you get your life card, and give it to Uncle Death to exchange for your life.");
        Dialogue dialogue3 = new Dialogue("", "However, weird things start to happen around you. You always hear your died friends crying in the night.");
        Dialogue dialogue4 = new Dialogue("", "Haunted End.");
        yield return dialogueManager.DisplaySentence(new Dialogue[]{dialogue1, dialogue2, dialogue3, dialogue4});
        BackToMenu();
    }

    public IEnumerator EndLife()
    {
        Dialogue dialogue1 = new Dialogue("", "After your hard work, you get your life card. You give it to Uncle Death to exchange for your life, and live happily ever since.");
        Dialogue dialogue2 = new Dialogue("", "Good End.");
        yield return dialogueManager.DisplaySentence(new Dialogue[]{dialogue1, dialogue2});
        BackToMenu();
    }

}

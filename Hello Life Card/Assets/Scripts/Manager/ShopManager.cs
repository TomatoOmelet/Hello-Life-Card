using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int uiIntelligence;//just used for testing purposes
    private int uiMoney = 0;
    public RNGGenerator lottery;

    public Dictionary<string,System.Action> items = new Dictionary<string,System.Action>();
    //RNGGenerator rng = new RNGGenerator();
   
    private void Start()
    {

        Debug.Log(SystemManager.instance.playerMoney);
        items.Add("Textbook", getTextbook);
        items.Add("Fancy Desk", studyRate);
        //items.Add("Fancy Cell Phone", addContacts);
        items.Add("Lottery Ticket", rollLotterySmall);
        items.Add("Fancy Lottery Ticket", rollLotteryMedium);
        items.Add("Nothing", doNothing);
        items.Add("Extreme Lottery Ticket", rollLotteryLarge);
        items.Add("Life Card Fragment", addLifeCardFragment);
        items.Add("10", gainCMoney);
        items.Add("100", gainBMoney);
        items.Add("500", gainAMoney);
        items.Add("1000", gainSMoney);
    }

    private void gainSMoney()
    {
        Debug.Log("got 1000");
        SystemManager.instance.playerMoney += 1000;
    }

    private void gainAMoney()
    {
        Debug.Log("got 500");
        SystemManager.instance.playerMoney += 500;
    }

    private void gainBMoney()
    {
        Debug.Log("got 100");
        SystemManager.instance.playerMoney += 100;
    }

    private void gainCMoney()
    {
        Debug.Log("got 10");
        SystemManager.instance.playerMoney += 10;
    }

    public void makePurchase(string itemNC)
    {//takes a string formatted in the form cost/itemname ex: 70/textbook/int/total
        string strCost = "";
        int i = 0; while(i<itemNC.Length && itemNC[i]!= '/')
        {
            strCost += itemNC[i];
            ++i;
            
        }
        int cost = int.Parse(strCost);
        if (cost <= SystemManager.instance.playerMoney)
        {

            Debug.Log("this is the cost: "+cost);
            //Debug.Log(itemNC.Substring(i + 1));
            //StartCoroutine(SubtractCash(cost));
            items[itemNC.Substring(i + 1)]();
            StartCoroutine(SubtractCash(cost));
            //Debug.Log(SystemManager.instance.playerMoney + " total money");
            
            //do the item action
        }
        else
        {
            //have a popup that says something to the effect of not enough money
        }
        //buys the item associated with the button

    }

    private void getTextbook()
    {
        Debug.Log("After receiving a textbook, you feel like you've gotten smarter");
        StartCoroutine(IntelIncrease(30, (new Dialogue("", "With a textbook, you feel smarter. Your intelligence increases by " + 30 + " ."))));
        
        
    }
   
    

    private void studyRate()
    {
        Debug.Log("With a new desk, you will be better at studying");
    }
   // private void addContacts()
    //{

    //}

    private void rollLotterySmall()
    {
        StartCoroutine(display(new Dialogue("", "You have received a Normal Lottery Ticket, best of luck!")));
        string str = lottery.GenerateCPrize();
        items[str]();
    }
    private void rollLotteryMedium()
    {
        StartCoroutine(display(new Dialogue("", "You have received a Fancy Lottery Ticket, best of luck!")));
        string str = lottery.GenerateBPrize();
        items[str]();

    }
    private void doNothing()
    {
        StartCoroutine(display (new Dialogue("", "You have gained nothin from this exchange")));
        Debug.Log("You get nothing ");
    }
    private void rollLotteryLarge()
    {
        StartCoroutine(display(new Dialogue("", "You have received an Extreme Lottery Ticket, best of luck!")));
        string str = lottery.GenerateAPrize();
        items[str]();
    }
    private void addLifeCardFragment()
    {
        Debug.Log("Life is in your grasp");
    }

public IEnumerator display(Dialogue dialogue)
    {
        yield return SystemManager.instance.dialogueManager.DisplaySentence(dialogue);
    }

public IEnumerator IntelIncrease(int amount,Dialogue dialogue)
{
    
    //construc the sentence displayed before 
    yield return SystemManager.instance.dialogueManager.DisplaySentence(dialogue);
    StartCoroutine(AddScore(amount));
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
    public IEnumerator SubtractCash(int value)
    {
        Debug.Log("TOTAL AMOUNT OF MONEY" + SystemManager.instance.playerMoney);
        
        SystemManager.instance.playerMoney -= value;
        //SystemManager.instance.uiManager.PromptIntelligenceChange(value);
        //add numbers one by one
        Debug.Log(SystemManager.instance.playerMoney);
        for (int x = 0; x < value; ++x)
        {
            //UPDATE UI
            SystemManager.instance.uiManager.UpdateMoneyUI(--uiMoney);
            yield return null;
        }
    }

    
};

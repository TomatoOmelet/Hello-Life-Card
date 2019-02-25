using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int money;//just used for testing purposes
    public RNGGenerator lottery;
    public Dictionary<string,System.Action> items = new Dictionary<string,System.Action>();
    //RNGGenerator rng = new RNGGenerator();

    private void Start()
    {
        
        items.Add("Textbook", addIntelligence);
        items.Add("Fancy Desk", studyRate);
        items.Add("Fancy Cell Phone", addContacts);
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
        SystemManager.instance.playerMoney += 1000;
    }

    private void gainAMoney()
    {
        SystemManager.instance.playerMoney += 500;
    }

    private void gainBMoney()
    {
        SystemManager.instance.playerMoney += 100;
    }

    private void gainCMoney()
    {
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
            SystemManager.instance.playerMoney -= cost;
            //Debug.Log(itemNC.Substring(i + 1));
            items[itemNC.Substring(i + 1)]();
            Debug.Log(SystemManager.instance.playerMoney + " total money");
            
            //do the item action
        }
        else
        {
            //have a popup that says something to the effect of not enough money
        }
        //buys the item associated with the button

    }

    private void addIntelligence()
    {
        Debug.Log("After receiving a textbook, you feel like you've gotten smarter");
        SystemManager.instance.playerIntelligence += 3;
        
    }
    private void studyRate()
    {
        Debug.Log("With a new desk, you will be better at studying");
    }
    private void addContacts()
    {

    }

    private void rollLotterySmall()
    {
        string str = lottery.GenerateCPrize();
        Debug.Log(str+ " In rollLotterySmall");
        items[str]();
    }
    private void rollLotteryMedium()
    {
        string str = lottery.GenerateBPrize();
        Debug.Log(str + "In rollLotteryMedium");
        items[str]();
        
    }
    private void doNothing()
    {
        Debug.Log("You get nothing ");
    }
    private void rollLotteryLarge()
    {
        string str = lottery.GenerateAPrize();
        Debug.Log(str + "In rollLotteryLarge");
        items[str]();
    }
    private void addLifeCardFragment()
    {
        Debug.Log("Life is in your grasp");
    }
}

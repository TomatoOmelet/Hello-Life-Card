using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    // Start is called before the first frame update
   // private int uiIntelligence;//just used for testing purposes
    //private int uiMoney = 0;
    public RNGGenerator lottery;
    
    public Dictionary<string,System.Action> items = new Dictionary<string,System.Action>();
    //RNGGenerator rng = new RNGGenerator();
   
    private void Start()
    {
        //SystemManager.instance.playerMoney = 2000;
       // SystemManager.instance.playerMoney = 2500;
      //Debug.Log(SystemManager.instance.playerMoney);
        items.Add("Textbook", getTextbook);
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
//      Debug.Log(itemNC);
 //     Debug.Log(SystemManager.instance.playerMoney);
        int i = 0; while(i<itemNC.Length && itemNC[i]!= '/')
        {
            strCost += itemNC[i];
            ++i;
            
        }
        int cost = int.Parse(strCost);

        if (cost <= SystemManager.instance.playerMoney)
        {
            string itemname = itemNC.Substring(i + 1);

            
            StartCoroutine(handleDialog(itemname));
            

            //items[itemNC.Substring(i + 1)]();
            AdjustCash(-1*cost);

        }
        else
        {
            
            StartCoroutine(display(new Dialogue("", "Unfortunately, you don't have the proper funding")));
            //have a popup that says something to the effect of not enough money
        }
        //buys the item associated with the button

    }

    private void getTextbook()
    {

       //ystemManager.instance.playerIntelligence += 30;
        Debug.Log("After receiving a textbook, you feel like you've gotten smarter");
       
        
        
    }
   
    

    private void studyRate()
    {
        Debug.Log("With a new desk, you will be better at studying");
    }
   private void addContacts()
    {

    }


    private void  rollLotterySmall()
    {
        //StartCoroutine(display(new Dialogue("", "You have received a Normal Lottery Ticket, best of luck!")));
        string str = lottery.GenerateCPrize();
        StartCoroutine(handleDialog(str));
        //items[str]();
    }
    

    private void rollLotteryMedium()
    {
        //StartCoroutine(display(new Dialogue("", "You have received a Fancy Lottery Ticket, best of luck!")));
        string str = lottery.GenerateBPrize();
        StartCoroutine(handleDialog(str));
        //items[str]();
        
    }
    private void doNothing()
    {
        //StartCoroutine(display (new Dialogue("", "You have gained nothing from this exchange")));
        Debug.Log("You get nothing ");
    }
    private void rollLotteryLarge()
    {
        //StartCoroutine(display(new Dialogue("", "You have received an Extreme Lottery Ticket, best of luck!")));
        string str = lottery.GenerateAPrize();
        StartCoroutine(handleDialog(str));
        //items[str]();
    }
    private void addLifeCardFragment()
    {
        SystemManager.instance.playerLifeCardFragment++;
        Debug.Log("Life is in your grasp");
    }

    public IEnumerator handleDialog(string itemName)
    {
      Debug.Log("got here " + itemName);
        if (itemName == "Textbook")
        {
            //  Debug.Log("this is dumb");
            yield return display(new Dialogue("", "After receiving a textbook, you feel like you've gotten smarter"));
            items[itemName]();
            StartCoroutine(AddScore(30));
        }
        else if (itemName == "Fancy Desk")
        {
            //  Debug.Log("this is fancy desk");
            yield return display(new Dialogue("", "With a new desk, you will be better at studying"));
            items[itemName]();
        }
        else if (itemName == "Fancy Cell Phone")
        {
            //  Debug.Log("this is fancy cell phone");
            yield return null; //display(new Dialogue "", "")
            items[itemName]();
        }
        else if (itemName == "Lottery Ticket")
        {
            //  Debug.Log("this is Lottery Ticket");
            yield return display(new Dialogue("", "You have received a Normal Lottery Ticket, best of luck!"));
            items[itemName]();
        }
        else if (itemName == "Fancy Lottery Ticket")
        {
            // Debug.Log("this is Fancy Lottery Ticket");
            yield return display(new Dialogue("", "You have received a Fancy Lottery Ticket, best of luck!"));
            items[itemName]();
        }
        else if (itemName == "Extreme Lottery Ticket")
        {
            // Debug.Log("this is Extreme Lottery Ticket");
            yield return display(new Dialogue("", "You have received an Extreme Lottery Ticket, best of luck!"));
            items[itemName]();
        }
        else if (itemName == "Nothing")
        {
            Debug.Log("this is nothing");
            yield return display(new Dialogue("", "You have gained nothing from this exchange"));
        }
        else if (itemName == "10")
        {
            yield return display(new Dialogue("", "You have received 10 dollars!"));

            AdjustCash(10);
        }
        else if (itemName == "100")
        {
            yield return display(new Dialogue("", "You have received 100 dollars!"));
            AdjustCash(100);
        }
        else if (itemName == "500")
        {
            yield return display(new Dialogue("", "You have received 500 dollars!"));
            AdjustCash(500);

        }
        else if (itemName == "1000")
        {
            yield return display(new Dialogue("", "You have received 1000 dollars!"));
            AdjustCash(1000);
        }
        else if (itemName == "Life Card Fragment")
        {
            yield return display(new Dialogue("", "Life is in your grasp"));
            items[itemName]();
        }
        
        //yield return display(new Dialogue("",""));
        //yield return display(new Dialogue("",""));
        //yield return display(new Dialogue("",""));
        //yield return display(new Dialogue("",""));
        //yield return display(new Dialogue("",""));
        //yield return display(new Dialogue("",""));
        //yield return display(new Dialogue("",""));
        //yield return display(new Dialogue("",""));
    }

public IEnumerator display(Dialogue dialogue)
    {
        //Debug.Log(dialogue.content + " this is the dialog content");
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
        //SystemManager.instance.playerIntelligence += value;
      //SystemManager.instance.uiManager.PromptIntelligenceChange(value);
        //add numbers one by one
        
        //construc the sentence displayed before 
       
       //ield return SystemManager.instance.dialogueManager.DisplaySentence(dialogue);
        StartCoroutine(SystemManager.instance.uiManager.AddValueToUI(SystemManager.instance.uiManager.intelligenceText, value));
        
        yield return null;
      //StartCoroutine(SystemManager.instance.uiManager.AddValueToUI(SystemManager.instance.uiManager.intelligenceText, value));
        //syield return SystemManager.instance.uiManager.AddValueToUI(SystemManager.instance.uiManager.intelligenceText, value);
            //yield return null;
        
    }
    public void AdjustCash(int value)
    {
      //Debug.Log("TOTAL AMOUNT OF MONEY" + SystemManager.instance.playerMoney);
        
      //SystemManager.instance.playerMoney-= (value);
        //SystemManager.instance.uiManager.PromptIntelligenceChange(value);
        //add numbers one by one
     // Debug.Log(SystemManager.instance.playerMoney);
        
        
            //UPDATE UI
        StartCoroutine(SystemManager.instance.uiManager.AddValueToUI(SystemManager.instance.uiManager.moneyText, value));

        // yield return null;
       

        //int intelligence = IntelligenceIncrease();
        //construc the sentence displayed before 
        //Dialogue dialogue = new Dialogue("", "After studying hard, your intelligence increases by " + intelligence + " .");
        //yield return SystemManager.instance.dialogueManager.DisplaySentence(dialogue);
        //StartCoroutine(SystemManager.instance.uiManager.AddValueToUI(SystemManager.instance.uiManager.intelligenceText, intelligence));
    }

    
};

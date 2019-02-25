using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int money;//just used for testing purposes
    public Dictionary<string,System.Action> items = new Dictionary<string,System.Action>();
    RNGGenerator rng = new RNGGenerator();

    private void Start()
    {
        items.Add("Textbook", addIntelligence);
        items.Add("Fancy Desk", intelligenceModifier);
        items.Add("Fancy Cell Phone", addContacts);
        //items.Add("Cheap Lottery", rng.GenerateCPrize);
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
            items[itemNC.Substring(i + 1)]();
            
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
        SystemManager.instance.playerIntelligence += 3;
        
    }
    private void intelligenceModifier()
    {

    }
    private void addContacts()
    {

    }
}

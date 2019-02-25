using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int money;//just used for testing purposes

    public void makePurchase(int cost,string itemName)
    {   
        if (cost <= SystemManager.instance.playerMoney)
        {
            SystemManager.instance.playerMoney -= cost;
            
            //do the item action
        }
        else
        {
            //have a popup that says something to the effect of not enough money
        }
        //buys the item associated with the button

    }


}

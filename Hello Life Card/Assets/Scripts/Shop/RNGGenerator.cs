using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RNGGenerator : MonoBehaviour
{
    public Dictionary<int, string> cheapLottery = new Dictionary<int, string>();
    private int randomPercentage()
    {
        return Random.Range(1, 1001);
    }
    //LOTTERY TYPES
    // total/1000 = % so 1/1000 = .1%
    //10$
    //  1-599 Nothing
    //  600-749 Lottery Ticket
    //  750-774 Fancy Lottery Ticket
    //  775-784 Extreme Lottery Ticket
    //  785- 884 $10
    //  885- 909 $100
    //  910-919 $500
    //  920-924 $1000
    //  925-974 Textbook
    //  975-999 Fancy Desk
    // 1000 Life Card Fragment

    void GeneratePrize(int ticketPrice)
    {
        int landed = this.randomPercentage();
        if (ticketPrice == 10)
        {
            //handlecsmall 
        }
        else if(ticketPrice == 100)
        {
            //handlesmall
        }
        else
        {
            //handlelarge
        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RNGGenerator 
{
    public Dictionary<int, string> cheapLottery = new Dictionary<int, string>();
    string[]prizes = new string[1000];
    private int randomPercentage()
    {
        return Random.Range(0, 1000);
    }
    //LOTTERY TYPES
    // total/1000 = % so 1/1000 = .1%
    //10$
    //  0-598 Nothing
    //  599-748 Lottery Ticket
    //  749-773 Fancy Lottery Ticket
    //  774-783 Extreme Lottery Ticket
    //  784- 883 $10
    //  884- 908 $100
    //  909-918 $500
    //  919-923 $1000
    //  924-973 Textbook
    //  974-998 Fancy Desk
    //  999 Life Card Fragment

    public string GenerateCPrize()
    {
        int landed = this.randomPercentage();

        {
            PrizeRanges(new List<(double, string)>
            {
                (59.9,"Nothing"),
                (15.0,"Lottery Ticket"),
                (2.5,"Fancy Lottery Ticket"),
                (1.0,"Extreme Lottery Ticket"),
                (10.0,"10"),
                (2.5,"100"),
                (1.0,"500"),
                (0.5,"1000"),
                (5.0,"Textbook"),
                (2.5,"Fancy Desk"),
                (0.1,"Life Card Fragment")
            });
            return prizes[landed];

        }
    }
    public string GenerateBPrize() { 
        int landed = this.randomPercentage();
        {
            //handlemedium
            PrizeRanges(new List<(double, string)>
            {
                (49.0,"Nothing"),
                (10.0,"Lottery Ticket"),
                (5.0,"Fancy Lottery Ticket"),
                (2.5,"Extreme Lottery Ticket"),
                (5.0,"10"),
                (10.0,"100"),
                (2.5,"500"),
                (1.0,"1000"),
                (9.0,"Textbook"),
                (5.0,"Fancy Desk"),
                (1.0,"Life Card Fragment") });
            return prizes[landed];
        }
    }
        

    
    
    //generates an array where each slot is an item, which corresponds to a certain percent up to 1000
    //ie: 4% chance for $10 = [$10,$10,$10,$10,other,other,other,...]
    //win%'s can only be accurate up to one decimal place, 
    //winrate must also total 100
    public void PrizeRanges(List<(double,string)>percentages)
    {
        //makes the array of prizes based on the ticket
        int lenPercentages = percentages.Count;
        int j = 0;
        for (int i = 0; i < lenPercentages; ++i)
        {
            int firstindex = j;
            for (; j < firstindex+percentages[i].Item1*10; ++j)
            {
                prizes[j] = percentages[i].Item2;
            }
        }
    }
}

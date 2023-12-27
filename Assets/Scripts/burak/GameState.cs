using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    List<Hex> Lands;
    int TotalGold;
    int Income;
    public GameState(List<Hex> lands, int totalGold,int income)
    {
        Lands = lands;
        TotalGold = totalGold;
        Income = income;
    }

}

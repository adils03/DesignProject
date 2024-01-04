using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateH 
{
    public int TotalGold { get; set; }
    public int Income { get; set; }
    public List<Hex> Hexes { get; set; }
    public List<Hex> ProtectedHex {  get; set; }

    public List<Soldier> soldiers { get; set; }// sahip olunan asker

    /// <summary>
    /// Oyun i�in bir oyun durumu �retilecek olan s�n�f
    /// </summary>
    /// <param name="totalgold"> oyuncunun kasadaki alt�n� </param>
    /// <param name="income"> oyuncunun geliri </param>
    /// <param name="hexes"> sahip oldu�u topraklar </param>
    public GameStateH(int totalgold,int income,List<Hex> hexes)
    {
        TotalGold = totalgold;
        Income = income;
        Hexes = hexes;

    }

}

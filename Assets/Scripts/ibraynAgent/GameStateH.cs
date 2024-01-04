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
    /// Oyun için bir oyun durumu üretilecek olan sýnýf
    /// </summary>
    /// <param name="totalgold"> oyuncunun kasadaki altýný </param>
    /// <param name="income"> oyuncunun geliri </param>
    /// <param name="hexes"> sahip olduðu topraklar </param>
    public GameStateH(int totalgold,int income,List<Hex> hexes)
    {
        TotalGold = totalgold;
        Income = income;
        Hexes = hexes;

    }

}

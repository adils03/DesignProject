using System;
using System.Collections.Generic;
using UnityEngine;

public class Player 
{
    public string playerName;
    public List<Hex> ownedHexes = new List<Hex>(); //sahip olduğu hexler

    public Player(String name){
        playerName=name;
    }
    public void StartTurn()
    {
        Debug.Log(playerName + " oyuncunun sirasi basladi.");
    }
}

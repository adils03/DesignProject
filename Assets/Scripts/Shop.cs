using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
   public GameManager gameManager;
   public EconomyManager economyManager;
   public Player player;
   
    private void Awake() 
    {
       gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    public void buySoldier(ObjectType soldierType)
    {
        Player turnPlayer = gameManager.GetTurnPlayer();
        if(turnPlayer != null)
        {
            Soldier newSoldier = new Soldier(); 
            newSoldier.soldierLevel = soldierType;
            newSoldier.owner = turnPlayer; 
            turnPlayer.soldiers.Add(newSoldier);
        }
        
        economyManager = new EconomyManager();
        player.PlayerTotalGold = economyManager.CurrentGold;

    }

    public void buyTower()
    {
        
    }

    public void buyFarm()
    {

    }

}

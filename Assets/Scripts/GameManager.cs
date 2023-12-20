using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public List<String> playerNames = new List<string>(); 
    public List<Player> players = new List<Player>();
    public List<Hex> hexes;
    private TurnManager turnManager;
    private GridSystem gridSystem;
    private Player player;
    private SpawnManager spawnManager;
    [SerializeField]private TextMeshProUGUI text;

    public int Burak = 0;
    public int Halil = 0;
    public int Emin = 0;

    public int BurakIncome = 0;
    public int HalilIncome = 0;
    public int EminIncome = 0;

    public List<Hex> burakhex = new List<Hex>();
    public List<Hex> halilhex = new List<Hex>();
    public List<Hex> eminhex = new List<Hex>();
    
    private void Awake()
    {
        gridSystem = GameObject.Find("GridSystem").GetComponent<GridSystem>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        this.hexes = gridSystem.hexes;
        
        
    }
    private void Start()
    {
        StartGame();
       
    }
    private void Update()
    {
        Burak = players[0].PlayerTotalGold;
        Halil = players[1].PlayerTotalGold;
        Emin = players[2].PlayerTotalGold;

        BurakIncome = players[0].economyManager.CalculateIncome();
        HalilIncome = players[1].economyManager.CalculateIncome();
        EminIncome = players[2].economyManager.CalculateIncome();

        burakhex = players[0].ownedHexes;
        halilhex=players[1].ownedHexes;
        eminhex=players[2].ownedHexes;

        CheckPlayerTown(players, spawnManager.spawnedHouses);

        Player currPlayer = GetTurnPlayer();
        if(currPlayer!=null)
        {
            string a = "Turn: " + currPlayer.playerName;
            a += $"\nGold :{currPlayer.PlayerTotalGold}";
            a += $"\nIncome :{currPlayer.economyManager.totalIncome}";
            text.text = a;
        }
      

    }
    void StartGame()
    {
        players.Clear();
        players.Add(new Player("Burak"));
        players.Add(new Player("Halil"));
        players.Add(new Player("Emin"));

        turnManager = new TurnManager(players);        
        
        spawnManager.SpawnLandOfPlayers(gridSystem.size,players);
        spawnManager.SpawnTrees();// ağaçlar sonra eklenmeli yosam ağaç olan yere ev kuruyor
      

    }
    public void endTurn() //Buton ataması için konulmuştur.
    {
        turnManager.StartTurn();
        DeathCheck();
        foreach(Player player in players)
        {
            foreach (Soldier soldier in player.soldiers)
            {
                soldier.hasMoved = false;
                if(GetTurnPlayer()!=player){
                    soldier.GetComponent<CircleCollider2D>().enabled=false;
                }
                else{
                    soldier.GetComponent<CircleCollider2D>().enabled=true;
                }
            }
        }
        //text.text="Turn: " + GetTurnPlayer().playerName;
        Debug.Log(players[0].playerName);
        if (turnManager.turnQueue.Count == 0) 
        {
            spawnManager.TreesSpread();
        }
    
    }

    public Player GetTurnPlayer()
    {
        if(turnManager.turnQueue.Count==0)
        {
            return players[0];
        }
        return turnManager.turnQueue.Peek();
    }

    void CheckPlayerTown(List<Player> players, List<Hex> spawnedHouses)//playerın evi yıkıldı mı yıkılmadı mı onu kontrol ediyo sürekli kontrol etmeyi update yazdım 
    {
    for (int i = 0; i < players.Count; i++)
    {
        Player player = players[i];
        Hex playerHex = spawnedHouses[i];

        if (playerHex.HexObjectType != ObjectType.TownHall)
        {
            player.Death();
        }
    }
    }

    void DeathCheck()// Parası 0, income 0 ve hiç askeri olmadığında ölmesini sağlıyor
    {
        Player currentPlayer = GetTurnPlayer();

        if(currentPlayer.PlayerTotalGold == 0 && currentPlayer.soldiers.Count == 0 && currentPlayer.economyManager.totalIncome == 0 )
        {
            currentPlayer.Death();
            if (turnManager.turnQueue.Count > 0) 
            {
                turnManager.turnQueue.Dequeue();
            }
        }
    }
}

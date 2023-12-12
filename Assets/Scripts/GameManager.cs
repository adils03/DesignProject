using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public List<String> playerNames = new List<string>(); 
    public List<Player> players = new List<Player>();
    public List<Hex> hexes;
    private TurnManager turnManager;
    private EconomyManager economyManager;
    private GridSystem gridSystem;
    private SpawnManager spawnManager;
    [SerializeField]private Text text;

    public int Burak = 0;
    public int Halil = 0;
    public int Emin = 0;

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

    }
    void StartGame()
    {
        players.Clear();
        players.Add(new Player("Burak"));
        players.Add(new Player("Halil"));
        players.Add(new Player("Emin"));


        turnManager = new TurnManager(players);
        
        
        spawnManager.SpawnLandOfPlayers(gridSystem.size,players);
        spawnManager.SpawnTrees();
        text.text="Turn: " + turnManager.players[0].playerName;
    }
    public void endTurn() //Buton ataması için konulmuştur.
    {
        turnManager.StartTurn();
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
        text.text="Turn: " + GetTurnPlayer().playerName;
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
    
}

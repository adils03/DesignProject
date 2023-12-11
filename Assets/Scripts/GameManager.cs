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
        text.text="Turn: " + players[0].playerName;
        spawnManager.SpawnLandOfPlayers(gridSystem.size,players);
        spawnManager.SpawnTrees();
    }
    public void endTurn() //Buton ataması için konulmuştur.
    {
        foreach(Player player in players)
        {
            foreach (Soldier soldier in player.soldiers)
            {
                soldier.hasMoved = false;
                
            }
        }
        turnManager.StartTurn();
        if(turnManager.turnQueue.Count==0)
        {
            text.text="Turn: " + players[0].playerName;
        }else{
            text.text="Turn: " + turnManager.turnQueue.Peek().playerName;
        }
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

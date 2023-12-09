using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<String> playerNames = new List<string>(); 
    public List<Player> players = new List<Player>();
    public List<Hex> hexes;
    private TurnManager turnManager;
    private EconomyManager economyManager;
    private GridSystem gridSystem;
    private SpawnManager spawnManager;
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

    }
    void StartGame()
    {
        players.Clear();
        players.Add(new Player("Burak"));
        players.Add(new Player("Halil"));
        players.Add(new Player("Emin"));



        //assingPlayers();
        turnManager = new TurnManager(players);
        spawnManager.SpawnLandOfPlayers(gridSystem.size,players);
        spawnManager.SpawnTrees();
    }
    public void endTurn() //Buton ataması için konulmuştur.
    {
        turnManager.StartTurn();
        spawnManager.TreesSpread();// ağaç yayılma test
    }
    void assingPlayers()
    { //Oyuncuları unity ekranından istediğimiz sayıda ve isimde atamamızı sağlar

        spawnManager.SpawnTrees();// burda test ediyorum evet biraz çorba

        Hex selected = gridSystem.FindHex(0, 0);
        List<Hex> devletHexs = new List<Hex>();
        devletHexs.Add(selected);
        foreach (var hex in selected.neighbors)
        {
            if (hex != null && hex)
                devletHexs.Add(hex);
        }//Buraya rastgelelik eklenecek
        devletHexs.Add(gridSystem.FindHex(-2,0));
        devletHexs.Add(gridSystem.FindHex(-3,0));

        foreach (String names in playerNames)
        {
            Player newPLayer = new Player(names,devletHexs,Color.black);
            players.Add(newPLayer);
        }
    }




}

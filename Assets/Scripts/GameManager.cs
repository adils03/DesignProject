using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public List<Player> players = new List<Player>();
    public List<Hex> hexes;
    public List<Vector2> playerStartingPositions = new List<Vector2>();
    private TurnManager turnManager = new TurnManager();
    //private EconomyManager economyManager;
    private GridSystem gridSystem;
    public Hex denemhex;
    private void Awake()
    {
        gridSystem = GameObject.Find("GridSystem").GetComponent<GridSystem>();
        //turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        //economyManager = GameObject.Find("EconomyManager").GetComponent<EconomyManager>();
        turnManager.players = players;

        this.hexes = gridSystem.hexes;
    }
    private void Start()
    {
        StartGame();
        
    }
    private void Update() {
        if (Input.GetKey(KeyCode.T))
        {
            denemhex=gridSystem.FindHex(0,0);
        }
    }
    void StartGame()
    {
        assingPlayers();
    }
    void assingPlayers()
    { //Oyuncuları atar

        Hex selected = gridSystem.FindHex(0, 0);
        Debug.Log(selected);
        List<Hex> devletHexs = new List<Hex>();
        devletHexs.Add(selected);

        foreach (var hex in selected.neighbors)
        {
            if (hex != null && hex)
                devletHexs.Add(hex);
        }
        Debug.Log(devletHexs[1]);



        Player player1 = new Player("adil", devletHexs);
        Player player2 = new Player("ibo");
        Player player3 = new Player("burak");
        players.Add(player1);
        players.Add(player2);
        players.Add(player3);
    }




}

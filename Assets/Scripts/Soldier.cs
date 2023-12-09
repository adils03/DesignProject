using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public ObjectType soldierLevel;
    public Hex onHex;
    public Player owner;
    public GameManager gameManager;
    public String playerName;
    private void Start() {
        gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();
        owner=gameManager.players[0];
        playerName=owner.playerName;
    }
}

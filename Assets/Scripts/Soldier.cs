using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public ObjectType soldierLevel;
    public Hex onHex;
    public Player owner;
    public String playerName;
    public bool hasMoved = false;//Asker tur içinde yürüdü mü
    private void OnDestroy() {
        owner.soldiers.Remove(this);
    }
    private void Start() {
        playerName=owner.playerName;
    }

    public void activateIndicator(bool request){
        transform.GetChild(0).gameObject.SetActive(request);
    }
}


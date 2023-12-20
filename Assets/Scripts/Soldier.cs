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

    public void walkToHex(Hex hex){
        if (hex.HexObjectType == ObjectType.Tree || hex.HexObjectType == ObjectType.TreeWeak)
        {
            hex.destroyObjectOnHex();
            owner.PlayerTotalGold += 4;
        }
        else if (hex.ObjectOnHex != null)
        {
            hex.destroyObjectOnHex();
        }
        onHex.HexObjectType = ObjectType.None;
        onHex.ObjectOnHex = null;
        hex.ObjectOnHex = this.gameObject;
        hex.HexObjectType = soldierLevel;
        if (hex.Owner != null && owner != hex.Owner)
        {
            hex.Owner.ownedHexes.Remove(hex);
        }
        hex.Owner = owner;
        hex.Owner.ownedHexes.Add(hex);
        hex.playerName = playerName;
        hex.GetComponent<SpriteRenderer>().color = owner.playerColor;
        onHex = hex;
        transform.position = hex.transform.position;
        hasMoved = true;
        hex.UpdateAdvantageOrDisadvantageValue();
    }
}


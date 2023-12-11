using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
    bool canWalk = false;
    public GameObject soldier;
    List<Hex> walkableArea;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cast();
        }

    }

    void cast()
{
    Vector2 soldierRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    RaycastHit2D hit = Physics2D.Raycast(soldierRay, Vector2.zero);
    if (hit.collider != null)
    {
        GameObject hitObject = hit.collider.gameObject;
        if (hitObject.tag == "Soldier" && !canWalk)
        {
            HandleSoldierHit(hitObject);
        }
        else if (hitObject.tag == "Hex" && canWalk)
        {
            HandleHexHit(hitObject);
        }
        else
        {
            ResetWalk();
        }
    }
}

void HandleSoldierHit(GameObject soldierHit)
{
    soldier = soldierHit;
    if(gameManager.GetTurnPlayer() == soldier.GetComponent<Soldier>().owner && !soldier.GetComponent<Soldier>().hasMoved)
    {
        canWalk = true;
        walkableArea = soldier.GetComponent<Soldier>().onHex.travelContinentByStep(4);
        foreach (Hex hex in walkableArea)
        {
            hex.activateIndicator(true);
        }
    }
}

void HandleHexHit(GameObject hexHit)
{
    Hex hexComponent = hexHit.GetComponent<Hex>();
    if (walkableArea != null && walkableArea.Contains(hexComponent))
    {
        ProcessValidHex(hexComponent);
    }
    else
    {
        ResetWalk();
    }
}

void ProcessValidHex(Hex hex)
{
    if(hex.HexObjectType == ObjectType.Tree || hex.HexObjectType == ObjectType.TreeWeak)
    {
        hex.destroyObjectOnHex();
        soldier.GetComponent<Soldier>().owner.PlayerTotalGold += 4;
        Debug.Log(soldier.GetComponent<Soldier>().owner.PlayerTotalGold);
    }
    soldier.GetComponent<Soldier>().onHex.HexObjectType = ObjectType.None;
    soldier.GetComponent<Soldier>().onHex.ObjectOnHex = null;
    hex.ObjectOnHex = soldier;
    hex.HexObjectType = soldier.GetComponent<Soldier>().soldierLevel;
    hex.Owner = soldier.GetComponent<Soldier>().owner;
    hex.Owner.ownedHexes.Add(hex);
    hex.playerName = soldier.GetComponent<Soldier>().playerName;
    hex.GetComponent<SpriteRenderer>().color = soldier.GetComponent<Soldier>().owner.playerColor;
    soldier.GetComponent<Soldier>().onHex = hex;
    soldier.transform.position = hex.transform.position;
    canWalk = false;
    soldier.GetComponent<Soldier>().hasMoved = true;
    hex.UpdateAdvantageOrDisadvantageValue();
    foreach (Hex _hex in walkableArea)
    {
        _hex.activateIndicator(false);
    }
}


void ResetWalk()
{
    canWalk = false;
    if (walkableArea != null)
    {
        foreach (Hex hex in walkableArea)
        {
            hex.activateIndicator(false);
        }
    }
}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMovement : MonoBehaviour
{
    public GameObject soldier;
    List<Hex> walkableArea;
    public GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Walk();
        }

    }

    void Walk()
    {
        Vector2 soldierRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D soldierHit = Physics2D.Raycast(soldierRay, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Soldier"));
        if (soldierHit.collider != null)
        {
            GameObject hitObject = soldierHit.collider.gameObject;
            if (hitObject.tag == "Soldier")
            {
                HandleSoldierHit(hitObject);
            }
            else if (hitObject.tag == "Hex")
            {
                HandleHexHit(hitObject);
            }
            else
            {
                ResetWalk();
            }
        }
        else
        {
            // Hexagon'a tıklanıp tıklanmadığını kontrol et
            RaycastHit2D hexHit = Physics2D.Raycast(soldierRay, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Hex"));
            if (hexHit.collider != null)
            {
                GameObject hitObject = hexHit.collider.gameObject;
                if (hitObject.tag == "Hex")
                {
                    HandleHexHit(hitObject);
                }
                else
                {
                    ResetWalk();
                }
            }
        }
    }

    void HandleSoldierHit(GameObject soldierHit)//Askere tıklamak için
    {
        soldier = soldierHit;
        if (gameManager.GetTurnPlayer() == soldier.GetComponent<Soldier>().owner && !soldier.GetComponent<Soldier>().hasMoved)
        {
            walkableArea = soldier.GetComponent<Soldier>().onHex.travelContinentByStepForSoldier(4, soldier.GetComponent<Soldier>().owner, soldier.GetComponent<Soldier>().soldierLevel);
            foreach (Hex hex in walkableArea)
            {
                hex.activateIndicator(true);
            }
        }
    }

    void HandleHexHit(GameObject hexHit)//Hexe tıklamak için
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

    void ProcessValidHex(Hex hex) //Askerin yürüdüğü toprağın parametrelerini ayarlar 
    {
        Soldier soldierSc = soldier.GetComponent<Soldier>();
        if (hex.HexObjectType == ObjectType.Tree || hex.HexObjectType == ObjectType.TreeWeak)
        {
            hex.destroyObjectOnHex();
            soldierSc.owner.PlayerTotalGold += 4;
        }
        else if (hex.ObjectOnHex != null)
        {
            hex.destroyObjectOnHex();
        }
        soldierSc.onHex.HexObjectType = ObjectType.None;
        soldierSc.onHex.ObjectOnHex = null;
        hex.ObjectOnHex = soldier;
        hex.HexObjectType = soldierSc.soldierLevel;
        if (hex.Owner != null && soldierSc.owner != hex.Owner)
        {
            hex.Owner.ownedHexes.Remove(hex);
        }
        hex.Owner = soldierSc.owner;
        hex.Owner.ownedHexes.Add(hex);
        hex.playerName = soldierSc.playerName;
        hex.GetComponent<SpriteRenderer>().color = soldierSc.owner.playerColor;
        soldierSc.onHex = hex;
        soldier.transform.position = hex.transform.position;
        soldierSc.hasMoved = true;
        hex.UpdateAdvantageOrDisadvantageValue();
        foreach (Hex _hex in walkableArea)
        {
            _hex.activateIndicator(false);
        }
        walkableArea = null;
    }


    void ResetWalk()
    {
        if (walkableArea != null)
        {
            foreach (Hex hex in walkableArea)
            {
                hex.activateIndicator(false);
            }
            walkableArea = null;
        }
    }

}

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
        Vector2 soldierRAy = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(soldierRAy, Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Soldier" && !canWalk)
            {
                soldier = hit.collider.gameObject;
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
            else if (hit.collider.gameObject.tag == "Hex" && canWalk)
            {
                if (walkableArea != null && walkableArea.Contains(hit.collider.gameObject.GetComponent<Hex>()))
                {
                    if(hit.collider.gameObject.GetComponent<Hex>().HexObjectType==ObjectType.Tree||hit.collider.gameObject.GetComponent<Hex>().HexObjectType==ObjectType.TreeWeak){
                        hit.collider.gameObject.GetComponent<Hex>().destroyObjectOnHex();
                        soldier.GetComponent<Soldier>().owner.PlayerTotalGold+=4;
                        Debug.Log(soldier.GetComponent<Soldier>().owner.PlayerTotalGold);
                    }
                    soldier.GetComponent<Soldier>().onHex.HexObjectType = ObjectType.None;
                    soldier.GetComponent<Soldier>().onHex.ObjectOnHex=null;
                    hit.collider.gameObject.GetComponent<Hex>().ObjectOnHex=soldier;
                    hit.collider.gameObject.GetComponent<Hex>().HexObjectType = soldier.GetComponent<Soldier>().soldierLevel;
                    hit.collider.gameObject.GetComponent<Hex>().Owner = soldier.GetComponent<Soldier>().owner;
                    hit.collider.gameObject.GetComponent<Hex>().Owner.ownedHexes.Add(hit.collider.gameObject.GetComponent<Hex>());
                    hit.collider.gameObject.GetComponent<Hex>().playerName = soldier.GetComponent<Soldier>().playerName;
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().color = soldier.GetComponent<Soldier>().owner.playerColor;
                    soldier.GetComponent<Soldier>().onHex = hit.collider.gameObject.GetComponent<Hex>();
                    soldier.transform.position = hit.collider.transform.position;
                    canWalk = false;
                    soldier.GetComponent<Soldier>().hasMoved = true;
                    hit.collider.gameObject.GetComponent<Hex>().UpdateAdvantageOrDisadvantageValue();
                    foreach (Hex hex in walkableArea)
                    {
                        hex.activateIndicator(false);
                    }
                }
                else
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
            else
            {
                canWalk = false;
            }
        }
        else
        {
            canWalk = false;
        }
    }
}

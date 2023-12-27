using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private GameManager gameManager;
    private SpawnManager spawnManager;
    private Hex selectedHex;
    Hex startHex;
    private bool isWaitingForInput = true;

    // defence buldingevel1 için cost 15 , level2 için cost 35


    List<Hex> placeAbleArea = new List<Hex>();
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
    public void buySoldier()// level 1 asker
    {
        buyAnyOfMaterial(ObjectType.SoldierLevel1, 10);
    }
    public void buySoldier2()// level 2 asker
    {
        buyAnyOfMaterial(ObjectType.SoldierLevel2, 20);
    }
    public void buySoldier3()// level 3 asker
    {
        buyAnyOfMaterial(ObjectType.SoldierLevel3, 30);
    }
    public void buySoldier4()// level 4 asker
    {
        buyAnyOfMaterial(ObjectType.SoldierLevel4, 40);
    }
    public void buyFarm()// her binadan sonra coastı artar
    {
        int cost = 12;
        Player currentPlayer = gameManager.GetTurnPlayer();

        int a = currentPlayer.ownedHexes.Where(x => x.HexObjectType == ObjectType.BuildingFarm).ToList().Count;

        cost = cost + a * 5;

        buyAnyOfMaterial(ObjectType.BuildingFarm, cost);
    }

    public void buyAnyOfMaterial(ObjectType s, int cost)// 
    {

        Player currentPlayer = gameManager.GetTurnPlayer();
        if (currentPlayer.PlayerTotalGold >= cost)//costtan fazla parası var mı yok mu
        {
            placeAbleArea = new List<Hex>();    

            if (s == ObjectType.SoldierLevel1 || s == ObjectType.SoldierLevel2 || s == ObjectType.SoldierLevel3 || s == ObjectType.SoldierLevel4)
            {
                placeAbleArea.Clear();
                startHex=currentPlayer.ownedHexes[0];
                placeAbleArea = startHex.travelContinentByStepForSoldier(50, currentPlayer, s);
                Debug.Log(currentPlayer.playerName);

            }
            else if(s==ObjectType.BuildingFarm){
                placeAbleArea.Clear();
                foreach (Hex hex in currentPlayer.ownedHexes)
                {
                    if (hex.HexObjectType == ObjectType.BuildingFarm||hex.HexObjectType == ObjectType.TownHall)//düzeltilecek
                    {
                        foreach (Hex hex1 in hex.neighbors)
                        {
                            if(currentPlayer.ownedHexes.Contains(hex1)&&hex1.HexObjectType==ObjectType.None){
                            placeAbleArea.Add(hex1);                                
                            }
                        }
                    }
                }
            }
            PlaceAbleAreaSet(true);
            isWaitingForInput = true;
            StartCoroutine(WaitForHexSelection(s, cost));// selectedHex gelcek ve ücret
        }

    }

    private IEnumerator WaitForHexSelection(ObjectType spawnObje, int a)// anyType
    {
        Player currentPlayer = gameManager.GetTurnPlayer();
        while (isWaitingForInput)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 soldierRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hexHit = Physics2D.Raycast(soldierRay, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Hex"));

                if (hexHit.collider != null && hexHit.collider.gameObject.GetComponent<Hex>() != null)
                {
                    selectedHex = hexHit.collider.gameObject.GetComponent<Hex>();
                    if (placeAbleArea.Contains(selectedHex))
                    {
                        spawnManager.SpawnObje(selectedHex, spawnObje, currentPlayer);
                        isWaitingForInput = false; // �stenilen durum ger�ekle�ti�inde d�ng�y� sonland�r
                        selectedHex.Owner.PlayerTotalGold -= a;// ücret kesildi
                    }
                    else
                    {
                        PlaceAbleAreaReset();
                    }
                }
            }
            yield return null;
        }
        PlaceAbleAreaReset();
    }
    void PlaceAbleAreaSet(bool a)
    {
        if (placeAbleArea != null)
        {
            foreach (Hex hex in placeAbleArea)
            {
                hex.activateIndicator(a);
            }
        }
    }
    void PlaceAbleAreaReset()
    {
        if (placeAbleArea != null)
        {
            foreach (Hex hex in placeAbleArea)
            {
                hex.activateIndicator(false);
            }
        }
        //placeAbleArea.Clear();
    }




}

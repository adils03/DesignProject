using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameManager gameManager;
    public SpawnManager spawnManager;
    private Hex selectedHex;
    private bool isWaitingForInput = true;



    List<Hex> placeAbleArea = new List<Hex>();
    private void Awake() 
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
    private void Update()
    {
        Debug.Log("isWaitngFor: "+isWaitingForInput);
    }
    public void buySoldier()// level 1 asker
    {
        Debug.Log("Buysoldier girdi");
        Player currentPlayer = gameManager.GetTurnPlayer();
        
        placeAbleArea = currentPlayer.ownedHexes;
        PlaceAbleAreaSet(true);

        if(currentPlayer.PlayerTotalGold>=-100)// ücretten az ise vermem kardeþim asker masker þimdilik -100dedim
            StartCoroutine(WaitForHexSelection(ObjectType.SoldierLevel1));// selectedHex gelcek
        else
            PlaceAbleAreaReset();

        return;


    }
    private IEnumerator WaitForHexSelection(ObjectType spawnObje)// anyType
    {
        while (isWaitingForInput)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 soldierRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hexHit = Physics2D.Raycast(soldierRay, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Hex"));

                if (hexHit.collider != null)
                {
                    selectedHex = hexHit.collider.gameObject.GetComponent<Hex>();

                    if (placeAbleArea.Contains(selectedHex))
                    {
                        //spawnManager.SpawnSoldier(selectedHex,spawnObje);
                        spawnManager.SpawnObje(selectedHex,spawnObje);
                        isWaitingForInput = false; // Ýstenilen durum gerçekleþtiðinde döngüyü sonlandýr
                        PlaceAbleAreaReset();
                        selectedHex.Owner.PlayerTotalGold -= 10;// ücret kesildi
                    }
                }
            }

            yield return null;
        }
        isWaitingForInput = true;
       
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
            placeAbleArea = null;
        }
    }


    public void buyTower()
    {
        
    }

    public void buyFarm()
    {

    }

}

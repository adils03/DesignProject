using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    public int x,y;
    GridSystem gridSystem;
    public GameObject housePrefab;
      public GameObject soldierPrefab;
    private void Awake() {
        gridSystem = GameObject.Find("GridSystem").GetComponent<GridSystem>();
    }
     private void Update() {
        if(Input.GetKey(KeyCode.T))
        {
            SpawnSoldier(0,0);
        }
    }
    public void SpawnBuilding(int x,int y){

        Hex uygunHex = gridSystem.FindHex(x, y); 
        if (uygunHex != null && !uygunHex.HexEmpty)
        {
            Instantiate(housePrefab, new Vector3(uygunHex.transform.position.x,uygunHex.transform.position.y), Quaternion.identity);
            uygunHex.HexEmpty = true;
        }
       
    }// oyuncuların başlangış noktalarını atan metod
    public void BuildHouse(int x, int y)
    {
        SpawnBuilding(x, y);
        Hex hex = gridSystem.FindHex(x, y);
        Vector3 position = gameObject.transform.position;
        Debug.Log("Evin X koordinatı: " + position.x + ", Y koordinatı: " + position.y);
    }

    public void SpawnSoldier(int x, int y)
    {
        Hex uygunHex = gridSystem.FindHex(x, y); 
        if (uygunHex != null && !uygunHex.HexEmpty)
        {
            GameObject soldier;
            soldier = Instantiate(soldierPrefab, new Vector3(uygunHex.transform.position.x,uygunHex.transform.position.y), Quaternion.identity);
            uygunHex.HexEmpty = true;

            soldier.GetComponent<Soldier>().onHex = uygunHex;
        }

    }

   /* public class Tower : Unite
{
    SpawnManager spawnManager;
    GridSystem gridSystem;
    public List<Hex> ProtectedHexes = new List<Hex>();
     private void Awake() {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        gridSystem = GameObject.Find("GridSystem").GetComponent<GridSystem>();
    }

  

    public void DestroyTower()
    {
        foreach (Hex hex in ProtectedHexes)
        {
            hex.SetProtected = false;
        }
        ProtectedHexes.Clear();
    }
    
    private List<Hex> GetNeighboringHexes(int x, int y)
    { 
        Hex hex = gridSystem.FindHex(x, y); 
        return new List<Hex>();
    }*/

    /*public void BuildTower(int x, int y)
    {
        SpawnBuilding(x, y);

        Hex hex = gridSystem.FindHex(x, y);
        Vector3 position = gameObject.transform.position;
        Debug.Log("Objenin X koordinatı: " + position.x + ", Y koordinatı: " + position.y);

        
        List<Hex> neighboringHexes = hex.neighbors;
        foreach (Hex neighbour in neighboringHexes)
        {
            housePrefab.GetComponent<Tower>().ProtectedHexes.Add(neighbour);
            neighbour.SetProtected = true;
            Vector3 neighbourPosition = neighbour.transform.position;
            Debug.Log("Komşu Hex'in X koordinatı: " + neighbourPosition.x + ", Y koordinatı: " + neighbourPosition.y);
        }
        
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.X))
        {
            BuildTower(0,0);
        }
        if(Input.GetKey(KeyCode.C))
        {
            BuildHouse(3,2);
        }
    }
*/

}

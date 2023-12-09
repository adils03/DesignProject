using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    public int x,y;
    GridSystem gridSystem;
    Hex hex;
    public GameObject housePrefab;
    public GameObject soldierPrefab;
    public List<Hex> continent = new List<Hex>();
    public Color playerColor;
    
    
    private void Awake() {
        gridSystem = GameObject.Find("GridSystem").GetComponent<GridSystem>();
    }
    private void Update() {
    if (Input.GetKeyDown(KeyCode.Space)) 
    {
        int size = gridSystem.size;
        SpawnHouse(size);
    }
      
    }
    public void SpawnHouse(int size)
    {
        List<Hex> grassHexes = gridSystem.hexes.Where(hex => hex._hexType == Hex.hexType.grass).ToList();
        List<Hex> spawnedHouses = new List<Hex>();
        int numberOfHouses = 4; 

        for (int i = 0; i < numberOfHouses; i++)
        {
            if (grassHexes.Count > 0)
            {

                Hex randomHex = grassHexes[UnityEngine.Random.Range(0, grassHexes.Count)];
                do
                {
                    randomHex = grassHexes[UnityEngine.Random.Range(0, grassHexes.Count)];
                }
                while (spawnedHouses.Any(house => GridSystem.FindDistanceBetweenHexes(house, randomHex) < 10));
                Instantiate(housePrefab, randomHex.transform.position, Quaternion.identity);
                grassHexes.RemoveAll(hex => GridSystem.FindDistanceBetweenHexes(hex, randomHex) < 10);
                spawnedHouses.Add(randomHex);
            }
        }

    }// oyuncuların başlangış noktalarını atan metod

    public void spawnDevletis(List<Hex> houseHexes)
    {
    
        foreach (Hex startHex in houseHexes)
        {
        if (startHex == null || startHex.HexEmpty) 
        {
            continue;
        }

        foreach (Hex center in houseHexes)
        {
            if (GridSystem.FindDistanceBetweenHexes(startHex, center) < 10)
            {   
                continue;
            }
        }

        List<Hex> ownedHexes = Player.ownedHexes;
        Hex.hexType hexType = Hex.hexType.grass;
        List<Hex> BuildHouse = new List<Hex>();

        List<Hex> devletHexes = travelContinentSpawn(startHex, 7); 


        if (devletHexes.Count == 7)
        {
            foreach (Hex hex in devletHexes)
            {
                BuildHouse.Add(hex);
                hex.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            }
            
            BuildHouse.Add(startHex);
            startHex.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        
        
        }
    }
  
    public List<Hex> travelContinentSpawn(Hex startHex, int count) 
    {
    Stack<Hex> stack = new Stack<Hex>();

    stack.Push(startHex);

    Hex.hexType __hexType = startHex._hexType;

    while (stack.Count > 0 && continent.Count < count)
    {
        Hex currentHex = stack.Pop();

        if (!currentHex.hasVisited && currentHex._hexType == __hexType)
        {
            continent.Add(currentHex);
            currentHex.hasVisited = true;

            if (continent.Count == count)
            {
                break; 
            }

            foreach (Hex neighbor in currentHex.neighbors)
            {
                if (!neighbor.hasVisited)
                {
                    stack.Push(neighbor);
                }
            }
        }
    }
    foreach (Hex hex in continent)
    {
        hex.hasVisited = false;
    }
    return continent;
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
        
    }*/

}

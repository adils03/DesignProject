using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.UIElements;


public class SpawnManager : MonoBehaviour
{
    GridSystem gridSystem;
    private List<Hex> continent = new List<Hex>();
    [Header("Prefabs")]
    [SerializeField]private GameObject housePrefab;
    [SerializeField]private GameObject TreeWeakPrefab;
    [SerializeField]private GameObject towerPrefab;
    [SerializeField]private GameObject FarmPrefab;
    [SerializeField]private GameObject soldierPrefab;
    [SerializeField]private GameObject soldierPrefab2;
    [SerializeField]private GameObject soldierPrefab3;
    [SerializeField]private GameObject soldierPrefab4;
    private void Awake()
    {
        gridSystem = GameObject.Find("GridSystem").GetComponent<GridSystem>();


    }
    private void Update()
    {
    }
    public List<Hex> SpawnLandOfPlayers(int size, List<Player> players)//Devletleri atar gösterir
    {
        List<Hex> grassHexes = gridSystem.hexes.Where(hex => hex._hexType == Hex.hexType.grass).ToList();
        List<Hex> spawnedHouses = new List<Hex>();
        int numberOfHouses = players.Count;
        for (int i = 0; i < numberOfHouses; i++)
        {
            if (grassHexes.Count > 0)
            {

                Hex randomHex;
                do
                {
                    randomHex = grassHexes[UnityEngine.Random.Range(0, grassHexes.Count)];
                }
                while (spawnedHouses.Any(house => GridSystem.FindDistanceBetweenHexes(house, randomHex) < 2 && randomHex.neighbors.Count < 6));
                //Instantiate(housePrefab, randomHex.transform.position, Quaternion.identity);


                grassHexes.RemoveAll(hex => GridSystem.FindDistanceBetweenHexes(hex, randomHex) < 10 && hex != randomHex);
                spawnedHouses.Add(randomHex);
            }
        }

        Stack<Color> colors = new Stack<Color>();// oyuncu renkleri
        colors.Push(Color.red);
        colors.Push(Color.green);
        colors.Push(Color.blue);
        colors.Push(Color.magenta);
        //devletleri ata
        for (int j = 0; j < players.Count; j++)
        {
            Player player = players[j];
            Hex hex = spawnedHouses[j];

            InstantiateHouseOfLands(hex);
            hex.HexObjectType = ObjectType.TownHall;
            List<Hex> land = new List<Hex>();
            land.Add(hex);
            foreach (Hex item in hex.neighbors)
            {
                if (item._hexType == Hex.hexType.grass)
                {
                    land.Add(item);
                }
            }

            int a = 1;
            while (land.Count < 7)
            {

                foreach (Hex item in land[a].neighbors)
                {
                    if (land.Count == 7)
                        continue;
                    if (item._hexType == Hex.hexType.grass && !land.Contains(item))
                    {
                        land.Add(item);
                    }



                }
                a++;
            }


            player.PlayerUpdate(land, colors.Pop());
        }

        return spawnedHouses;
    }
    private void InstantiateHouseOfLands(Hex hex)//AnaBinaları ekler
    {
        GameObject treeWeak = Instantiate(housePrefab, new Vector3(hex.transform.position.x, hex.transform.position.y), Quaternion.identity);
    }
    public List<Hex> travelContinentSpawn(Hex startHex, int count)
    {
        continent.Clear();
        Stack<Hex> stack = new Stack<Hex>();

        stack.Push(startHex);

        Hex.hexType hexType = startHex._hexType;

        while (stack.Count > 0 && continent.Count < count)
        {
            Hex currentHex = stack.Pop();

            if (!currentHex.hasVisited && currentHex._hexType == hexType)
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



    public void SpawnObje(Hex uygunHex,ObjectType s)//genel spawn
    {
        if(s == ObjectType.BuildingDefenceLevel1 || s == ObjectType.BuildingDefenceLevel2)
        {
            SpawnDefenceBuilding(uygunHex, s);
        }
        else if(s == ObjectType.SoldierLevel1|| s == ObjectType.SoldierLevel2 || s == ObjectType.SoldierLevel3 || s == ObjectType.SoldierLevel4)
        {
            SpawnSoldier(uygunHex, s);
        }
        else if(s == ObjectType.BuildingFarm)
        {
            SpawnFarmBuildng(uygunHex);
            uygunHex.UpdateAdvantageOrDisadvantageValue();
        }      
    }
    public void SpawnDefenceBuilding(Hex uygunHex, ObjectType s)
    {
        if(s == ObjectType.BuildingDefenceLevel1)
        {
            BuildTower(uygunHex,1);
          

        }else if(s == ObjectType.BuildingDefenceLevel2)
        {
            BuildTower(uygunHex, 2);
      
        }
    }
    public void SpawnFarmBuildng(Hex uygunHex)
    {
        InstantiateFarmBuilding(uygunHex);
        uygunHex.UpdateAdvantageOrDisadvantageValue();
    }

    public GameObject[] soldierPrefabs;
    public void SpawnSoldier(Hex uygunHex,ObjectType s)// tüm soldierlar için yapılmalı
    {

        if (uygunHex != null && uygunHex.HexObjectType == ObjectType.None||uygunHex.HexObjectType==ObjectType.TreeWeak)
        {
            Debug.Log("spawnSoldierA girdi ife");

            if(uygunHex.HexObjectType== ObjectType.TreeWeak)
            {
                uygunHex.destroyObjectOnHex();
                uygunHex.Owner.PlayerTotalGold += 4;
            }

              
            InstantiateSoldier(uygunHex,s);
          

        }

    }

    public void SpawnTrees()// en başta rastgele ağaçlar atanacak genelde ikişerli üçerli olacak şekilde
    {
        List<Hex> hexes = gridSystem.hexes;
        System.Random rand = new System.Random();//rnd

        int maxTreeCount = 30;
        int treeCounter = 0;// sayaç

        foreach (var hex in hexes)// olası eski ağaç konumlarını temizler
        {
            if (hex.HexObjectType == ObjectType.TreeWeak)
            {
                hex.HexObjectType = ObjectType.None;
            }
        }

        while (treeCounter < maxTreeCount)// ağaçların konumlarını atar treeWeak yapar HexObjectType'ını
        {
            int index = rand.Next(hexes.Count);

            Hex hex = hexes[index];
            int neighborIndex = rand.Next(hex.neighbors.Count);
            int neighbor2Index = rand.Next(hex.neighbors.Count);

            if (hex._hexType == Hex.hexType.grass && hex.HexObjectType == ObjectType.None)
            {
                hex.HexObjectType = ObjectType.TreeWeak;
                hex.UpdateAdvantageOrDisadvantageValue();
                InstantiateTree(hex);
                treeCounter++;
            }

            Hex neighborHex = hex.neighbors[neighborIndex];
            if (neighborHex.HexObjectType == ObjectType.None && neighborHex._hexType == Hex.hexType.grass)
            {
                neighborHex.HexObjectType = ObjectType.TreeWeak;
                neighborHex.UpdateAdvantageOrDisadvantageValue();
                InstantiateTree(neighborHex);
                treeCounter++;
            }
            Hex neighbor2Hex = hex.neighbors[neighbor2Index];
            if (neighbor2Hex.HexObjectType == ObjectType.None && neighbor2Hex._hexType == Hex.hexType.grass && treeCounter % 2 == 0)//rastgele 3.ağaç
            {
                neighbor2Hex.HexObjectType = ObjectType.TreeWeak;
                neighbor2Hex.UpdateAdvantageOrDisadvantageValue();
                InstantiateTree(neighbor2Hex);
                treeCounter++;
            }

        }

    }
    private void InstantiateTree(Hex hex)//ağaçları haritaya ekler
    {
        GameObject treeWeak = Instantiate(TreeWeakPrefab, new Vector3(hex.transform.position.x, hex.transform.position.y), Quaternion.identity);
        hex.ObjectOnHex = treeWeak;
        // ağaçyerleştrimek için;
    }
    private void InstantiateTower(Hex hex)
    {
        GameObject tower = Instantiate(towerPrefab, new Vector3(hex.transform.position.x, hex.transform.position.y), Quaternion.identity);
    }
    private void InstantiateFarmBuilding(Hex hex)
    {
        GameObject farm = Instantiate(FarmPrefab, new Vector3(hex.transform.position.x, hex.transform.position.y), Quaternion.identity);
        hex.HexObjectType = ObjectType.BuildingFarm;
    }
    private void InstantiateSoldier(Hex hex,ObjectType s)// soldier type a göre soldier atar yaratır
    {

        GameObject gameObject = soldierPrefab;
        if(s == ObjectType.SoldierLevel1) { gameObject = soldierPrefab; }      
        else if (s == ObjectType.SoldierLevel2) { gameObject = soldierPrefab2; }
        else if (s == ObjectType.SoldierLevel3) { gameObject = soldierPrefab3; }
        else if (s == ObjectType.SoldierLevel4) { gameObject = soldierPrefab4; }
        
        GameObject soldier = Instantiate(gameObject, new Vector3(hex.transform.position.x, hex.transform.position.y), Quaternion.identity);
        Debug.Log("soldier Yaratıldı");
        Debug.Log("Gelen Hex"+hex.playerName);
        soldier.GetComponent<Soldier>().onHex = hex;
        Debug.Log("Hex atandı");
        soldier.GetComponent<Soldier>().owner = hex.Owner;
        soldier.GetComponent<Soldier>().playerName = hex.playerName;
        hex.ObjectOnHex = soldier;
        hex.Owner.soldiers.Add(soldier.GetComponent<Soldier>());
        hex.HexObjectType = s;
        //hex.HexEmpty = true;
        hex.HexObjectType = s;

    }

    public void TreesSpread()// ağaç yayılması tüm haritayı kapsamaz bir yerde durur.
    {
        // olan ağaçlar yayılma eğlimi gösterir SpawnTrees()'den sonra çağrılmalı ağaçlar 10 tane türeyecek
        List<Hex> hexes = gridSystem.hexes;
        System.Random rand = new System.Random();//
        int spreadCounter = 0;
        int spreadLimit = 15;// yayılma tetiklenince artacak ağaç sayısı

        List<Hex> hexesWithTree = hexes.Where(h => h.HexObjectType == ObjectType.TreeWeak).ToList();



        int hexesWithOutTreeCount = hexes.Where(h => h.HexObjectType == ObjectType.None && h._hexType == Hex.hexType.grass).ToList().Count();
        bool canSpread = true;

   
        if (hexesWithOutTreeCount < 40)
        {
            spreadLimit = 0;
            canSpread = false;
        }



        while (spreadCounter < spreadLimit && canSpread)
        {
            int index = rand.Next(hexesWithTree.Count);// türeme rastgele bi yerden başlicak
            bool spread = false;// yayılma için bayrak
            Hex hex = hexesWithTree[index];
            for (int i = 0; i < hex.neighbors.Count && !spread; i++)
            {
                Hex neighbor = hex.neighbors[i];
                if (neighbor.HexObjectType == ObjectType.None && neighbor._hexType == Hex.hexType.grass)
                {
                    neighbor.HexObjectType = ObjectType.TreeWeak;
                    neighbor.UpdateAdvantageOrDisadvantageValue();
                    InstantiateTree(neighbor);
                    spread = true;
                    spreadCounter++;
                }
            }

        }


    }
    public void BuildTower(Hex hex, int x)//Towerımız burada oluşturuluyor.
    {
        List<Hex> neighboringHexes = hex.neighbors;
        if (x == 1)
        {
            hex.HexObjectType = ObjectType.BuildingDefenceLevel1;
            InstantiateTower(hex);
        }
        else if (x == 2)
        {
            hex.HexObjectType = ObjectType.BuildingDefenceLevel2;
            InstantiateTower(hex);
        }
    }
}

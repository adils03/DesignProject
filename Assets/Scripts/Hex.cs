using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ObjectType// hex üzerindeki nesneler 
{
    None,   // Hiçbir nesne yok
    SoldierLevel1,
    SoldierLevel2,
    SoldierLevel3,
    SoldierLevel4,
    Tree,
    TreeWeak,
    BuildingFarm,
    BuildingDefenceLevel1,
    BuildingDefenceLevel2,
}

public class Hex : MonoBehaviour
{
    public hexType _hexType;
    public int q, r, s;
    public List<Hex> neighbors = new List<Hex>();
    public List<Hex> areaForStep = new List<Hex>();
    public bool hasVisited = false;
    public int Income = 3;// hex başına gelir default 3 
    public Player Owner;// kimin bu hex ,null ise kimsenin
    public ObjectType HexObjectType { get; set; } = ObjectType.None;// hex üzerindeki nesne asker , bina , ağaç
    public bool HexEmpty { get; set; }
    public bool SetProtected { get; set; }
    public enum hexType
    {
        grass,
        water
    }
    private void OnMouseDown()
    {
        travelContinentByStep(this, 2);
    }

    public void UpdateAdvantageOrDisadvantageValue()// ağaçlardan biri mevcut ise dezavantaj var 
    {
        if (this.HexObjectType == ObjectType.Tree || this.HexObjectType == ObjectType.TreeWeak)
            Income = 0;
        else if (this.HexObjectType == ObjectType.BuildingFarm)
            Income = 10;// bu da farmbinası başına verilen değer      
    }



    void travelContinentByStep(Hex startHex, int step) //Hex'in bulunduğu konumdan istenilen adım büyüklüğü kadar alanı areaForStep'e eşitler
    {
        step++;

        Queue<Hex> queue = new Queue<Hex>();

        queue.Enqueue(startHex);

        hexType __hexType = startHex._hexType;

        while (queue.Count > 0 && step > 0)
        {
            int size = queue.Count;

            for (int i = 0; i < size; i++)
            {
                Hex currentHex = queue.Dequeue();

                if (!currentHex.hasVisited && currentHex._hexType == __hexType)
                {
                    areaForStep.Add(currentHex);
                    currentHex.hasVisited = true;
                    currentHex.transform.localScale = new Vector3(1, 1, 1);

                    foreach (Hex neighbor in currentHex.neighbors)
                    {
                        if (!neighbor.hasVisited)
                        {
                            queue.Enqueue(neighbor);
                        }
                    }
                }
            }

            step--;
        }
        foreach (Hex hex in areaForStep)
        {
            hex.hasVisited = false;
        }
    }



}
using System;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public int CurrentGold { get; set; } = 0;

    private int totalIncome;

    private List<Hex> OwnedHexagons;

    void Awake()
    {
        OwnedHexagons = new List<Hex>(); // İlk değer ataması
       
    }

    void Start()
    {
        CalculateIncome();
        CalculateSalaries();
        UpdateGold();
    }

   
    void CalculateIncome()
    {     

        foreach (Hex hexagon in OwnedHexagons)
        {
            totalIncome += hexagon.Income;
        }
    }

    void CalculateSalaries()// maaş bilgilerini totalIncomedan yaniii gelirden düştük
    {
        foreach (Hex hex in OwnedHexagons)
        {
            int salarySoldier = 0;

            if(hex.HexObjectType == ObjectType.SoldierLevel1) { salarySoldier = 5; }
            else if (hex.HexObjectType == ObjectType.SoldierLevel2) { salarySoldier = 15; }
            else if (hex.HexObjectType == ObjectType.SoldierLevel3) { salarySoldier = 30; }
            else if (hex.HexObjectType == ObjectType.SoldierLevel4) { salarySoldier = 50; }
            else if (hex.HexObjectType == ObjectType.BuildingDefenceLevel1) { salarySoldier = 20; }
            else if (hex.HexObjectType == ObjectType.BuildingDefenceLevel2) { salarySoldier = 30; }

            totalIncome -= salarySoldier;
        }
       
    }

    void UpdateGold()
    {
        CurrentGold += totalIncome;
        Debug.Log("Total Gold: " + CurrentGold);
    }
    public void UpdateOwnedHexagons(List<Hex> newHexagons)
    {
        OwnedHexagons = newHexagons;
        CalculateIncome();
        CalculateSalaries();
        UpdateGold();
    }
   
    public void HexOwnershipChanged(Hex changedHex)
    {   
        // spesifik hex sahibi değişti bu metot çağıralabilir
        List<Hex> updatedHexagons = GetUpdatedHexagons(changedHex);

        // UpdateOwnedHexagons metodunu çağırarak ekonomiyi güncelle
        UpdateOwnedHexagons(updatedHexagons);
    }    
    private List<Hex> GetUpdatedHexagons(Hex changedHex)
    {
        // Hex'in sahipliği değiştiğinde çağrılacak yardımcı bir metod
        List<Hex> updatedHexagons = new List<Hex>(OwnedHexagons);

        // Değişen hex'i listeden kaldır veya ekleyebilirsiniz
        if (updatedHexagons.Contains(changedHex))
        {
            updatedHexagons.Remove(changedHex);
        }
        else
        {
            updatedHexagons.Add(changedHex);
        }

        return updatedHexagons;
    }

}





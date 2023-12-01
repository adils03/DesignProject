using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoldierLevel
{
    Level1,
    Level2,
    Level3,
    Level4
}

public class EconomyManager : MonoBehaviour
{
    public int TotalHexagons { get; set; } = 4;
    public int BaseIncomePerHexagon { get; set; } = 3;

    private int totalIncome;
    private int totalSalaries;
    private int currentGold = 0;
    private List<Hex> ownedHexagons = new List<Hex>();

    // Asker maaşlarını depolamak için sözlük
    private Dictionary<SoldierLevel, int> soldierSalaries = new Dictionary<SoldierLevel, int>();

    void Start()
    {
        CalculateIncome();
        CalculateSalaries();
        ApplyDisadvantages();
        UpdateGold();
    }

    void InitializeSoldierSalaries()
    {
        soldierSalaries[SoldierLevel.Level1] = 4;
        soldierSalaries[SoldierLevel.Level2] = 10;
        soldierSalaries[SoldierLevel.Level3] = 30;
        soldierSalaries[SoldierLevel.Level4] = 60;
    }

    void CalculateIncome()
    {
        totalIncome = TotalHexagons * BaseIncomePerHexagon;

        foreach (Hex hexagon in ownedHexagons)
        {
            totalIncome = hexagon.GetAdvantageValue(totalIncome);
        }
    }

    void CalculateSalaries()
    {
        int totalSalaries = 0;

        foreach (SoldierLevel level in Enum.GetValues(typeof(SoldierLevel)))
        {
            int soldierCount = GetSoldierCount(level);
            int salary = soldierSalaries[level];
            totalSalaries += soldierCount * salary;
        }

        this.totalSalaries = totalSalaries;
    }

    void ApplyDisadvantages()
    {
        foreach (Hex hexagon in ownedHexagons)
        {
            if (hexagon.HasDisadvantage())
            {
                totalIncome -= hexagon.GetDisadvantageValue();
            }
        }
    }

    void UpdateGold()
    {
        currentGold += totalIncome - totalSalaries;
        Debug.Log("Total Gold: " + currentGold);
    }

    int GetSoldierCount(SoldierLevel level)
    {
        // Gerçek oyun mantığına göre asker sayısını al
        // Örneğin: return 10;
        return 0;
    }
}





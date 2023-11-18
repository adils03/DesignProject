using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public int totalHexagons = 4; // sahip olunan toprak sayısı 
    
    public int baseIncomePerHexagon = 3;// el başına toprak geliri
   
    public int soldier1Salary = 4; // level 1 asker maaşı
    public int soldier2Salary = 10; 
    public int soldier3Salary = 30; 
    public int soldier4Salary = 60; 

    private int totalIncome;// toplam gelir
    private int totalSalaries;// toplam maaş gideri
    private int currentGold = 0;// kasada bulunan altın miktarı
    private List<Hex> ownedHexagons;

    void Start()
    {
        CalculateIncome();
        CalculateSalaries();
        ApplyDisadvantages(); // Dezavantajları uygula
        UpdateGold();
    }

   void CalculateIncome()//  gelir hesabı
   {
    totalIncome = totalHexagons * baseIncomePerHexagon;

    // Toprakların avantajlarına göre geliri güncelle
    foreach (Hex hexagon in ownedHexagons)
    {
        totalIncome = hexagon.GetAdvantageValue(totalIncome);
        // toprak eğer avantaja sahip ise üzerine farm binası eklenmişse
        // bu GetAdvantageValue metot gelire + 5 gold ekliyor tümsahip olunan topraklar için 
        // bu durum kontrol ediliyor
    }
    }

    void CalculateSalaries()// maaş gider hesabı
    {

    int level1Soldiers = GetLevel1SoldierCount() * soldier1Salary;
    int level2Soldiers = GetLevel2SoldierCount() * soldier2Salary;
    int level3Soldiers = GetLevel3SoldierCount() * soldier3Salary;
    int level4Soldiers = GetLevel4SoldierCount() * soldier4Salary;

    totalSalaries = level1Soldiers + level2Soldiers + level3Soldiers + level4Soldiers;
    }

    void ApplyDisadvantages()// DEzavantajları uygula
    {
        // Her toprak için dezavantajı kontrol et
        // burda hexagon sınıfını tanımlayıp HasDisadvantage metodunu düzenlemeliyiz
        // kısaca hexagon üzerinde dezavantaj durumu var mı(üerinde ağaç varsa gelir getirmez)
        foreach (Hex hexagon in ownedHexagons)
        {
            if (hexagon.HasDisadvantage()) // Eğer toprakta dezavantaj varsa
            {
                totalIncome -= hexagon.GetDisadvantageValue(); // Dezavantaj değerini gelirden çıkar
            }
        }
    }

    void updateAllWithTurn()// tur başına oyuncuların toprak sayısı ve ekonomisi ve asker sayısı güncellenir
    {
        //totalHexagons = ownedHexagons.Count();
          
    }

    void UpdateGold()// el başı kasadaki altını güncelle
    {
        currentGold += totalIncome - totalSalaries;
        Debug.Log("Total Gold: " + currentGold);
    }

    int GetLevel1SoldierCount()
    {
        // 1. seviye asker sayısını buradan alabilirsiniz.
        // burda asker sayısı ve maaşları çarparak maliyet hesabı yapılır
        // Örneğin: return 10;
        return 0;
    }
    int GetLevel2SoldierCount()
    {
        // 2. seviye asker sayısını buradan alabilirsiniz.
         // burda asker sayısı ve maaşları çarparak maliyet hesabı yapılır
        // Örneğin: return 10;
        return 0;
    }
    int GetLevel3SoldierCount()
    {
        // 3. seviye asker sayısını buradan alabilirsiniz.
         // burda asker sayısı ve maaşları çarparak maliyet hesabı yapılır
        // Örneğin: return 10;
        return 0;
    }
    int GetLevel4SoldierCount()
    {
        // 4. seviye asker sayısını buradan alabilirsiniz.
         // burda asker sayısı ve maaşları çarparak maliyet hesabı yapılır
        // Örneğin: return 10;
        return 0;
    }
    int GetTotalSoldierCount()
    {
    int totalcount = GetLevel1SoldierCount() + GetLevel2SoldierCount() + GetLevel3SoldierCount() + GetLevel4SoldierCount();
    return totalcount;
    }

}





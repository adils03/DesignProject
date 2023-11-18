using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    public int x;
    public int y;
    public List<Hex> neighbors;//Komşularını tutar
    private void OnMouseDown()
    {
        Debug.Log(x + ":" + y + "tiklandi");
    }
    public bool HasAdvantage { get; set; } // Toprağın avantajı var mı?
    public bool HasFarmBuilding { get; set; } // Çiftlik binası var mı?
    public bool HasSoldier { get; set; } // Asker var mı?
    
    // Dezavantajları kontrol etmek için bir metot
    public bool HasDisadvantage()
    {
        // Burada toprağın üzerindeki nesnelere göre dezavantajları kontrol edebilirsiniz
        // Örneğin, ağaç varsa dezavantaj vardır.
        return false; // Eğer dezavantaj yoksa false döndürün
    }

    // Asker veya bina olup olmadığını kontrol etmek için bir metot
    public bool HasSoldierOrBuilding()
    {
        return HasSoldier || HasFarmBuilding;
    }

    // Toprağın avantajını kontrol etmek için bir metot
    public int GetAdvantageValue(int baseIncome)
    {
        if (HasAdvantage)
        {
            return baseIncome + 5; // Eğer avantaj varsa geliri artır
        }
        else
        {
            return baseIncome; // Avantaj yoksa normal geliri döndür
        }
    }

    // Dezavantaj durumunda gelir düşüşünü belirlemek için bir metot
    public int GetDisadvantageValue()
    {
        // Eğer dezavantaj varsa, ne kadar gelir kaybı olacağını burada belirleyebilirsiniz.
        return 0; // Örneğin, ağaç olduğunda gelir kaybı sıfırdır.
    }



}

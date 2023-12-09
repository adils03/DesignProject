using System;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string playerName;
    public int PlayerTotalGold = 0;// player altını burdan da görüyoruz kaynak eManager
    public  List<Hex> ownedHexes = new List<Hex>(); //sahip olduğu hexler
    public EconomyManager economyManager;
    public static Color playerColor;
    
    public Player(String name)// bu ctor diğerleri patlamasın diye geçici duruyor daha karar verilmedi
    {
        playerName = name;
        economyManager = new EconomyManager();
    }
    public Player(String name, List<Hex> hexes,Color color)// dışardan gelen ilk hexlerle ve oyun boyunca elde edilecek olan hexler için inşa edilecek olan kısım
    {
        playerColor=color;
        ownedHexes = hexes;
        // Hex'lerin sahibini bu oyuncu olarak ayarla
        foreach (var hex in ownedHexes)
        {
            hex.playerName=name;
            hex.Owner = this;
            hex.gameObject.GetComponent<SpriteRenderer>().color = playerColor;
        }


        playerName = name;
        economyManager = new EconomyManager();

        //economyManager.UpdateOwnedHexagons(ownedHexes);
        PlayerTotalGold = economyManager.CurrentGold;// kasadaki altını  burdan ilkez aldık 


    }
    public void StartTurn()
    {
        economyManager.UpdateOwnedHexagons(ownedHexes);// sahip olduğumuz hexleri ekonomi managerde güncelledik
        PlayerTotalGold = economyManager.CurrentGold;// burdan da tur başına güncelledik kasadaki altını
        Debug.Log(playerName + " oyuncunun sirasi basladi. ve Altını : " + PlayerTotalGold);
    }
    public void ChangeHexOwnership(Hex changedHex)
    {
        // EconomyManager'ın sahip olduğu hex listesini güncelle spesifik bir hex'e göre
        economyManager.HexOwnershipChanged(changedHex);
    }
    public void AddHex(Hex newHex)
    {
        if (newHex != null && !ownedHexes.Contains(newHex))
        {
            ownedHexes.Add(newHex);
            newHex.Owner = this;
        }
    }
    public void AddHex(List<Hex> newHexes)// çok mu çok tatlı
    {
        foreach (var newHex in newHexes)
        {
            AddHex(newHex);
        }
    }

}

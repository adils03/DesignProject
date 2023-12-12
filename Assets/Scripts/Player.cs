using System;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string playerName;
    public int PlayerTotalGold = 10;// player altını burdan da görüyoruz kaynak eManager
    public List<Hex> ownedHexes = new List<Hex>(); //sahip olduğu hexler
    public List<Soldier> soldiers = new List<Soldier>();
    public EconomyManager economyManager;
    public  Color playerColor;
    
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
        UpdateTotalGold();// kasadaki altını  burdan ilkez aldık 
    }
    public void PlayerUpdate(List<Hex> hexes, Color color)
    {
        playerColor = color;
        ownedHexes = hexes;
        // Hex'lerin sahibini bu oyuncu olarak ayarla
        foreach (var hex in ownedHexes)
        {
           
            hex.Owner = this;
            hex.playerName = playerName;
            hex.gameObject.GetComponent<SpriteRenderer>().color = playerColor;
        }
        economyManager = new EconomyManager();

        //economyManager.UpdateOwnedHexagons(ownedHexes);
        UpdateTotalGold();// kasadaki altını  burdan ilkez aldık 
    }
    void UpdateTotalGold()
    {
        PlayerTotalGold += economyManager.totalIncome;
        economyManager.totalIncome = 0;
    }
    public void StartTurn()
    {
        economyManager.UpdateOwnedHexagons(ownedHexes);// sahip olduğumuz hexleri ekonomi managerde güncelledik
        UpdateTotalGold();// burdan da tur başına güncelledik kasadaki altını
        Debug.Log(playerName + " oyuncunun sirasi bitti ve Altını : " + PlayerTotalGold);
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
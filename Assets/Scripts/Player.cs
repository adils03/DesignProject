using System;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string playerName;
    public int PlayerTotalGold = 50;// player altını burdan da görüyoruz kaynak eManager
    public List<Hex> ownedHexes = new List<Hex>(); //sahip olduğu hexler
    public List<Soldier> soldiers = new List<Soldier>();
    public EconomyManager economyManager = new EconomyManager();
    public  Color playerColor;
    
    public Player(String name)// bu ctor diğerleri patlamasın diye geçici duruyor daha karar verilmedi
    {
        playerName = name;

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
        economyManager.UpdateOwnedHexagons(ownedHexes);
     
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
        economyManager.UpdateOwnedHexagons(ownedHexes);
      
    }
    void EconomyDeath()// ekonomi çöktü askerler imha edildi
    {
        Debug.Log("Ekonomi çöktü");
       
        foreach (var hex in ownedHexes)
        {
            ObjectType s = hex.HexObjectType;
            if(s == ObjectType.SoldierLevel1|| s == ObjectType.SoldierLevel2 || s == ObjectType.SoldierLevel3 || s == ObjectType.SoldierLevel4)
            {
                hex.HexObjectType = ObjectType.None;
                //hex.ObjectOnHex = null; // bundan emin değilim sahibi baksun (ben ibo)

                hex.destroyObjectOnHex();
                //Burdurda birde askerlerin yerine mezar gelmesi mantıklı olur 1 tur için 
            }
        }
        this.soldiers.Clear();
        
    }


    void UpdateTotalGold()
    {
        PlayerTotalGold += economyManager.totalIncome;
        if(PlayerTotalGold<0)
        {
            EconomyDeath();
            PlayerTotalGold = 0;
            economyManager.UpdateOwnedHexagons(ownedHexes);// income tekrar hesaplansın
        }         
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
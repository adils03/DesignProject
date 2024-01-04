using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ibraynAgent
{
    public class GameLogic_
    {
        private GameStateH currentState; // Mevcut oyun durumu
        private QTable_ qTable; // Q-tablosu
        private PriceDictionary priceDictionary;
        Player Player;// Fiyatları içeren sözlük
        Shop Shop;
     

        public GameLogic_(Player player)
        {
            Shop = GameObject.Find("Shop").GetComponent<Shop>();
            currentState = InitializeGameState(); // Oyun durumunu başlat
            qTable = new QTable_(); // Q-tablosunu başlat
            priceDictionary = new PriceDictionary(); // Fiyatları içeren sözlüğü başlat
            Player = player;       
        }

        private GameStateH InitializeGameState()
        {
            // Oyun durumunu başlatma lojikleri buraya eklenir
            // Örneğin, başlangıçta oyuncuların sahip olduğu kareleri ve altınları ayarlama



            return new GameStateH(Player.PlayerTotalGold,Player.economyManager.totalIncome,Player.ownedHexes);
        }

        public void PerformAction(Action action)
        {
            // Belirli bir aksiyonu gerçekleştirme lojikleri buraya eklenir
            // Örneğin, asker üretme, savunma kulesi inşa etme, vb.
            // Yapılan aksiyonun sonucunda yeni bir oyun durumu oluşturulur ve güncellenir
            


            

            GameStateH newGameState = UpdateGameState(currentState, action);
            currentState = newGameState;
        }

        private GameStateH UpdateGameState(GameStateH currentState, Action action)
        {
            // Yapılan aksiyona göre yeni bir oyun durumu oluşturulur ve döndürülür
            // Bu metot, asker üretme, savunma kulesi inşa etme, vb. gibi aksiyonlara özel durum güncelleme lojikleri içerir
            // Örneğin, asker üretildiyse, asker sayısı artar; savunma kulesi inşa edildiyse, savunma gücü artar
            // Bu kısımları, oyununuzun özel mantığına göre uyarlamalısınız
            return new GameStateH(Player.PlayerTotalGold, Player.economyManager.totalIncome, Player.ownedHexes);
        }

        public double GetReward(GameStateH currentState, Action action, GameStateH newGameState)
        {
            // Belirli bir aksiyonun gerçekleştirilmesi sonucu elde edilen ödülü hesapla
            // Örneğin, yeni durumun altın miktarını, asker sayısını kontrol ederek bir ödül hesapla
            // Bu kısımları, oyununuzun özel mantığına göre uyarlamalısınız
            return 0/* Ödülü Hesapla */;
        }

    }
}

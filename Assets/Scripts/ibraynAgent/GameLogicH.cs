using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ibraynAgent
{
    public class GameLogicH
    {
        private GameStateH currentState; // Mevcut oyun durumu
        private QTable qTable; // Q-tablosu
        private PriceDictionary priceDictionary; // Fiyatları içeren sözlük
     

        public GameLogicH(Player player)
        {
            currentState = InitializeGameState(player); // Oyun durumunu başlat
            qTable = new QTable(); // Q-tablosunu başlat
            priceDictionary = new PriceDictionary(); // Fiyatları içeren sözlüğü başlat
        
        }

        private GameStateH InitializeGameState(Player player)
        {
            // Oyun durumunu başlatma lojikleri buraya eklenir
            // Örneğin, başlangıçta oyuncuların sahip olduğu kareleri ve altınları ayarlama



            return new GameStateH(10,20,player.ownedHexes);
        }

        public void PerformAction(ActionH action)
        {
            // Belirli bir aksiyonu gerçekleştirme lojikleri buraya eklenir
            // Örneğin, asker üretme, savunma kulesi inşa etme, vb.
            // Yapılan aksiyonun sonucunda yeni bir oyun durumu oluşturulur ve güncellenir
            GameStateH newGameState = UpdateGameState(currentState, action);
            currentState = newGameState;
        }

        private GameStateH UpdateGameState(GameStateH currentState, ActionH action)
        {
            // Yapılan aksiyona göre yeni bir oyun durumu oluşturulur ve döndürülür
            // Bu metot, asker üretme, savunma kulesi inşa etme, vb. gibi aksiyonlara özel durum güncelleme lojikleri içerir
            // Örneğin, asker üretildiyse, asker sayısı artar; savunma kulesi inşa edildiyse, savunma gücü artar
            // Bu kısımları, oyununuzun özel mantığına göre uyarlamalısınız
            return new GameStateH(/* ... */);
        }

        public double GetReward(GameStateH currentState, ActionH action, GameStateH newGameState)
        {
            // Belirli bir aksiyonun gerçekleştirilmesi sonucu elde edilen ödülü hesapla
            // Örneğin, yeni durumun altın miktarını, asker sayısını kontrol ederek bir ödül hesapla
            // Bu kısımları, oyununuzun özel mantığına göre uyarlamalısınız
            return /* Ödülü Hesapla */;
        }

        public GameStateH GetCurrentState()
        {
            return currentState;
        }

        public QTable GetQTable()
        {
            return qTable;
        }

        public PriceDictionary GetPriceDictionary()
        {
            return priceDictionary;
        }

        public SoldierPriceDictionary GetSoldierPriceDictionary()
        {
            return soldierPriceDictionary;
        }


    }
}

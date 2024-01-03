using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ibraynAgent
{
    public class PriceDictionary
    {
        private Dictionary<GameItem, int> Prices;

        public PriceDictionary()
        {
            Prices = new Dictionary<GameItem, int>();
            InitializePrices();
        }

        private void InitializePrices()
        {
            Prices.Add(GameItem.SoldierLevel1, 10);
            Prices.Add(GameItem.SoldierLevel2, 20);
            Prices.Add(GameItem.SoldierLevel3, 30);
            Prices.Add(GameItem.SoldierLevel4, 40);
            Prices.Add(GameItem.TowerLevel1,15);
            Prices.Add(GameItem.TowerLevel2,35);
            // Diğer seviyeleri ekleyin
        }

        public int GetItemPrice(GameItem item)
        {
            if (Prices.TryGetValue(item, out int price))
            {
                return price;
            }
            
            throw new ArgumentException("Belirtilen item bulunamadı.");
        }
    }

    public enum GameItem
    {
        SoldierLevel1,
        SoldierLevel2,
        SoldierLevel3,
        SoldierLevel4,
        TowerLevel1,
        TowerLevel2,
        Farm
       
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ibraynAgent
{
    public class PriceDictionary
    {
        private Dictionary<ObjectType, int> Prices;

        public PriceDictionary()
        {
            Prices = new Dictionary<ObjectType, int>();
            InitializePrices();
        }

        private void InitializePrices()
        {
            Prices.Add(ObjectType.SoldierLevel1, 10);
            Prices.Add(ObjectType.SoldierLevel2, 20);
            Prices.Add(ObjectType.SoldierLevel3, 30);
            Prices.Add(ObjectType.SoldierLevel4, 40);
            Prices.Add(ObjectType.BuildingDefenceLevel1,15);
            Prices.Add(ObjectType.BuildingDefenceLevel2,35);
            // Diğer seviyeleri ekleyin
        }

        public int GetItemPrice(ObjectType item)
        {
            if (Prices.TryGetValue(item, out int price))
            {
                return price;
            }
            
            throw new ArgumentException("Belirtilen item bulunamadı.");
        }
    }

    //public enum ObjectType// hex üzerindeki nesneler (isimler için kullanıldı açılmaz)
    //{
    //    None,   // Hiçbir nesne yok
    //    Grave,
    //    Tree,
    //    TreeWeak,
    //    BuildingFarm,
    //    SoldierLevel1,
    //    SoldierLevel2,
    //    BuildingDefenceLevel1,
    //    SoldierLevel3,
    //    BuildingDefenceLevel2,
    //    TownHall,
    //    SoldierLevel4

    //}

}

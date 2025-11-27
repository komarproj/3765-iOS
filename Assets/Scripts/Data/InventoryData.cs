using System;
using System.Collections.Generic;
using UniRx;

namespace Data
{
    [Serializable]
    public class InventoryData
    {
        public IntReactiveProperty Balance = new (0);
        public IntReactiveProperty HealItems = new (3);
        public IntReactiveProperty FreezeItems = new (3);
        public IntReactiveProperty TimeItems = new (3);
        
        public List<int> Eggs = new(){1,2,3};
        
        public List<ChickenData> Chickens = new()
        {
            new ChickenData()
            {
                Id = 0,
                Level = 0
            },
            new ChickenData()
            {
                Id = 4,
                Level = 0
            }
        };
    }
}
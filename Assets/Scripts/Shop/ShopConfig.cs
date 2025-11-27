using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Shop
{
    [CreateAssetMenu(menuName = "Configs/Shop Config")]
    public class ShopConfig : BaseConfig
    {
        public List<ShopItem> ShopItems;
    }

    [Serializable]
    public class ShopItem
    {
        public Sprite EggSprite;
        public int Price;
    }
}
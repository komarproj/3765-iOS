using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Gameplay
{
    [CreateAssetMenu(menuName = "Configs/ChickensConfig")]
    public class ChickensConfig : BaseConfig
    {
        public List<ChickenVisualData> Chickens;
    }

    [Serializable]
    public class ChickenVisualData
    {
        public Sprite EggSprite;
        public Sprite ChickenSprite;
    }
}
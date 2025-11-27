using System;
using UniRx;

namespace Data
{
    [Serializable]
    public class ChickenData
    {
        public int Id;
        public int Level = 0;
        public IntReactiveProperty CoinsGenerated = new(0);
        public float EggChance = 0.25f;
    }
}
using System;
using UniRx;

namespace Data
{
    [Serializable]
    public class IncubatorUpgradeData
    {
        public IntReactiveProperty SizeUpgrade = new(0);
        public IntReactiveProperty SpeedUpgrade = new(0);
        public IntReactiveProperty TimeUpgrade = new(0);
    }
}
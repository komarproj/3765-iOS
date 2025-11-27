using System;
using UniRx;

namespace Data
{
    [Serializable]
    public class EnergyData
    {
        public IntReactiveProperty Energy = new(Constants.MaxEnergy);
    }
}
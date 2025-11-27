using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class UpgradesData
    {
        public List<IncubatorUpgradeData> Upgrades = new()
        {
            new(),
            new(),
            new(),
            new(),
        };
    }
}
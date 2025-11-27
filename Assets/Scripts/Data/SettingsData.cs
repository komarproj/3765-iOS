using System;
using UniRx;

namespace DefaultNamespace.Data
{
    [Serializable]
    public class SettingsData
    {
        public BoolReactiveProperty IsSoundOn = new BoolReactiveProperty(true);
        public BoolReactiveProperty IsMusicOn = new BoolReactiveProperty(true);
    }
}
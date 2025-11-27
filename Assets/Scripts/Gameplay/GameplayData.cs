using System.Collections.Generic;
using UniRx;

namespace DefaultNamespace.Gameplay
{
    public class GameplayData
    {
        public List<IncubatedEggData> IncubatedEggs = new();

        public FloatReactiveProperty LevelTime = new(30);

        public bool DroppedToFailure = false;
    }
}
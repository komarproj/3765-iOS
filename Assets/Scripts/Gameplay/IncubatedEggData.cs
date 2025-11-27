using Data;
using UniRx;

namespace DefaultNamespace.Gameplay
{
    public class IncubatedEggData
    {
        public int EggId;
        public int IncubatorId;
        public FloatReactiveProperty Health = new(Constants.StartingEggHealth);
        public BoolReactiveProperty IsActive = new (true);
    }
}
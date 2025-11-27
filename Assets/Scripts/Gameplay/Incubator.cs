using UnityEngine;

namespace DefaultNamespace.Gameplay
{
    public class Incubator : MonoBehaviour
    {
        [SerializeField] private IncubatorPreparation _incubatorPreparation;
        [SerializeField] private IncubatorGame _incubatorGame;
        
        public IncubatorPreparation Preparation => _incubatorPreparation;
        public IncubatorGame Game => _incubatorGame;

        private IncubatedEggData _egg;
        public IncubatedEggData Egg => _egg;

        public void SetEgg(IncubatedEggData egg)
        {
            _egg = egg;
            _incubatorGame.SetEgg(_egg);
        }

        public void StartPreparation()
        {
            _incubatorPreparation.Enable(true);
            _incubatorGame.Enable(false);
        }
        
        public void StartGame()
        {
            _incubatorPreparation.Enable(false);
            _incubatorGame.Enable(true);
        }
    }
}
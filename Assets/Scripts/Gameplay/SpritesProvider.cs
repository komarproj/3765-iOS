using AssetsProvider;
using UnityEngine;

namespace DefaultNamespace.Gameplay
{
    public class SpritesProvider
    {
        private readonly ConfigsProvider _configs;

        public SpritesProvider(ConfigsProvider configs)
        {
            _configs = configs;
        }

        public Sprite GetChickenSprite(int chickenId) => GetConfig().Chickens[chickenId].ChickenSprite;
        public Sprite GetEggSprite(int chickenId) => GetConfig().Chickens[chickenId].EggSprite;
        
        private ChickensConfig GetConfig() => _configs.Get<ChickensConfig>();
    }
}
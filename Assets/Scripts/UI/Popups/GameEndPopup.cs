using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using DefaultNamespace.Gameplay;
using DefaultNamespace.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Popups
{
    public class GameEndPopup : BasePopup
    {
        [SerializeField] private IncubationResult _prefab;
        [SerializeField] private RectTransform _resultParent;
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;
        
        [SerializeField] private TextMeshProUGUI _rewardText;
        [SerializeField] private TextMeshProUGUI _expText;
        
        private GameplayData _gameplayData;
        private SpritesProvider _spritesProvider;
        
        private List<IncubationResult> _results = new();

        private int _index = 0;
        
        [Inject]
        private void Construct(GameplayData gameplayData, SpritesProvider spritesProvider)
        {
            _gameplayData = gameplayData;
            _spritesProvider = spritesProvider;
        }

        private void Start()
        {
            foreach (var egg in _gameplayData.IncubatedEggs) 
                CreateResult(egg);
            
            _results[0].Show();

            if (_results.Count == 1)
            {
                _leftButton.gameObject.SetActive(false);
                _rightButton.gameObject.SetActive(false);
            }
            else
            {
                _leftButton.onClick.AddListener(() => ChangePage(-1));
                _rightButton.onClick.AddListener(() => ChangePage(+1));
            }

            int successChickens = _gameplayData.IncubatedEggs.Count(x => x.IsActive.Value);
            _rewardText.text = "+" + (successChickens * Constants.RewardPerIncubation);
            _expText.text = "+" + (successChickens * Constants.ExperiencePerIncubation);
        }

        private void CreateResult(IncubatedEggData eggData)
        {
            bool failed = !eggData.IsActive.Value;
            int id = eggData.EggId;

            Sprite sprite = failed ? _spritesProvider.GetEggSprite(id) : _spritesProvider.GetChickenSprite(id);
            string text = failed ? "incubation failed..." : "new chicken added to your collection!";
            
            var instance = Instantiate(_prefab, _resultParent);
            
            instance.SetData(text, sprite, failed);
            
            _results.Add(instance);
        }
        
        private void ChangePage(int increment)
        {
            _results[_index].Hide();
            
            _index += increment;
            
            if(_index >= _results.Count)
                _index = 0;
            if(_index < 0)
                _index = _results.Count - 1;
            
            _results[_index].Show();
        }
    }
}
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.Gameplay
{
    public class GameStage : MonoBehaviour
    {
        [SerializeField] private List<Incubator> _incubators;
        [SerializeField] private CanvasGroup _canvasGroup;

        public void StartGame()
        {
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.DOFade(1, 1).SetLink(gameObject);

            foreach (var incubator in _incubators) 
                incubator.StartGame();
        }
    }
}
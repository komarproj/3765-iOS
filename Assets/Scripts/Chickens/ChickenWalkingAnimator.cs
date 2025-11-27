using System.Collections.Generic;
using DefaultNamespace.Gameplay;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Gameplay.Services
{
    public class ChickenWalkingAnimator : IInitializable
    {
        private const float _xMin = 0.33f;
        private const float _xMax = 0.67f;
        private const float _yMin = 0.3f;
        private const float _yMax = 0.6f;

        private const float _moveSpeed = 5;
        private const float _waitMin = 2;
        private const float _waitMax = 5;

        private const float _scaleMin = 0.3f;
        private const float _scaleMax = 1f;
        
        private Dictionary<ChickenMono, Sequence> _animations = new ();

        private Camera _camera;
        
        private Vector3 _minWorld;
        private Vector3 _maxWorld;
        
        public void Initialize()
        {
            _camera = Camera.main;
            
            var screenWidth = Screen.width;
            var screenHeight = Screen.height;
            
            _minWorld = _camera.ScreenToWorldPoint(new Vector3(_xMin * screenWidth, _yMin * screenHeight,
                Mathf.Abs(_camera.transform.position.z)));
            _maxWorld = _camera.ScreenToWorldPoint(new Vector3(_xMax * screenWidth, _yMax * screenHeight,
                Mathf.Abs(_camera.transform.position.z)));
        }
        
        public void AddChicken(ChickenMono chicken)
        {
            var pos = new Vector3(
                Random.Range(_minWorld.x, _maxWorld.x),
                Random.Range(_minWorld.y, _maxWorld.y),
                chicken.transform.position.z
            );
            
            AnimatePet(chicken, pos);
        }

        public void AddEggRandom(EggView eggView)
        {
            var pos = new Vector3(
                Random.Range(_minWorld.x, _maxWorld.x),
                Random.Range(_minWorld.y, _maxWorld.y),
                eggView.transform.position.z
            );
            
            SpawnEgg(eggView, pos);
        }

        public void SpawnEgg(EggView eggView, Vector3 pos)
        {
            eggView.transform.position = pos;
            eggView.transform.localScale = Vector3.zero;
            
            eggView.transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutCirc).SetLink(eggView.gameObject);
        }
        
        public void StopAnimation(ChickenMono chicken) => _animations[chicken]?.Kill();

        public void AnimatePet(ChickenMono chicken, Vector3 pos)
        {
            chicken.transform.position = pos;

            void AppendNextMove()
            {
                if (!chicken)
                    return;

                var randomTarget = new Vector3(
                    Random.Range(_minWorld.x, _maxWorld.x),
                    Random.Range(_minWorld.y, _maxWorld.y),
                    chicken.transform.position.z
                );

                var distance = Vector2.Distance(chicken.transform.position, randomTarget);
                var moveTime = distance / _moveSpeed;
                var waitTime = Random.Range(_waitMin, _waitMax);

                var sequence = DOTween.Sequence();

                sequence.Append(
                    chicken.transform.DOMove(randomTarget, moveTime)
                        .SetEase(Ease.Linear)
                        .OnUpdate(() => UpdateScale(chicken, _minWorld, _maxWorld))
                );

                sequence.AppendInterval(waitTime);
                sequence.AppendCallback(AppendNextMove);
                sequence.SetLink(chicken.gameObject);
                
                _animations[chicken] = sequence;
            }

            UpdateScale(chicken, _minWorld, _maxWorld);
            AppendNextMove();
        }

        private void UpdateScale(ChickenMono chicken, Vector3 minWorld, Vector3 maxWorld)
        {
            if (!chicken) 
                return;

            var t = Mathf.InverseLerp(maxWorld.y, minWorld.y, chicken.transform.position.y);
            var scale = Mathf.Lerp(_scaleMin, _scaleMax, t);
            chicken.transform.localScale = new Vector3(scale, scale, 1f);
        }
    }
}
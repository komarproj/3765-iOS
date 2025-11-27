using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Spinner
{
    public class SpinnerService
    {
        private float _angleStep = 360f / 8;

        private int _minAddSpins = 7;
        private int _maxAddSpins = 10;

        private float _spinTime = 5;

        public async UniTask Spin(RectTransform transform, int targetIndex)
        {
            float angleTarget = targetIndex * _angleStep;

            int additionalSpins = Random.Range(_minAddSpins, _maxAddSpins);
            
            angleTarget += additionalSpins * 360;
            
            Vector3 target = new Vector3(0, 0, angleTarget);
            
            transform.DORotate(target, _spinTime, RotateMode.FastBeyond360).SetEase(Ease.OutSine).SetLink(transform.gameObject);
            await UniTask.WaitForSeconds(_spinTime);
        }
    }
}
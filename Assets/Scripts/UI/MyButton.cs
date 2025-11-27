using System;
using DG.Tweening;
using Game.UserData;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    [RequireComponent(typeof(Button))]
    public class MyButton : MonoBehaviour
    {
        [SerializeField] private bool _debug = false;
        
        private void Awake() => GetComponent<Button>().onClick.AddListener(ProcessClick);

        private void ProcessClick()
        {
            if(_debug)
                Debug.Log("??");
            
            AudioManager.Instance.PlayButtonSound();
            transform.DOKill();
            transform.DOPunchScale(Vector3.one * 0.1f, 0.25f).SetLink(gameObject);
        }
    }
}
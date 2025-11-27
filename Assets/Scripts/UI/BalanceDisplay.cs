using Game.UserData;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.UI
{
    public class BalanceDisplay : MonoBehaviour 
    {
        [SerializeField] private TextMeshProUGUI _balanceText;

        [Inject]
        private void Construct(SaveSystem saveSystem)
        {
            var balance = saveSystem.Data.InventoryData.Balance;
            balance.Subscribe((value) =>
            {
                _balanceText.text = value.ToString();
            }).AddTo(gameObject);
        }
    }
}
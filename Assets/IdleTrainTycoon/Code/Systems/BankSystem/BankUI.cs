using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using VContainer;

namespace IdleTrainTycoon.Code.Systems.BankSystem
{
    public class BankUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI goldLabel;
        [Inject] [SerializeField] private Bank bank;
        private readonly CompositeDisposable _disposables = new();

        private void Start()
        {
            bank.Gold
                .Subscribe(Refresh)
                .AddTo(_disposables);
        }

        private void Refresh(int amount)
        {
            goldLabel.transform.DOScale(1.5f, 0.2f).SetLoops(2, LoopType.Yoyo).SetUpdate(true);
            goldLabel.text = amount.ToString();
        }

        private void OnDestroy() => _disposables.Dispose();
    }
}
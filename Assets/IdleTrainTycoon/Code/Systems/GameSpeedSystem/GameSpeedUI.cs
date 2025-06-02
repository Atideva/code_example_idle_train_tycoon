using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace IdleTrainTycoon.Code.Systems.GameSpeedSystem
{
    public class GameSpeedUI : MonoBehaviour
    {
        [SerializeField] private Slider speedSlider;
        [SerializeField] private TextMeshProUGUI speedLabel;

        public IReadOnlyReactiveProperty<float> Speed => _speed;
        private readonly ReactiveProperty<float> _speed = new(1f);

        private void Start()
        {
            speedSlider.value = _speed.Value;

            speedSlider
                .OnValueChangedAsObservable()
                .Subscribe(Refresh)
                .AddTo(this);

            Refresh(_speed.Value);
        }

        private void Refresh(float spd)
        {
            _speed.Value = spd;

            if (speedLabel)
                speedLabel.text = $"x{spd:F1}";
        }

    }
}
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace IdleTrainTycoon.Code.Systems.GameSpeedSystem
{
    public class GameSpeed : MonoBehaviour
    {
        [SerializeField] [ReadOnly] private GameSpeedUI gameSpeedUI;

        public void Init(GameSpeedUI ui)
        {
            gameSpeedUI = ui;
        }

        private void Start()
        {
            gameSpeedUI.Speed
                .Subscribe(speed =>
                {
                    Time.timeScale = speed;
                    Debug.Log($"Game speed set to: {speed}");
                })
                .AddTo(this);
        }
    }
}
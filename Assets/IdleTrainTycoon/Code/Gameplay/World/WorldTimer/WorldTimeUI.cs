using TMPro;
using UniRx;
using UnityEngine;

namespace IdleTrainTycoon.Code.Gameplay.World.WorldTimer
{
    public class WorldTimeUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textDisplay;
        [SerializeField] private WorldTime worldTime;

        private void Start()
        {
            if (!worldTime)
            {
                Debug.LogError("Timer reference not set in TimerUI!");
                return;
            }

            worldTime.SecondsElapsed
                .Subscribe(sec => { textDisplay.text = $"{sec} sec"; })
                .AddTo(this); 
        }
    }
}
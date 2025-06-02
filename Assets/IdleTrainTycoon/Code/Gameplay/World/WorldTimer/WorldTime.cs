using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace IdleTrainTycoon.Code.Gameplay.World.WorldTimer
{
    public class WorldTime : MonoBehaviour
    {
        public ReactiveProperty<int> SecondsElapsed { get; private set; } = new(0);

        private void Start()
        {
            RunTimerAsync().Forget();
        }

        private async UniTaskVoid RunTimerAsync()
        {
            while (true)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: this.GetCancellationTokenOnDestroy());
                SecondsElapsed.Value++;
            }
        }
    }
}
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using IdleTrainTycoon.Code.Gameplay.Supplies;
using IdleTrainTycoon.Code.Gameplay.Trains;
using UnityEngine;

namespace IdleTrainTycoon.Code.Gameplay.World.MapsWaypoints
{
    public class Mine : Waypoint
    {
        [Header("Production")]
        [SerializeField] private SupplySO supply;
        [SerializeField, Min(0.1f)] private float harvestDurationMult = 1f;
        [SerializeField] [Min(1)] private int limit = 10;
        [SerializeField] private SpriteRenderer supplyIcon;
        private readonly List<(float start, float end)> _busy = new();

        public float HarvestDurationMult => harvestDurationMult;

        private bool _isLoadingTrain = false;
        private readonly Queue<Train> _trainQueue = new();


        private void Awake()
        {
            supplyIcon.sprite = supply.Icon;
        }


        public void StartLoadTrain(Train train)
        {
            if (!train)
            {
                Debug.LogWarning("StartLoadTrain called with null train!");
                return;
            }

            _trainQueue.Enqueue(train);
            TryLoadNextTrain().Forget();
        }

        private async UniTask TryLoadNextTrain()
        {
            while (true)
            {
                if (_isLoadingTrain) return;
                if (_trainQueue.Count == 0) return;

                _isLoadingTrain = true;

                var train = _trainQueue.Dequeue();
                var dur = train.HarvestDurationSec * HarvestDurationMult;
                var durMiliSec = Mathf.RoundToInt(dur * 1000);

                Debug.Log((name + " is loading " + supply.Name + " to " + train.name + ". Time: " + dur.ToString("F0") + "sec."));
                await UniTask.Delay(durMiliSec);
                train.Loaded(supply);
                Debug.Log((name + " has been load " + supply.Name + " to " + train.name + "."));

                _isLoadingTrain = false;
            }
        }

    }
}
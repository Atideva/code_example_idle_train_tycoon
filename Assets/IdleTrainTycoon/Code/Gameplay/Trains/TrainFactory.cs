using System;
using System.Collections.Generic;
using IdleTrainTycoon.Code.Gameplay.Trains.Data;
using IdleTrainTycoon.Code.Systems.PoolSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace IdleTrainTycoon.Code.Gameplay.Trains
{
    public class TrainFactory : MonoBehaviour
    {
        [SerializeField] private Train prefab;
        [SerializeField] private Transform container;
        private Pool<Train> _pool;
        [Space(20)]
        [SerializeField] [ReadOnly] private List<Train> trainsOnDuty = new();

        public IReadOnlyList<Train> TrainsOnDuty => trainsOnDuty;
        public event Action<Train> OnSpawn = delegate { };

        private TrainDatabaseSO _database;

        public void Init(TrainDatabaseSO database)
        {
            _database = database;
            _pool = new Pool<Train>(prefab, container);
            ClearScene();
        }

        private void ClearScene()
        {
            var trains = container.GetComponentsInChildren<Train>();
            foreach (var train in trains)
                _pool.Return(train);
        }

        public Train SpawnRandom()
        {
            var type = _database.GetRandom();

            var train = _pool.Get();
            train.transform.SetParent(container);
            train.Init(type);
            train.Unload();
            train.name = "Train #" + (trainsOnDuty.Count + 1);

            trainsOnDuty.Add(train);

            OnSpawn(train);
            return train;
        }

    }
}
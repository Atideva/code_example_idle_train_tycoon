using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace IdleTrainTycoon.Code.Gameplay.Trains.Data
{
    [CreateAssetMenu(fileName = "New TrainDatabase", menuName = "Game/Train Database SO")]
    public class TrainDatabaseSO : ScriptableObject
    {
        [InlineEditor]
        [SerializeField]
        private List<TrainSO> trains = new();

        public TrainSO GetRandom()
        {
            var type = trains[Random.Range(0, trains.Count)];
            return type;
        }
    }
}
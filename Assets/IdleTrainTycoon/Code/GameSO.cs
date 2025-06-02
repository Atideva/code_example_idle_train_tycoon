using IdleTrainTycoon.Code.Gameplay.Supplies;
using IdleTrainTycoon.Code.Gameplay.Trains.Data;
using UnityEngine;

namespace IdleTrainTycoon.Code
{
    [CreateAssetMenu(fileName = "Game Config", menuName = "Game/Game Config SO")]
    public class GameSO : ScriptableObject
    {
        [SerializeField] private bool spawnTrainsAtRandomStations;
        [Space(20)]
        [SerializeField] private TrainDatabaseSO trainDatabase;
        [SerializeField] private SuppliesDatabaseSO suppliesDatabase;
        public bool SpawnTrainsAtRandomStations => spawnTrainsAtRandomStations;
        public SuppliesDatabaseSO SuppliesDatabase => suppliesDatabase;
        public TrainDatabaseSO TrainDatabase => trainDatabase;
    }
}
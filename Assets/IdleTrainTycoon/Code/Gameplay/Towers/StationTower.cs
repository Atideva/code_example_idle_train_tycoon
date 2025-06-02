using IdleTrainTycoon.Code.Gameplay.Supplies;
using IdleTrainTycoon.Code.Gameplay.World.Maps;
using UnityEngine;

namespace IdleTrainTycoon.Code.Gameplay.Towers
{
    public class StationTower : MonoBehaviour
    {
        private Map _map;

        public void Init(Map newMap, SuppliesDatabaseSO database)
        {
            _map = newMap;
            InitStationMarket(database);
        }

        private void InitStationMarket(SuppliesDatabaseSO database)
        {
            foreach (var station in _map.Stations)
            {
                station.SetDesiredSupplies(database);
            }
        }
    }
}
using System.Collections.Generic;
using IdleTrainTycoon.Code.Gameplay.Supplies;
using IdleTrainTycoon.Code.Gameplay.World.MapsWaypoints;
using IdleTrainTycoon.Code.Systems.PoolSystem;
using UnityEngine;

namespace IdleTrainTycoon.Code.Gameplay.UI
{
    public class StationsPanelUI : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private StationUI stationPrefabUI;
        [SerializeField] private SupplyUI supplyPrefabUI;

        private Pool<StationUI> _stationPool;
        private Pool<SupplyUI> _supplyPool;

        public void Init()
        {
            _stationPool = new Pool<StationUI>(stationPrefabUI, container);
            _supplyPool = new Pool<SupplyUI>(supplyPrefabUI, container);
            ReturnPanelsToPool();
        }

        private void ReturnPanelsToPool()
        {
            var panels = container.GetComponentsInChildren<StationUI>();
            foreach (var panel in panels)
            {
                _stationPool.Return(panel);
            }
        }

        public void Create(IReadOnlyList<Station> station)
        {
            foreach (var s in station)
            {
                CreateDisplay(s);
            }
        }

        private void CreateDisplay(Station station)
        {
            var panel = _stationPool.Get();
            panel.transform.SetParent(container);
            panel.Init(station, _supplyPool);
        }
    }
}
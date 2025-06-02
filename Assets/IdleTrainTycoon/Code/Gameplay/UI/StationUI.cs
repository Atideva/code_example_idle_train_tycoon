using System.Collections.Generic;
using IdleTrainTycoon.Code.Gameplay.Supplies;
using IdleTrainTycoon.Code.Gameplay.World.MapsWaypoints;
using IdleTrainTycoon.Code.Systems.PoolSystem;
using TMPro;
using UnityEngine;

namespace IdleTrainTycoon.Code.Gameplay.UI
{
    public class StationUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameTxt;
        [SerializeField] private Transform container;
        private readonly Dictionary<SupplySO, SupplyUI> _dict = new();
        private Pool<SupplyUI> _pool;
        private Station _station;

        public void Init(Station station, Pool<SupplyUI> pool)
        {
            nameTxt.text = station.Name;
            _station = station;
            _station.Stock.OnStockChanged += Refresh;
            _pool = pool;
            ReturnToPoolAll();
            Refresh();
        }

        private void ReturnToPoolAll()
        {
            var all = container.GetComponentsInChildren<SupplyUI>();
            foreach (var stationUI in all)
            {
                _pool.Return(stationUI);
            }
        }

        private void Refresh()
        {
            Debug.Log(_station.Name + " display: total supplies " + _station.Stock.List.Count);
            foreach (var stock in _station.Stock.List)
            {
                var supply = stock.type;
                var price = _station.GetOfferedPrice(supply);
                var panel = GetSupplyUI(supply);
                panel.Set(stock, price);
            }
        }

        private SupplyUI GetSupplyUI(SupplySO supply)
        {
            if (_dict.TryGetValue(supply, out var panel)) return panel;

            var newPanel = _pool.Get();
            newPanel.transform.SetParent(container);
            _dict.Add(supply, newPanel);
            return newPanel;
        }

    }
}
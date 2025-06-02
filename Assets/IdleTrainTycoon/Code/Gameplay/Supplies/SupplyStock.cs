using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IdleTrainTycoon.Code.Gameplay.Supplies
{
    public class SupplyStock : MonoBehaviour
    {

        [SerializeField] private List<SupplyData> list = new();
        public IReadOnlyList<SupplyData> List => list;

        public event Action OnStockChanged = delegate { };

        public void Add(SupplySO supply, int amount)
        {
            var data = Find(supply);
            data.amount.Value += amount;
            OnStockChanged();
        }

        public int GetAmount(SupplySO so) => Find(so).amount.Value;

        private SupplyData Find(SupplySO supply)
        {
            if (list.Any(s => s.type == supply))
                return list.First(s => s.type == supply);

            var data = new SupplyData(supply, 0);
            list.Add(data);
            return data;
        }
    }
}
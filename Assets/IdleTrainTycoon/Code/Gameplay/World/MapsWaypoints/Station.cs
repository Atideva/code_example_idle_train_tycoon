using System.Collections.Generic;
using IdleTrainTycoon.Code.Gameplay.Supplies;
using UnityEngine;
using Random = UnityEngine.Random;

namespace IdleTrainTycoon.Code.Gameplay.World.MapsWaypoints
{
    public class Station : Waypoint
    {
        [SerializeField] private SupplyStock supplyStock;
        [SerializeField] [Min(0)] private float incomeMultiplier = 1f;
        [SerializeField] private List<SupplyTradeData> market = new();
        public SupplyStock Stock => supplyStock;

        public void SetDesiredSupplies(SuppliesDatabaseSO database)
        {
            foreach (var supply in database.Supplies)
            {
                var newTrade = new SupplyTradeData(supply, 0, 0);
                market.Add(newTrade);
                supplyStock.Add(supply, 0);
            }

            RefreshDesiredPrices();
        }

        public int GetOfferedPrice(SupplySO supply)
        {
            var find = market.Find(s => s.type == supply);
            return find.offeredPrice;
        }

        private void RefreshDesiredPrices()
        {
            foreach (var supply in market)
            {
                supply.originalPrice = supply.type.Price;
                supply.offeredPrice = CalcOfferedPrice(supply.type, supply.originalPrice);
            }
        }

        private int CalcOfferedPrice(SupplySO supply, int originalPrice)
        {
            var inStock = supplyStock.GetAmount(supply);
            var mult = CalcOfferMult(inStock);
            var random = Random.Range(0.5f, 1.5f);
            return (int) (originalPrice * mult * random);
        }

        private int CalcOfferMult(int amount)
        {
            return amount switch
            {
                <= 1 => 10,
                <= 3 => 7,
                <= 5 => 5,
                <= 7 => 3,
                <= 10 => 2,
                _ => 1
            };
        }


        public void Delivery(SupplySO supply)
        {
            supplyStock.Add(supply, 1);
            RefreshDesiredPrices();
        }

    }
}
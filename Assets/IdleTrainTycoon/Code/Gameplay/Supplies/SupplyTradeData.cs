namespace IdleTrainTycoon.Code.Gameplay.Supplies
{
    [System.Serializable]
    public class SupplyTradeData
    {
        public SupplySO type;
        public int originalPrice;
        public int offeredPrice;

        public SupplyTradeData(SupplySO type, int originalPrice, int offeredPrice)
        {
            this.type = type;
            this.originalPrice = originalPrice;
            this.offeredPrice = offeredPrice;
        }
    }
}
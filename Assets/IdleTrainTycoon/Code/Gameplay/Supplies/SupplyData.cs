using UniRx;

namespace IdleTrainTycoon.Code.Gameplay.Supplies
{
    [System.Serializable]
    public class SupplyData
    {
        public SupplySO type;
        public ReactiveProperty<int> amount;

        public SupplyData(SupplySO type, int amount)
        {
            this.type = type;
            this.amount = new ReactiveProperty<int>(amount);
        }

        public void AddAmount(int a)
        {
            amount.Value += a;
        }
    }
}
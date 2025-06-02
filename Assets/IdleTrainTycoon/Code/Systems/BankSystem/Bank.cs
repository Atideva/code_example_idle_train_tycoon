using UniRx;
using UnityEngine;

namespace IdleTrainTycoon.Code.Systems.BankSystem
{
    public class Bank : MonoBehaviour
    {
        public ReactiveProperty<int> Gold { get; private set; } = new(0);

        public void AddGold(int amount)
        {
            Gold.Value += amount;
        }

    }
}

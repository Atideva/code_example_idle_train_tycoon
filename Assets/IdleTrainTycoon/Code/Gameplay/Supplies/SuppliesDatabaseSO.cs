using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace IdleTrainTycoon.Code.Gameplay.Supplies
{
    [CreateAssetMenu(fileName = "New Supplies Database", menuName = "Game/Supplies Database SO")]
    public class SuppliesDatabaseSO : ScriptableObject
    {
        [InlineEditor]
        [SerializeField] private List<SupplySO> supplies = new();
        public IReadOnlyList<SupplySO> Supplies => supplies;
    }
}
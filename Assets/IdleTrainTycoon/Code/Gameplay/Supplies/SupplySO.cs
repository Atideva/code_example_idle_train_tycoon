using Sirenix.OdinInspector;
using UnityEngine;

namespace IdleTrainTycoon.Code.Gameplay.Supplies
{
    [CreateAssetMenu(fileName = "NewSupply", menuName = "Game/Supply SO")]
    public class SupplySO : ScriptableObject
    {
        [HorizontalGroup("Labels", Width = 50)] [SerializeField] [HideLabel] [PreviewField(50)]
        private Sprite icon;
        [HorizontalGroup("Labels")] [SerializeField]  [LabelWidth(40)]
        private string _name;
        [HorizontalGroup("Labels")] [SerializeField]  [LabelWidth(40)] 
        private int price;
        public Sprite Icon => icon;
        public int Price => price;
        public string Name => _name;
    }
}
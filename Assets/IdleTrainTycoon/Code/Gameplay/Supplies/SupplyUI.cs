using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IdleTrainTycoon.Code.Gameplay.Supplies
{
    public class SupplyUI : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI txt;
        [SerializeField] private TextMeshProUGUI priceTxt;

        public void Set(SupplyData supply, int offerPrice)
        {
            icon.sprite = supply.type.Icon;
            txt.text = supply.amount.ToString();
            priceTxt.text = offerPrice.ToString();
        }
    }
}

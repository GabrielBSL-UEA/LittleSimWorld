using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Main.UI
{
    //Class to manage the shop button more easily
    public class ShopButton : MonoBehaviour
    {
        [SerializeField] private Button buttonComponent;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private TextMeshProUGUI nameText;

        public Image Icon()
        {
            return icon;
        }
        public TextMeshProUGUI Price()
        {
            return priceText;
        }
        public TextMeshProUGUI Name()
        {
            return nameText;
        }
        public Button Button()
        {
            return buttonComponent;
        }
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.UI
{
    //Class to manage the clothing button more easily
    public class ClothingButton : MonoBehaviour
    {
        [SerializeField] private Button buttonComponent;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI nameText;

        public Image Icon()
        {
            return icon;
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

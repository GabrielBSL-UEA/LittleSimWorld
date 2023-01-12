using Main;
using UnityEngine;

namespace Custom
{
    [CreateAssetMenu(fileName = "OpenShop", menuName = "Event/CustomEvent/OpenShop")]
    public class OpenSpecialPanel : CustomEvent
    {
        public override void TriggerEvent()
        {
            base.TriggerEvent();

            GameManager.Instance.OpenSpecialPanel();
        }
    }
}
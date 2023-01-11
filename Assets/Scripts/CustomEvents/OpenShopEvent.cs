using UnityEngine;

namespace Custom
{
    [CreateAssetMenu(fileName = "OpenShop", menuName = "Event/CustomEvent/OpenShop")]
    public class OpenShopEvent : CustomEvent
    {
        public override void TriggerEvent()
        {
            base.TriggerEvent();

            Debug.Log("teste");
        }
    }
}
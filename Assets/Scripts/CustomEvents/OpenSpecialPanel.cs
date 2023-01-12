using Main;
using UnityEngine;

namespace Custom
{
    //Custom event to open the special panel of the scene
    [CreateAssetMenu(fileName = "OpenShop", menuName = "Event/CustomEvent/OpenShop")]
    public class OpenSpecialPanel : CustomEvent
    {
        public override void TriggerEvent()
        {
            base.TriggerEvent();

            GameManager.Instance.SetPlayerControl(false);
            GameManager.Instance.OpenSpecialPanel();
        }
    }
}
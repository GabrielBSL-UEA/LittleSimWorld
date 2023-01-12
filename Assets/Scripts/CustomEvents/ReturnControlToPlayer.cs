using Main;
using UnityEngine;

namespace Custom
{
    [CreateAssetMenu(fileName = "ReturnControl", menuName = "Event/CustomEvent/ReturnControlToPlayer")]
    public class ReturnControlToPlayer : CustomEvent
    {
        public override void TriggerEvent()
        {
            base.TriggerEvent();

            GameManager.Instance.SetPlayerControl(true);
        }
    }
}

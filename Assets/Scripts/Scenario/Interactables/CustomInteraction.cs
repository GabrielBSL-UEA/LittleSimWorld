using Custom;
using UnityEngine;

namespace Scenario
{
    //Class to trigger a custom event when interacted
    public class CustomInteraction : Interactable
    {
        [SerializeField] private CustomEvent customEvent;
        [SerializeField] private bool oneTimeInteraction;

        private bool _triggered;

        public override void Interact(PlayerInteraction interactor)
        {
            if(_triggered && oneTimeInteraction)
            {
                return;
            }
            _triggered = true;

            base.Interact(interactor);
            customEvent.TriggerEvent();
        }
    }
}

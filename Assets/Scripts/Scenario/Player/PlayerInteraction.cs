using UnityEngine;

namespace Scenario
{
    /*
     *  Interaction component of the player character
     * 
     *  detects interactable game objects around and start an interaction when receives the input
     */
    public class PlayerInteraction : MonoBehaviour
    {
        //Communication with the main component
        public PlayerController Controller { get; set; }

        [SerializeField] private float interactionRadius;
        [SerializeField] private LayerMask interactableLayerMask;

        private Interactable currentInteractable;

        private void Awake()
        {
            currentInteractable = GetClosestInteractable();
        }

        private void FixedUpdate()
        {
            SetUpInteractable();
        }

        //Function that saves the closest interactable object, activating it's outline
        //In case of a new interactable being found, the old closest interactable has it's outline disabled
        private void SetUpInteractable()
        {
            var newClosestInteractable = GetClosestInteractable();

            if (newClosestInteractable != currentInteractable)
            {
                currentInteractable?.SetOutline(false);
                newClosestInteractable?.SetOutline(true);

                currentInteractable = newClosestInteractable;
            }
        }

        //Function that searches for the closest interactable object to return;
        private Interactable GetClosestInteractable()
        {
            Interactable closest = null;
            var interactableHit = Physics2D.OverlapCircleAll(transform.position, interactionRadius, interactableLayerMask);
            var closestDistance = float.MaxValue;

            for (int i = 0; i < interactableHit.Length; i++)
            {
                //Instead of comparing the interaction distances, it's used the squared distance instead, avoiding calculate square roots every fixed frame
                var distanceSquared = (transform.position - interactableHit[i].transform.position).sqrMagnitude;
                if (distanceSquared > closestDistance)
                {
                    continue;
                }

                interactableHit[i].TryGetComponent(out closest);
                closestDistance = distanceSquared;
            }

            return closest;
        }

        //Trigger the interaction of the closest object
        public void TriggerInteraction()
        {
            currentInteractable?.Interact();
        }
    }
}
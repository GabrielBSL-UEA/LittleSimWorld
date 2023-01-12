using Main;
using Main.Inventory;
using UnityEngine;

namespace Scenario
{
    //Class that holds an item for the player to collect in a interaction
    public class Collectable : Interactable
    {
        //Item to hold
        [SerializeField] private Item itemToCollect;

        //To avoid double interaction
        private bool _collected;

        protected override void Awake()
        {
            base.Awake();

            UpdateCollectableSprite();
        }

        //Update the object sprite and the outline object with the holded item
        private void UpdateCollectableSprite()
        {
            TryGetComponent(out SpriteRenderer mainRenderer);

            mainRenderer.sprite = itemToCollect.Icon();

            for (int i = 0; i < 4; i++)
            {
                outlines[i].sprite = itemToCollect.Icon();
            }
        }

        public override void Interact(PlayerInteraction interactor)
        {
            base.Interact(interactor);

            Collect();
        }

        //Load the item to the player inventory and then destroy itself
        public void Collect(bool destroyAfter = true)
        {
            if (_collected)
            {
                return;
            }
            _collected = true;

            GameManager.Instance.RegisterNewItemToPlayerInventory(itemToCollect);

            if (destroyAfter)
            {
                Destroy(gameObject);
            }
        }
    }
}
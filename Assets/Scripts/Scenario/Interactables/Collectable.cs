using Main;
using Main.Inventory;
using UnityEngine;
using Audio;
using System.Collections;

namespace Scenario
{
    //Class that holds an item for the player to collect in a interaction
    public class Collectable : Interactable
    {
        //Item to hold
        [SerializeField] private Item itemToCollect;

        [Header("Effects")]
        [SerializeField] private Transform lightTransform;
        [SerializeField] private float fullRotationTime;
        [SerializeField] private GameObject particleOnCollect;

        //To avoid double interaction
        private bool _collected;

        protected override void Awake()
        {
            base.Awake();

            UpdateCollectableSprite();
            StartCoroutine(RotateLight());
        }

        private IEnumerator RotateLight()
        {
            while (true)
            {
                lightTransform.eulerAngles += new Vector3(0, 0, 360) * (Time.deltaTime / fullRotationTime);
                yield return null;
            }
        }

        //Update the object sprite and the outline object with the holded item
        private void UpdateCollectableSprite()
        {
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
            AudioManager.Instance.PlaySFX("s_GetItem");
            StopAllCoroutines();

            Instantiate(particleOnCollect, transform.position, Quaternion.identity);

            if (destroyAfter)
            {
                Destroy(gameObject);
            }
        }
    }
}
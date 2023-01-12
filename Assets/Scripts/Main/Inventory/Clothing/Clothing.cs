using Scenario;
using UnityEngine;

namespace Main.Inventory
{
    public class Clothing : Item
    {
        [SerializeField] private Sprite idleDownSprite;
        [SerializeField] private RuntimeAnimatorController clothingAnimatorController;

        public virtual void SetPlayerClothing(PlayerAnimation playerAnimation)
        {

        }

        public RuntimeAnimatorController AnimController()
        {
            return clothingAnimatorController;
        }
        //Idle_down sprite, to use in clothing preview
        public Sprite IdleDownSprite()
        {
            return idleDownSprite;
        }
    }
}
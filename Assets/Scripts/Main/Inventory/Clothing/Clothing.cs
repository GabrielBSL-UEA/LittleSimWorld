using Scenario;
using UnityEditor.Animations;
using UnityEngine;

namespace Main.Inventory
{
    public class Clothing : Item
    {
        [SerializeField] private Sprite idleDownSprite;
        [SerializeField] private AnimatorController clothingAnimatorController;

        public virtual void SetPlayerClothing(PlayerAnimation playerAnimation)
        {

        }

        public AnimatorController AnimController()
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
using Scenario;
using UnityEditor.Animations;
using UnityEngine;

namespace Main.Inventory
{
    public class Clothing : Item
    {
        [SerializeField] private AnimatorController clothingAnimatorController;

        public virtual void SetPlayerClothing(PlayerAnimation playerAnimation)
        {

        }

        public AnimatorController AnimController()
        {
            return clothingAnimatorController;
        }
    }
}
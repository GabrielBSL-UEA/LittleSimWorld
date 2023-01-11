using UnityEditor.Animations;
using UnityEngine;

namespace Main.Inventory
{
    public class Clothing : Item
    {
        [SerializeField] private AnimatorController clothingAnimatorController;

        public AnimatorController AnimController()
        {
            return clothingAnimatorController;
        }
    }
}
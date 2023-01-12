using Scenario;
using UnityEngine;

namespace Main.Inventory
{
    [CreateAssetMenu(fileName = "MessageSegment", menuName = "Clothing/Hair")]
    public class Hair : Clothing
    {
        //Set the player's hair
        public override void SetPlayerClothing(PlayerAnimation playerAnimation)
        {
            base.SetPlayerClothing(playerAnimation);

            playerAnimation.HairAnimator().runtimeAnimatorController = AnimController();
        }
    }
}
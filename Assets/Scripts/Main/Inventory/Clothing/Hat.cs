using Scenario;
using UnityEngine;

namespace Main.Inventory
{
    [CreateAssetMenu(fileName = "MessageSegment", menuName = "Clothing/Hat")]
    public class Hat : Clothing
    {
        //Set the player's hat
        public override void SetPlayerClothing(PlayerAnimation playerAnimation)
        {
            base.SetPlayerClothing(playerAnimation);

            playerAnimation.HatAnimator().runtimeAnimatorController = AnimController();
            GameManager.Instance.Inventory().CurrentHat = this;
        }
    }
}
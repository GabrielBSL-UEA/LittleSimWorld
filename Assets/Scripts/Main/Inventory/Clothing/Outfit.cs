using Scenario;
using UnityEngine;

namespace Main.Inventory
{
    [CreateAssetMenu(fileName = "MessageSegment", menuName = "Clothing/Outfit")]
    public class Outfit : Clothing
    {
        //Set the player's outfit
        public override void SetPlayerClothing(PlayerAnimation playerAnimation)
        {
            base.SetPlayerClothing(playerAnimation);

            playerAnimation.OutfitAnimator().runtimeAnimatorController = AnimController();
            GameManager.Instance.Inventory().CurrentOutfit = this;
        }
    }
}

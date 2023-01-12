using UnityEngine;

namespace Main.Inventory
{
    //Class of scriptable objects that can be saved in the player inventory
    [CreateAssetMenu(fileName = "Item", menuName = "Item/Common Item")]
    public class Item : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private bool canStack = false;
        [SerializeField] private int purchasePrice;
        [SerializeField] private int salePrice;

        [SerializeField] private Sprite itemSprite;

        public Sprite Icon()
        {
            return itemSprite;
        }

        public string Name()
        {
            return itemName;
        }
        public bool Stackable()
        {
            return canStack;
        }
        public int PurchasePrice()
        {
            return purchasePrice;
        }
        public float SalePrice()
        {
            return salePrice;
        }
    }
}
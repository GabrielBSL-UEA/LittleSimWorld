using UnityEngine;

namespace Main.Inventory
{
    public class Item : ScriptableObject
    {
        [SerializeField] private Sprite itemSprite;

        public Sprite Icon()
        {
            return itemSprite;
        }
    }
}
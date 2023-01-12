using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Main.Inventory
{
    /*
     *  Main class to manage the player's inventory
     *
     */
    public class InventoryManager : MonoBehaviour
    {
        //List of the player's clothes and items
        private List<Clothing> _clothes;
        private List<Item> _playerInventory = new List<Item>();

        private void Awake()
        {
            LoadClothesIntoMemory();
        }
        //Gets all the clothes from the game files
        private void LoadClothesIntoMemory()
        {
            _clothes = Resources.FindObjectsOfTypeAll<Clothing>().ToList();
        }

        //Add an item to the player's inventory
        public void AddItemToPlayerInventory(Item newItem)
        {
            _playerInventory.Add(newItem);
        }
        //Remove an item from the player's inventory
        public void RemoveItemFromPlayerInventory(Item toRemove)
        {
            _playerInventory.Remove(toRemove);
        }

        //Return all the player's clothes
        public List<Clothing> GetAvailableClothes()
        {
            var playerClothes = _playerInventory.FindAll(c => c.GetType().IsSubclassOf(typeof(Clothing))).ConvertAll(c => (Clothing)c);
            return _clothes.Except(playerClothes).ToList();
        }

        //Return all the player's items, can be called to return only items with selling prices
        public List<Item> Inventory(bool withSellingPrices = false)
        {
            if (withSellingPrices)
            {
                var sellingItems = _playerInventory.FindAll(i => i.SalePrice() > 0);
                return sellingItems;
            }

            return _playerInventory;
        }
    }
}

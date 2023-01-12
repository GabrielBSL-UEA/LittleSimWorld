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

        [Header("Default")]
        [SerializeField] private Outfit defaultOutfit;
        [SerializeField] private Hair defaultHair;
        [SerializeField] private Hat defaultHat;

        public Hair CurrentHair { get; set; }
        public Hat CurrentHat { get; set; }
        public Outfit CurrentOutfit { get; set; }

        private void Awake()
        {
            LoadClothesIntoMemory();

            AddItemToPlayerInventory(defaultHat);
            AddItemToPlayerInventory(defaultHair);
            AddItemToPlayerInventory(defaultOutfit);

            CurrentHair = defaultHair;
            CurrentHat = defaultHat;
            CurrentOutfit = defaultOutfit;
        }
        //Gets all the clothes from the game files
        private void LoadClothesIntoMemory()
        {
            _clothes = Resources.LoadAll<Clothing>("ScriptableObjects/Clothes").ToList();
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
        public List<Clothing> GetAvailableClothes(bool toBuy = true)
        {
            var playerClothes = _playerInventory.FindAll(c => c.GetType().IsSubclassOf(typeof(Clothing))).ConvertAll(c => (Clothing)c);

            if (toBuy)
            {
                return _clothes.Except(playerClothes).ToList();
            }
            return playerClothes;
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

        public void DressClothingPiece(Clothing clothing)
        {
            GameManager.Instance.Player().Dress(clothing);
        }

        public void DressPlayer()
        {
            GameManager.Instance.Player().Dress(CurrentHat);
            GameManager.Instance.Player().Dress(CurrentHair);
            GameManager.Instance.Player().Dress(CurrentOutfit);
        }
    }
}

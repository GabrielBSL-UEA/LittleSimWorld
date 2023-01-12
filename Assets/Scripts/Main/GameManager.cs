using Main.Inventory;
using Main.UI;
using Scenario;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main
{
    /*
     *  Main class to handle the game current info and to facilitate important classes conversation
     *
     */
    public class GameManager : MonoBehaviour
    {
        //Singleton of the GameManager
        public static GameManager Instance;

        //Inportant classes
        private UIManager _uIManager;
        private InventoryManager _inventoryManager;
        private PlayerController _player;

        private int _money = 100;

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            _inventoryManager = transform.GetComponentInChildren<InventoryManager>();
            _uIManager = transform.GetComponentInChildren<UIManager>();
        }

        //Start the conversation segment
        public void StartConversation(MessageSegment conversation)
        {
            _player.DeactivateInputs();
            _uIManager.LoadMessageSegment(conversation);
        }

        //Activate or deactivate the player inputs
        public void SetPlayerControl(bool value)
        {
            if (value)
            {
                _uIManager.DeactivateInputs();
                _player.ActivateInputs();
                return;
            }

            _uIManager.ActivateInputs();
            _player.DeactivateInputs();
        }

        //Adds a new item to player's inventory
        public void RegisterNewItemToPlayerInventory(Item itemToAdd)
        {
            _inventoryManager.AddItemToPlayerInventory(itemToAdd);
        }
        //Removes a new item from the player's inventory
        public void RemoveItemFromPlayerInventory(Item itemToRemove)
        {
            _inventoryManager.RemoveItemFromPlayerInventory(itemToRemove);
        }

        //Add money
        public void AddMoney(int value)
        {
            _money += value;
        }
        //Remove money, returning true if operation was successful
        public bool RemoveMoney(int value)
        {
            if(_money < value)
            {
                return false;
            }

            _money -= value;
            return true;
        }

        //Open the current special panel
        public void OpenSpecialPanel()
        {
            _uIManager.OpenInterfaceSpecialPanel();
        }

        //----------------------
        // GET FUNCTIONS
        //----------------------
        public int CurrentMoney()
        {
            return _money;
        }
        public List<Clothing> GetAvailableClothes()
        {
            return _inventoryManager.GetAvailableClothes();
        }
        public List<Item> GetPlayerInventory(bool withSellingPrices)
        {
            return _inventoryManager.Inventory(withSellingPrices);
        }

        //----------------------
        // UNITY CALLBACKS
        //----------------------
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoad;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded += OnSceneLoad;
        }

        //Scene load callback, use to set up important classes in a new scene
        private void OnSceneLoad(Scene scene, LoadSceneMode loadScene)
        {
            _uIManager.SetUp();

            _player = FindObjectOfType<PlayerController>();
        }
    }
}
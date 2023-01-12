using Main.Inventory;
using Enums;
using Main.UI;
using Scenario;
using Audio;
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
        [SerializeField] private UIManager uIManager;
        private InventoryManager _inventoryManager;
        private PlayerController _player;

        private int _money = 0;
        private int _entranceBuffer = 0;

        private Dictionary<string, string> _sceneMusic = new Dictionary<string, string>();

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

            _sceneMusic["scene_Archipelago"] = "m_Exterior";
            _sceneMusic["scene_House"] = "m_House";
            _sceneMusic["scene_Shop"] = "m_Shop";
        }

        //Start the conversation segment
        public void StartConversation(MessageSegment conversation)
        {
            _player.DeactivateInputs();
            uIManager.LoadMessageSegment(conversation);
        }

        //Activate or deactivate the player inputs
        public void SetPlayerControl(bool value)
        {
            if (value)
            {
                uIManager.DeactivateInputs();
                _player.ActivateInputs();
                return;
            }

            uIManager.ActivateInputs();
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
            uIManager.OpenInterfaceSpecialPanel();
        }

        public void StartSceneTransition(string sceneName, int entranceIndex = 0)
        {
            _player.DeactivateInputs();
            _entranceBuffer = entranceIndex;

            AudioManager.Instance.FadeMusic(0, .3f);
            uIManager.PlaySceneTransitionAnimation(callback: () => 
            {
                if(sceneName.Length > 0)
                {
                    SceneManager.LoadScene(sceneName);
                    return;
                }

                Application.Quit();
            });
        }

        //----------------------
        // GET FUNCTIONS
        //----------------------
        public int CurrentMoney()
        {
            return _money;
        }
        public List<Clothing> GetAvailableClothes(bool toBuy = true)
        {
            return _inventoryManager.GetAvailableClothes(toBuy);
        }
        public List<Item> GetPlayerInventory(bool withSellingPrices)
        {
            return _inventoryManager.Inventory(withSellingPrices);
        }
        public PlayerController Player()
        {
            return _player;
        }
        public InventoryManager Inventory()
        {
            return _inventoryManager;
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
            uIManager.SetUp();

            AudioManager.Instance.PlayMusic(_sceneMusic[scene.name]);
            AudioManager.Instance.FadeMusic(1, .3f);

            _player = FindObjectOfType<PlayerController>();
            FindObjectOfType<PlayerEntrance>().TransportPlayer(_entranceBuffer);

            _inventoryManager.DressPlayer();
        }
    }
}
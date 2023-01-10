using Main.UI;
using Scenario;
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
        private PlayerController _player;

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
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
                _player.ActivateInputs();
                return;
            }

            _player.DeactivateInputs();
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

        //Scene load callback, use to load the important classes in a new scene
        private void OnSceneLoad(Scene scene, LoadSceneMode loadScene)
        {
            _uIManager = FindObjectOfType<UIManager>();
            _uIManager.SetUp();

            _player = FindObjectOfType<PlayerController>();
        }
    }
}
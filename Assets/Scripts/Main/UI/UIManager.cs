using Scenario;
using System;
using UnityEngine;

namespace Main.UI
{
    /*
     *  Main class to send commands and informations to the current UI class handler
     * 
     *  Works as a bridge between the game world and it's interface
     */
    public class UIManager : MonoBehaviour
    {
        //Input System: receive input information
        private PlayerInputs _inputActions;

        //Variable that buffs the current UI class handler
        private GameInterface _gameInterface;

        //Variables that helps in the message box logic
        private int _conversationIndex;
        private bool _inConversation;
        private MessageSegment _currentMessageSegment;

        //Set the input callbacks
        private void Awake()
        {
            _inputActions = new PlayerInputs();

            _inputActions.Interface.Navigation.performed += ctx => ReceiveNavigationInput(ctx.ReadValue<Vector2>());
            _inputActions.Interface.Navigation.canceled += ctx => ReceiveNavigationInput(Vector2.zero);

            _inputActions.Interface.Select.performed += ctx => ReceiveSelectInput();
        }

        //Activate the interface inputs and prepare the conversation end callback
        public void ActivateInputs()
        {
            _inputActions.Enable();
        }

        //Deactivate the interface inputs and the conversation end callback
        public void DeactivateInputs()
        {
            _inputActions.Disable();
        }

        private void ReceiveNavigationInput(Vector2 direction)
        {

        }

        //Function that receive the confirmation input from the player
        private void ReceiveSelectInput()
        {
            //In case the interface is in a conversation state
            if (_inConversation)
            {
                //Checks if the game interface is still animating the message box text, finishing imediatly in case
                if (_gameInterface.InMessageAnimation)
                {
                    GoToNextMessage(_currentMessageSegment.Messages()[_conversationIndex - 1]);
                    return;
                }

                //If there is no more texts to show, the conversation segment ends
                if (_conversationIndex >= _currentMessageSegment.Messages().Length)
                {
                    _inConversation = false;
                    _gameInterface.CloseConversation();
                    return;
                }

                //Show the next text
                GoToNextMessage(_currentMessageSegment.Messages()[_conversationIndex]);
                _conversationIndex++;
            }
        }

        //Get the message segment and set up the interface and the player inputs
        public void LoadMessageSegment(MessageSegment conversation)
        {
            ActivateInputs();
            GoToNextMessage(conversation.Messages()[0]);
            _currentMessageSegment = conversation;

            _inConversation = true;
            _conversationIndex = 1;
        }

        //Get the message information and sends it to the game interface class
        private bool GoToNextMessage(Message newMessage)
        {
            var message = newMessage.messageText;
            var character = newMessage.character;
            var mood = newMessage.mood;
            var newEvent = newMessage.eventToTrigger;

            return _gameInterface.ShowMessageOnScreen(message, character, mood, newEvent);
        }

        //Function called in the scene beginning
        public void SetUp()
        {
            _gameInterface = FindObjectOfType<GameInterface>();
        }
    }
}

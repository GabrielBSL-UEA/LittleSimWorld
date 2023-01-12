using Main;
using Main.Inventory;
using UnityEngine;

namespace Scenario
{
    /*
     *  Main component of the player character
     * 
     *  Receive information and pass to the other components
     */

    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerAnimation))]
    [RequireComponent(typeof(PlayerInteraction))]
    public class PlayerController : MonoBehaviour
    {
        //Input System: receive input information
        private PlayerInputs _inputActions;

        //Player components
        private PlayerMovement _playerMovement;
        private PlayerAnimation _playerAnimation;
        private PlayerInteraction _playerInteraction;

        //Rigidbody2D, used to set player movement
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            //Set player input action callbacks
            _inputActions = new PlayerInputs();

            _inputActions.Player.Movement.performed += ctx => ReceiveDirectionalInput(ctx.ReadValue<Vector2>());
            _inputActions.Player.Movement.canceled += ctx => ReceiveDirectionalInput(Vector2.zero);

            _inputActions.Player.Interaction.performed += _ => ReceiveInteractionInput();

            //Set the other components
            TryGetComponent(out _playerMovement);
            TryGetComponent(out _playerAnimation);
            TryGetComponent(out _playerInteraction);

            _playerMovement.Controller = this;
            _playerAnimation.Controller = this;
            _playerInteraction.Controller = this;

            TryGetComponent(out _rigidbody2D);
        }

        //Input activation/deactivation functions
        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        public void ActivateInputs()
        {
            _inputActions.Enable();
        }
        public void DeactivateInputs()
        {
            _inputActions.Disable();
        }

        //Receive directional input and pass the information to the required components
        private void ReceiveDirectionalInput(Vector2 direction)
        {
            _playerMovement.TranslateMovementInputs(direction);
        }
        //Receive interaction input and pass the information to the required components
        private void ReceiveInteractionInput()
        {
            _playerInteraction.TriggerInteraction();
        }

        public void AddItemIntoInventory(Item newItem)
        {
            GameManager.Instance.RegisterNewItemToPlayerInventory(newItem);
        }

        //--------------------
        // GET FUNCTIONS
        //--------------------
        public Rigidbody2D Rigidbody2D()
        {
            return _rigidbody2D;
        }
    }
}

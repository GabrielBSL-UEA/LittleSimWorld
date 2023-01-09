using System;
using UnityEngine;

namespace Scenario
{
    /*
     *  Movement component of the player character
     * 
     *  define player velocity
     */

    public class PlayerMovement : MonoBehaviour
    {
        //Communication with the main component
        public PlayerController Controller { get; set; }

        //Max velocity of the player
        [Header("Directional")]
        [SerializeField] private float movementSpeed;

        //Current direction
        private Vector2 _currentDirection;

        private void FixedUpdate()
        {
            SetPlayerVelocity();
        }

        //Set player velocity based on the saved current direction
        private void SetPlayerVelocity()
        {
            Controller.Rigidbody2D().velocity = _currentDirection * movementSpeed;
        }

        //-------------------
        // INPUT FUNCTIONS
        //-------------------
        public void TranslateMovementInputs(Vector2 newDirection)
        {
            //Set new direction to _currentDirection
            _currentDirection = newDirection;
        }
    }
}

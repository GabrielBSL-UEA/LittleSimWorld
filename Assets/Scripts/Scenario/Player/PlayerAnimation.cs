using System.Collections.Generic;
using UnityEngine;
using Utils;
using Enums;
using Main.Inventory;

namespace Scenario
{
    /*
     *  Animation component of the player character
     * 
     *  Translate player and external inputs into animation
     */
    public class PlayerAnimation : MonoBehaviour
    {
        //Communication with the main component
        public PlayerController Controller { get; set; }

        //Reference to the Animator component in the PlayerSprite, child of the main object
        [Header("Main")]
        [SerializeField] private Animator playerAnimator;

        [Header("Clothing")]
        [SerializeField] private Animator hairAnimator;
        [SerializeField] private Animator hatAnimator;
        [SerializeField] private Animator outfitAnimator;

        //Enum variables that keeps track of the current state of the character
        private direction _currentDirection = direction.down;
        private AnimationNames _currentAnimation = AnimationNames.idle_down;

        //Dictionary that help translate direction into AnimationNames
        //In the tuple, the second value (int) defines the context of the animation to play based in the direction, the first value
        private Dictionary<(direction, int), AnimationNames> _moveToAnimationName = new Dictionary<(direction, int), AnimationNames>();

        private void Awake()
        {
            SetUpAnimNamesDictionary();
        }

        public void LoadClothing(Clothing toDress)
        {
            toDress.SetPlayerClothing(this);
        }

        //Function that sets up the translator dictionary
        private void SetUpAnimNamesDictionary()
        {
            _moveToAnimationName[(direction.up, 0)] = AnimationNames.idle_up;
            _moveToAnimationName[(direction.down, 0)] = AnimationNames.idle_down;
            _moveToAnimationName[(direction.left, 0)] = AnimationNames.idle_left;
            _moveToAnimationName[(direction.right, 0)] = AnimationNames.idle_right;

            _moveToAnimationName[(direction.up, 1)] = AnimationNames.walk_up;
            _moveToAnimationName[(direction.down, 1)] = AnimationNames.walk_down;
            _moveToAnimationName[(direction.left, 1)] = AnimationNames.walk_left;
            _moveToAnimationName[(direction.right, 1)] = AnimationNames.walk_right;
        }

        void FixedUpdate()
        {
            SetAutomaticAnimation();
        }

        //Function that define, automatically, the animation to play
        private void SetAutomaticAnimation()
        {
            //Get the current velocity
            var direction = Controller.Rigidbody2D().velocity;

            //Translate the current velocity into a direction variable
            GetCurrentDirection(direction);

            //Depending if the player is stationary or not, the _moveToAnimationName will return a different animation
            if(direction == Vector2.zero)
            {
                PlayAnimation(_moveToAnimationName[(_currentDirection, 0)]);
                return;
            }

            PlayAnimation(_moveToAnimationName[(_currentDirection, 1)]);
        }


        //Fucntion that translate a direction vector2 into a direction variable
        private void GetCurrentDirection(Vector2 direction)
        {
            if(direction == Vector2.zero)
            {
                return;
            }

            if(Mathf.Abs(direction.x) > .01f)
            {
                if(direction.x > .01f)
                {
                    _currentDirection = Enums.direction.right;
                    return;
                }

                _currentDirection = Enums.direction.left;
                return;
            }

            if(direction.y > .01f)
            {
                _currentDirection = Enums.direction.up;
                return;
            }

            _currentDirection = Enums.direction.down;
        }

        //Function that plays a animation but only if this animation is not playing at the moment
        private void PlayAnimation(AnimationNames newAnimation)
        {
            if (_currentAnimation == newAnimation)
            {
                return;
            }

            _currentAnimation = newAnimation;

            var animationName = Animations.Name(newAnimation);
            playerAnimator.Play(animationName);

            SetClothingAnimation(animationName);
        }

        //Play the current animation on the clothes animators
        private void SetClothingAnimation(string animationName)
        {
            hatAnimator.Play(animationName);
            hairAnimator.Play(animationName);
            outfitAnimator.Play(animationName);
        }

        //--------------------
        // GET FUNCTIONS
        //--------------------
        public Animator HairAnimator()
        {
            return hairAnimator;
        }
        public Animator HatAnimator()
        {
            return hatAnimator;
        }
        public Animator OutfitAnimator()
        {
            return outfitAnimator;
        }
    }
}

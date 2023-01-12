using System.Collections;
using UnityEngine;

namespace Scenario
{
    /*
     *  Main class to interaction
     * 
     *  Any class that require player interaction need to be Inherit from this class
     */
    public class Interactable : MonoBehaviour
    {
        //Outline sprite renderers
        [SerializeField] protected SpriteRenderer[] outlines;

        private Animator _animator;
        private bool _selected;

        protected virtual void Awake()
        {
            //Security meajure in case outline doesn't have elements
            if (outlines.Length == 0)
            {
                outlines = new SpriteRenderer[4];

                for (int i = 0; i < 4; i++)
                {
                    transform.GetChild(i).TryGetComponent(out SpriteRenderer outline);
                    outlines[i] = outline;
                }
            }

            SetOutline(false);

            TryGetComponent(out _animator);
            if(_animator != null)
            {
                StartCoroutine(OutlineSpriteUpdate());
            }
        }

        //Set the interactable outlines
        public void SetOutline(bool enableValue)
        {
            _selected = enableValue;

            for (int i = 0; i < outlines.Length; i++)
            {
                outlines[i].enabled = enableValue;
            }
        }

        //This IEnumerator is called just in case the interactable object has an animator, meaning we have to update the outline sprites
        private IEnumerator OutlineSpriteUpdate()
        {
            TryGetComponent(out SpriteRenderer mainRenderer);

            while (true)
            {
                if (_selected)
                {
                    for (int i = 0; i < outlines.Length; i++)
                    {
                        outlines[i].sprite = mainRenderer.sprite;
                    }
                }

                yield return null;
            }
        }

        //Interaction function, to be define by it's inherit classes
        public virtual void Interact(PlayerInteraction interactor)
        {

        }
    }
}

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
        [SerializeField] private SpriteRenderer[] outlines;

        private void Awake()
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
        }

        //Set the interactable outlines
        public void SetOutline(bool enableValue)
        {
            for (int i = 0; i < outlines.Length; i++)
            {
                outlines[i].enabled = enableValue;
            }
        }

        //Interaction function, to be define by it's inherit classes
        public virtual void Interact()
        {

        }
    }
}

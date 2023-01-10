using Main;
using UnityEngine;

namespace Scenario
{
    /*
     *  Class to start a message segment in case of interaction
     * 
     *  Any object that want to start a message segment needs to have this class or an inherit class
     */
    public class Conversation : Interactable
    {
        //The sequence of messages
        [SerializeField] private MessageSegment[] segments;

        //Function that selects a random message segment and sends it to the GameManager
        public override void Interact()
        {
            if(segments.Length == 0)
            {
                Debug.LogWarning("Interaction without message segments");
                return;
            }

            var randomSegment = Random.Range(0, segments.Length);

            GameManager.Instance.StartConversation(segments[randomSegment]);
        }
    }
}
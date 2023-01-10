using Scenario;
using UnityEngine;

namespace Scenario
{
    /*
     *  Scriptable object class that saves a message sequence, avoiding having to manually add texts to the interactable object
     *
     */
    [CreateAssetMenu(fileName = "MessageSegment", menuName = "Custom/MessageSegment")]
    public class MessageSegment : ScriptableObject
    {
        [SerializeField] private Message[] _messageSequence;

        public Message[] Messages()
        {
            return _messageSequence;
        }
    }
}

using Enums;

namespace Scenario
{
    //Class that saves information of a single line of conversation
    [System.Serializable]
    public struct Message
    {
        public Characters character;
        public Mood mood;
        public string messageText;
    }
}
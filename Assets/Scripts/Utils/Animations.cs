using System;
using System.Collections.Generic;
using Enums;

namespace Utils
{
    /*
     *  Class responsible to translate the AnimationNames to string, avoiding having to do .ToString() everytime a animation is played
     */

    public static class Animations
    {
        //Dictionary that translate animationNames into your string conversion
        private static Dictionary<AnimationNames, string> _animationDictionary = new Dictionary<AnimationNames, string>();

        //Function that receive a AnimationName enum and returns your string conversion
        public static string Name(AnimationNames animName)
        {
            if (_animationDictionary.Count == 0)
            {
                SetUpAnimDictionary();
            }

            return _animationDictionary[animName];
        }

        //This function set up the _animationDictionary in the case the values are not yet passed
        private static void SetUpAnimDictionary()
        {
            foreach (AnimationNames anim in Enum.GetValues(typeof(AnimationNames)))
            {
                _animationDictionary[anim] = anim.ToString();
            }
        }
    }

}
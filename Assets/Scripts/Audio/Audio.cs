using UnityEngine;

namespace Audio
{
    [System.Serializable]
    public class Audio
    {
        [HideInInspector] public AudioSource source;

        public string name;
        public AudioClip clip;
        public float volume;
        public bool isMusic;
    }
}
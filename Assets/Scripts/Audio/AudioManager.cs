using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField] private List<Audio> audios;

        private Audio _currentMusic;
        private float _musicVolumeFade = 1;

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            Instance = this;

            foreach (var audio in audios)
            {
                audio.source = gameObject.AddComponent<AudioSource>();

                audio.source.name = audio.name;
                audio.source.clip = audio.clip;
                audio.source.volume = audio.volume;

                audio.source.loop = audio.isMusic;
            }
        }

        public void PlaySFX(string name)
        {
            var audioToPlay = audios.Find(a => a.name == name);

            if(audioToPlay == null)
            {
                return;
            }

            audioToPlay.source.Play();
        }

        public void PlayMusic(string name)
        {
            var musicToPlay = audios.Find(m => m.name == name);

            if(musicToPlay == null || musicToPlay == _currentMusic || !musicToPlay.isMusic)
            {
                return;
            }

            if(_currentMusic != null)
            {
                _currentMusic.source.Stop();
            }

            musicToPlay.source.Play();
            _currentMusic = musicToPlay;
        }

        public void FadeMusic(float volume, float duration)
        {
            LeanTween.cancel(gameObject);

            var oldFadeVolume = _musicVolumeFade;

            LeanTween.value(oldFadeVolume, volume, duration)
                .setOnUpdate((float value) =>
                {
                    _musicVolumeFade = value;
                    _currentMusic.source.volume = _currentMusic.volume * value;
                });
        }
    }
}

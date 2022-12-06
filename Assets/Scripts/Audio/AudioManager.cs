using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private float soundVolume = 1f;

        public static AudioManager Instance;

        public float GlobalVolume
        {
            get => soundVolume;
            set => soundVolume = value;
        }

        private void Awake ()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject, 1f);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }

        public void PlaySound (Sound sound)
        {
            if (sound == null)
            {
                Debug.LogWarning("Sound not defined!");
                return;
            }
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.audio;
            sound.audioSource.volume = (sound.volume * GlobalVolume);
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.loop;
            sound.audioSource.rolloffMode = AudioRolloffMode.Linear;
            sound.audioSource.Play();
            Destroy(sound.audioSource, sound.audioSource.clip.length + 0.1f);
        }

        public void PlayRandomSound (Sound[] soundArray)
        {
            PlaySound(soundArray[Random.Range(0, soundArray.Length)]);
        }
    }
}

using UnityEngine;

namespace Audio
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] AudioSource _music;

        private void Awake()
        {
            _music = GetComponent<AudioSource>();    
        }

        private void Start()
        {
            _music.volume = AudioManager.Instance.GlobalVolume;
            _music.Play();
        }
    }
}

using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
	[System.Serializable]
	public class Sound
	{
		[HideInInspector]
		public AudioSource audioSource;

		public string name;
	
		public AudioClip audio;

		[Range(0f, 1f)] public float volume = 1f;

		[Range(.1f, 3f)] public float pitch = 1f;
	
		public bool loop = false;
	}
}


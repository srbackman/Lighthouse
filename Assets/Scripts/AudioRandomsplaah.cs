using UnityEngine;
using System.Collections;

public class AudioRandomsplaah : MonoBehaviour
{


    
        [Header("Settings")]
        public AudioClip[] playlist;
        public bool playOnStart = true;

        private AudioSource _audioSource;

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();

            if (playOnStart && playlist.Length > 0)
            {
                StartCoroutine(PlayPlaylistRoutine());
            }
        }

        IEnumerator PlayPlaylistRoutine()
        {
            while (true)
            {
                // 1. Pick a random clip from the array
                AudioClip nextClip = playlist[Random.Range(0, playlist.Length)];

                // 2. Assign and play
                _audioSource.clip = nextClip;
                _audioSource.Play();

                // 3. Wait until the clip is finished
                // We wait for the duration of the clip plus a tiny buffer
                yield return new WaitForSeconds(nextClip.length);

                // Optional: Small delay between songs
                yield return new WaitForSeconds(0.5f);
            }
        }
    }


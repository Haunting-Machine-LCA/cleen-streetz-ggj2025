using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.Untitled
{
    public class AudioManager : MonoBehaviour
    {
        public AudioClip menuMusic;
        public AudioClip gameMusic;

        private AudioSource audioSource;
        private AudioSource sfxSource;
        public AudioClip hmSFX;
        public AudioClip backSFX;
        public AudioClip navSFX;
        public AudioClip selectSFX;

        void Awake()
        {
            DontDestroyOnLoad(gameObject); // Keep playing across scenes

            // Create component
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.playOnAwake = false;
            audioSource.volume = 0.5f;
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;
            sfxSource.volume = 1f;

            // Start on menu
            audioSource.clip = menuMusic;
        }

        void Update()
        {
            // if (Input.GetKeyDown(KeyCode.Space))
            // {
            //     CrossfadeMusic(gameMusic);
            // }
        }

        public void StartMainMenuMusic()
        {
            PlayMusic(menuMusic);
        }

        public void PlayMusic(AudioClip clip)
        {
            if (audioSource.clip == clip && audioSource.isPlaying)
                return;

            audioSource.clip = clip;
            audioSource.Play();

        }

        public void PlayBackSFX()
        {
            PlaySFX(backSFX);
        }

        public void PlayNavSFX()
        {
            PlaySFX(navSFX);
        }

        public void PlaySelectSFX()
        {
            PlaySFX(navSFX);
        }

        public void PlayHMSFX()
        {
            PlaySFX(hmSFX);
        }

        private void PlaySFX(AudioClip clip)
        {
            sfxSource.PlayOneShot(clip);
        }

        public void CrossfadeMusic(AudioClip newClip, float fadeDuration = 1.5f)
        {
            StartCoroutine(FadeMusic(newClip, fadeDuration));
        }

        private IEnumerator FadeMusic(AudioClip clip, float duration)
        {
            float startVolume = audioSource.volume;

            // Fade out
            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / duration;
                yield return null;
            }

            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();

            // Fade in
            while (audioSource.volume < startVolume)
            {
                audioSource.volume += startVolume * Time.deltaTime / duration;
                yield return null;
            }
        }
    }
}

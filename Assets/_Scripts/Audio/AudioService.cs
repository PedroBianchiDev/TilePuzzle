using System.Linq;
using TilePuzzle.Core;
using UnityEngine;

namespace TilePuzzle.Audio
{
    public class AudioService : MonoBehaviourService
    {
        [Header("Fontes de Áudio (Audio Sources)")]
        [SerializeField]
        private AudioSource musicSource;

        [SerializeField]
        private AudioSource sfxSource;

        [SerializeField]
        private AudioConfiguration audioConfiguration;

        private float musicVolume = 1f;
        private float sfxVolume = 1f;

        public override void Initialize()
        {

        }

        public void PlaySong(string songId)
        {
            AudioClip song = GetAudioClipByGroupAndId(AudioType.MUSIC, songId);

            if (song == null) return;

            musicSource.clip = song;
            musicSource.loop = true;
            SetMusicVolume(musicVolume);
            musicSource.Play();
        }

        public void PlaySFX(string sfxId)
        {
            AudioClip sfx = GetAudioClipByGroupAndId(AudioType.SFX, sfxId);

            if (sfx == null) return;

            sfxSource.clip = sfx;
            SetSFXVolume(sfxVolume);
            sfxSource.Play();
        }

        private void SetMusicVolume(float volume)
        {
            musicVolume = Mathf.Clamp01(volume);
            musicSource.volume = musicVolume;
        }

        private void SetSFXVolume(float volume)
        {
            sfxVolume = Mathf.Clamp01(volume);
            sfxSource.volume = musicVolume;
        }

        private AudioGroup GetAudioGroupByType(AudioType audioType)
        {
            //return audioConfiguration.audioGroups.FirstOrDefault(group => group.type == audioType);

            foreach (AudioGroup item in audioConfiguration.audioGroups)
            {
                if (item.type == audioType)
                {
                    return item;
                }
            }

            return null;
        }

        private AudioClip GetAudioClipByGroupAndId(AudioType audioType, string id)
        {
            AudioGroup audioGroup = GetAudioGroupByType(audioType);

            if (audioGroup == null)
                return null;

            if (audioGroup.audioFiles == null || audioGroup.audioFiles.Count <= 0)
                return null;

            //audioGroup.audioFiles.FirstOrDefault(file => file.id == id);
            foreach (AudioFile item in audioGroup.audioFiles)
            {
                if (item.id == id)
                {
                    return item.clip;
                }
            }

            return null;
        }

        public void TocarSFX(AudioClip clip)
        {
            if (clip != null)
            {
                sfxSource.PlayOneShot(clip);
            }
        }
    }
}
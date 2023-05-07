using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoisonArch;
using System;

namespace PoisonArch
{
    /// <summary>
    /// Handles playing sounds and music based on their sound ID
    /// </summary>
    public enum SoundID
    {
        None,
        CoinSound,
        KeySound,
        ButtonSound,
        MenuMusic,
    }
    public enum MusicSourceID
    {
        All,
        MenuMusicSource,
    }
    public enum EffectSourceID
    {
        All,
        CoinEffectSource,
    }
    public class AudioManager : AbstractSingleton<AudioManager>
    {
        [Serializable]
        class SoundIDClipPair
        {
            public SoundID m_SoundID;
            public AudioClip m_AudioClip;
        }
        [Serializable]
        class MusicSourceIDPair
        {
            public MusicSourceID music_SourceID;
            public AudioSource music_Sources;
        }
        [Serializable]
        class EffectSourceIDPair
        {
            public EffectSourceID effect_SourceID;
            public AudioSource effect_Sources;
        }

        [SerializeField, Min(0f)]
        float m_MinSoundInterval = 0.1f;

        [SerializeField]
        SoundIDClipPair[] m_Sounds;
        [SerializeField]
        MusicSourceIDPair[] music_Sources;
        [SerializeField]
        EffectSourceIDPair[] effect_Sources;

        float m_LastSoundPlayTime;
        readonly Dictionary<SoundID, AudioClip> m_Clips = new();
        readonly Dictionary<MusicSourceID, AudioSource> m_Sources = new();
        readonly Dictionary<EffectSourceID, AudioSource> e_Sources = new();

        AudioSettings m_AudioSettings = new();

        /// <summary>
        /// Unmute/mute the music
        /// </summary>
        public bool EnableMusic
        {
            get => m_AudioSettings.EnableMusic;
            set
            {
                m_AudioSettings.EnableMusic = value;
                for (int i = 0; i < m_Sources.Count; i++)
                {
                    m_Sources[MusicSourceID.All].mute = !value;
                }
            }
        }

        /// <summary>
        /// Unmute/mute all sound effects
        /// </summary>
        public bool EnableSfx
        {
            get => m_AudioSettings.EnableSfx;
            set
            {
                m_AudioSettings.EnableSfx = value;
                for (int i = 0; i < e_Sources.Count; i++)
                {
                    e_Sources[EffectSourceID.All].mute = !value;
                }
            }
        }

        /// <summary>
        /// The Master volume of the audio listener
        /// </summary>
        public float MasterVolume
        {
            get => m_AudioSettings.MasterVolume;
            set
            {
                m_AudioSettings.MasterVolume = value;
                AudioListener.volume = value;
            }
        }

        void Start()
        {
            foreach (var sound in m_Sounds)
            {
                m_Clips.Add(sound.m_SoundID, sound.m_AudioClip);
            }
            foreach (var mSources in music_Sources)
            {
                m_Sources.Add(mSources.music_SourceID, mSources.music_Sources);
            }
            foreach (var eSources in effect_Sources)
            {
                e_Sources.Add(eSources.effect_SourceID, eSources.effect_Sources);
            }
        }

        void OnEnable()
        {
            if (SaveManager.Instance == null)
            {
                // Disable music, enable sfx, and 
                // set volume to a very low amount
                // in the LevelEditor
                EnableMusic = true;
                EnableSfx = true;
                MasterVolume = 0.2f;
                return;
            }

            var audioSettings = SaveManager.Instance.LoadAudioSettings();
            EnableMusic = audioSettings.EnableMusic;
            EnableSfx = audioSettings.EnableSfx;
            MasterVolume = audioSettings.MasterVolume;
        }

        void OnDisable()
        {
            if (SaveManager.Instance == null)
            {
                return;
            }

            SaveManager.Instance.SaveAudioSettings(m_AudioSettings);
        }

        void PlayMusic(AudioClip audioClip, AudioSource sources, bool looping = true)
        {
            if (sources.isPlaying)
                return;

            sources.clip = audioClip;
            sources.loop = looping;
            sources.Play();
        }
        void PlayMusicForStart(AudioClip audioClip, AudioSource sources, bool looping = true)
        {
            sources.clip = audioClip;
            sources.loop = looping;
            sources.Play();
        }

        /// <summary>
        /// Play a music based on its sound ID
        /// </summary>
        /// <param name="soundID">The ID of the music</param>
        /// <param name="looping">Is music looping?</param>
        public void PlayMusic(SoundID soundID, MusicSourceID sourceID, bool looping = true)
        {
            PlayMusic(m_Clips[soundID], m_Sources[sourceID], looping);
        }
        public void PlayMusicForStart(SoundID soundID, MusicSourceID sourceID, bool looping = true)
        {
            PlayMusicForStart(m_Clips[soundID], m_Sources[sourceID], looping);
        }

        /// <summary>
        /// Stop the current music
        /// </summary>
        public void StopMusic(MusicSourceID sourceID)
        {
            m_Sources[sourceID].Stop();
        }

        void PlayEffect(AudioClip audioClip, AudioSource sources)
        {
            if (Time.time - m_LastSoundPlayTime >= m_MinSoundInterval)
            {
                sources.PlayOneShot(audioClip);
                m_LastSoundPlayTime = Time.time;
            }
        }

        /// <summary>
        /// Play a sound effect based on its sound ID
        /// </summary>
        /// <param name="soundID">The ID of the sound effect</param>
        public void PlayEffect(SoundID soundID, EffectSourceID sourceID)
        {
            if (soundID == SoundID.None)
                return;

            PlayEffect(m_Clips[soundID], e_Sources[sourceID]);
        }
    }
    public class AudioSettings
    {
        public bool EnableMusic;
        public bool EnableSfx;
        public float MasterVolume;

        public AudioSettings()
        {
            EnableMusic = true;
            EnableSfx = true;
            MasterVolume = 0.25f;
        }
    }
}



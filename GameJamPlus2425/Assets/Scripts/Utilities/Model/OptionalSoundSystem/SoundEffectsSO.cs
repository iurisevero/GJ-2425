using System;
using System.Collections.Generic;
using UnityEngine;

namespace F
{
    [CreateAssetMenu(fileName = "NewSoundEffect", menuName = "Audio/New Sound Effect")]
    public class SoundEffectSO : ScriptableObject
    {
        private static readonly float SEMITONES_TO_PITCH_CONVERSION_UNIT = 1.05946f;
        
        public AudioClip[] clips;
        public Vector2 volume = new Vector2(0.5f, 0.5f);
        public bool useSemitones;
        public Vector2Int semitones = new Vector2Int(0, 0);
        public Vector2 pitch = new Vector2(1f, 1f);
        
        [SerializeField] private SoundClipPlayOrder playOrder;
        [SerializeField] private int playIndex;
        
        private enum SoundClipPlayOrder
        {
            random,
            in_order,
            reverse
        }

        public void SyncPitchAndSemitones()
        {
            if (useSemitones)
            {
                pitch.x = Mathf.Pow(SEMITONES_TO_PITCH_CONVERSION_UNIT, semitones.x);
                pitch.y = Mathf.Pow(SEMITONES_TO_PITCH_CONVERSION_UNIT, semitones.y);
            }
            else
            {
                semitones.x = Mathf.RoundToInt(Mathf.Log10(pitch.x) / Mathf.Log10(SEMITONES_TO_PITCH_CONVERSION_UNIT));
                semitones.y = Mathf.RoundToInt(Mathf.Log10(pitch.y) / Mathf.Log10(SEMITONES_TO_PITCH_CONVERSION_UNIT));
            }
        }

        private AudioClip GetAudioClip()
        {
            AudioClip result = clips[(playIndex >= clips.Length) ? 0 : playIndex];
            switch (playOrder)
            {
                case SoundClipPlayOrder.random:
                    playIndex = UnityEngine.Random.Range(0, clips.Length);
                    break;
                case SoundClipPlayOrder.in_order:
                    playIndex = (playIndex + 1) % clips.Length;
                    break;
                case SoundClipPlayOrder.reverse:
                    playIndex = (playIndex + clips.Length - 1) % clips.Length;
                    break;
            }
            return result;
        }

        public AudioSource Play(AudioSource audioSourceParam = null)
        {
            if (clips.Length == 0)
            {
                Debug.LogError("Missing sound clips for " + name);
                return null;
            }

            AudioSource audioSource = audioSourceParam ?? AudioManager.Instance.GetPooledAudioSource();
            if (audioSource == null) return null;

            audioSource.clip = GetAudioClip();
            audioSource.volume = AudioManager.Instance.SFXVolume * UnityEngine.Random.Range(volume.x, volume.y);
            audioSource.pitch = useSemitones
                ? Mathf.Pow(SEMITONES_TO_PITCH_CONVERSION_UNIT, UnityEngine.Random.Range(semitones.x, semitones.y))
                : UnityEngine.Random.Range(pitch.x, pitch.y);

            audioSource.Play();
            AudioManager.Instance.ReturnAudioSourceToPool(audioSource, audioSource.clip.length / audioSource.pitch);
            return audioSource;
        }
    }
}

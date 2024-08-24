using System;
using UnityEngine;
using UnityEngine.Audio;


namespace Yrr.Audio
{
    internal sealed class SoundVolumeChanger : MonoBehaviour
    {
        [SerializeField] private AudioMixer mixer;

        [SerializeField] private VolumeSettings[] musicSettings;
        [SerializeField] private VolumeSettings[] soundsSettings;

        internal void SetMusicVolume(float volume)
        {
            for (int i = 0; i < musicSettings.Length; i++)
            {
                var set = musicSettings[i];
                mixer.SetFloat(set.mixerGroupName, ConvertToDb(set.globalModificator * volume));
            }
        }

        internal void SetSoundsVolume(float volume)
        {
            for (int i = 0; i < soundsSettings.Length; i++)
            {
                var set = soundsSettings[i];
                mixer.SetFloat(set.mixerGroupName, ConvertToDb(set.globalModificator * volume));
            }
        }

        private float ConvertToDb(float volume)
        {
            return Mathf.Lerp(-80, 0, volume);
        }
    }


    [Serializable]
    internal struct VolumeSettings
    {
        [SerializeField] public string mixerGroupName;
        [SerializeField] public float globalModificator;
    }
}

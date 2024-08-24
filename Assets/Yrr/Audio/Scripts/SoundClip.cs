using System;
using UnityEngine;


namespace Yrr.Audio
{
    [Serializable]
    public struct SoundClip
    {
        public AudioClip Clip;
        [Range(0, 2)]
        public float ClipVolume;
        [Range(-3, 3)]
        public float BasePitch;
        [Range(0, 3)]
        public float PitchRandom;
    }
}

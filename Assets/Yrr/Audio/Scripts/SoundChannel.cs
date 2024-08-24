using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Yrr.Utils;


namespace Yrr.Audio
{
    internal sealed class SoundChannel : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup mixerGroup;
        [SerializeField] private SoundsPool pool;
        [SerializeField] private SoundCatalog sounds;
        [SerializeField] private int maxSounds = 10;

        [SerializeField] private float fadeInTime;
        [SerializeField] private float fadeOutTime;

        [SerializeField] private bool loopSound;


        private readonly LinkedList<AudioSource> _playingSounds = new();
        private readonly List<AudioSource> _removableSounds = new();

        private AudioListener _listener;
        private AudioListener listener
        {
            get
            {
                if (_listener == null || !_listener.isActiveAndEnabled)
                {
                    _listener = FindObjectOfType<AudioListener>();
                }
                return _listener;
            }
        }


        private void Update()
        {
            foreach (var sound in _playingSounds)
            {
                if (!sound.loop && sound.time >= sound.clip.length)
                {
                    _removableSounds.Add(sound);
                }
            }


            if (_removableSounds.Count == 0) return;


            foreach (var sound in _removableSounds)
            {
                _playingSounds.Remove(sound);
                pool.DespawnObject(sound);
            }

            _removableSounds.Clear();

        }


        public void PlaySoundOnCamera(string soundId)
        {
            var source = BuildSource(soundId);
            PlayOnObject(source, listener.transform);
        }

        public void PlaySoundOnCamera(AudioClip clip)
        {
            var source = BuildSource(clip);
            PlayOnObject(source, listener.transform);
        }


        public void PlaySoundOnObject(string soundId, Transform tr)
        {
            var source = BuildSource(soundId);
            PlayOnObject(source, tr);
        }

        public void PlaySoundOnObject(AudioClip clip, Transform tr)
        {
            var source = BuildSource(clip);
            PlayOnObject(source, tr);
        }


        public void PlaySoundInPosition(string soundId, Vector3 position)
        {
            var source = BuildSource(soundId);
            PlayInPosition(source, position);
        }

        public void PlaySoundInPosition(AudioClip clip, Vector3 position)
        {
            var source = BuildSource(clip);
            PlayInPosition(source, position);
        }



        private AudioSource BuildSource(string soundId)
        {
            var sound = sounds.Get(soundId).GetRandomItem();
            var source = pool.SpawnObject();

            source.clip = sound.Clip;
            source.loop = loopSound;
            source.outputAudioMixerGroup = mixerGroup;
            source.pitch = sound.BasePitch + Random.Range(0f, sound.PitchRandom);

            if (fadeInTime > 0)
            {
                source.volume = 0.01f;
                StartCoroutine(SetVolume(source, sound.ClipVolume, fadeInTime));
            }
            else
            {
                source.volume = sound.ClipVolume;
            }

            return source;
        }

        private AudioSource BuildSource(AudioClip clip)
        {
            var source = pool.SpawnObject();

            source.clip = clip;
            source.loop = loopSound;
            source.outputAudioMixerGroup = mixerGroup;
            source.pitch = 1f;

            if (fadeInTime > 0)
            {
                source.volume = 0.01f;
                StartCoroutine(SetVolume(source, 1f, fadeInTime));
            }
            else
            {
                source.volume = 1f;
            }

            return source;
        }


        private void PlayOnObject(AudioSource source, Transform parent)
        {
            RestrictSoundsCount();
            var tr = source.transform;
            tr.SetParent(parent);
            tr.localPosition = Vector3.zero;

            _playingSounds.AddLast(source);
            source.Play();
        }

        private void PlayInPosition(AudioSource source, Vector3 position)
        {
            RestrictSoundsCount();
            var tr = source.transform;
            tr.SetParent(null);
            tr.position = position;

            _playingSounds.AddLast(source);
            source.Play();
        }

        private void RestrictSoundsCount()
        {
            if (_playingSounds.Count < maxSounds)
                return;

            var first = _playingSounds.First;
            var last = _playingSounds.Last;

            first.SwapWith(last);
            if (fadeOutTime > 0)
            {
                StartCoroutine(SetVolume(first.Value, 0f, fadeOutTime, true));
            }

            else
            {
                first.Value.volume = 0f;
            }
        }



        private IEnumerator SetVolume(AudioSource source, float targetVolume, float fadingDuration, bool killOnEnd = false)
        {
            var iteration = new WaitForEndOfFrame();
            var delta = Mathf.Abs(targetVolume - source.volume);
            var stepValue = delta / fadingDuration;


            while (!Mathf.Approximately(source.volume, targetVolume))
            {
                source.volume = Mathf.MoveTowards(source.volume,
                    targetVolume, stepValue * Time.unscaledDeltaTime);
                yield return iteration;
            }

            if (killOnEnd)
            {
                source.Stop();
                _removableSounds.Add(source);
            }
        }
    }
}

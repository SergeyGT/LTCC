using System;
using System.Collections.Generic;
using __Scripts.System;
using UnityEngine;
using EventReference = FMODUnity.EventReference;
using Random = System.Random;

namespace __Scripts.AnimationsController
{
    public class AnimationController : MonoBehaviour
    {
        public List<Sound> audioClips;

        public void Shot(string clipName)
        {
            foreach (Sound clip in audioClips)
            {
                if (clip.nameOfSound == clipName)
                {
                    Random rnd = new Random();
                    int indexClip = rnd.Next(clip.clips.Count);
                    AudioManager.Instance.PlayOneShot(clip.clips[indexClip]);
                }
            }
        }
    }

    [Serializable]
    public class Sound
    {
        public string nameOfSound;
        public List<EventReference> clips;
        public Surface surface;
        public SoundType type;
        [Range(0,2)] public float pitch;
    }

    [Serializable]
    public enum Surface
    {
        Wood,
        Metal,
        Water,
        Tile
    }

    [Serializable]
    public enum SoundType
    {
        Walk,
        Jump,
        Interact,
        Use
    }
}
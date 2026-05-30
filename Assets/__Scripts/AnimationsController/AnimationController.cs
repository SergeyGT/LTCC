using System;
using System.Collections.Generic;
using __Scripts.System;
using UnityEngine;

namespace __Scripts.AnimationsController
{
    [Serializable]
    public class AnimationController 
    {
        public SerializableDictionary<TypeSound, AudioClip> audioClips;
    }

    [Serializable]
    public enum TypeSound
    {
        Walk,
        Interact
    }
}
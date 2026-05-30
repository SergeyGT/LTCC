using System;
using __Scripts.System;
using EventReference = FMODUnity.EventReference;

namespace __Scripts.AnimationsController
{
    [Serializable]
    public class AnimationController 
    {
        public SerializableDictionary<TypeSound, EventReference> audioClips;

        public void OneFoot()
        {
            //EventReference eventReference = audioClips[TypeSound.Walk];
        }
    }

    [Serializable]
    public enum TypeSound
    {
        Walk,
        Interact
    }
}
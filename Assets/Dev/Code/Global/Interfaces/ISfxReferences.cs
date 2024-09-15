using UnityEngine;

namespace Assets.Global
{
    public interface ISfxReferences
    {
        public AudioSource SfxSource { get; }
        public AudioClip[] GlitchFx { get; }
        public AudioClip[] SavedFx { get; }
    }
}
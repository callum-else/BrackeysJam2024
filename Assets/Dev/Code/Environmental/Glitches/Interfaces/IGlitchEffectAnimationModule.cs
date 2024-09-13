namespace Assets.Environmental
{
    public interface IGlitchEffectAnimationModule
    {
        public void AnimateSpawn(int scaleDelta);
        public void ApplyScaleDelta(int delta);
    }
}
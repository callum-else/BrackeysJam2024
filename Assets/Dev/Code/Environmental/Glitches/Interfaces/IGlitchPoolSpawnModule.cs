namespace Assets.Environmental
{
    public interface IGlitchPoolSpawnModule
    {
        void SpawnRandomGlitch(int value = 0, float? minDistOverride = null, float? maxDistOverride = null);
    }
}
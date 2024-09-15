namespace Assets.Effects
{
    public interface IDigitalGlitch
    {
        float ColorIntensity { get; set; }
        float FlipIntensity { get; set; }
        float Intensity { get; set; }
        bool ShowGlitch { get; set; }
    }
}
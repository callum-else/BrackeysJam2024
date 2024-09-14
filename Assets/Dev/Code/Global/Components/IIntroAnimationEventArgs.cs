namespace Assets.Global
{
    public interface IIntroAnimationEventArgs
    {
        GlobalIntroAnimationPhase Phase { get; }
        double StartTime { get; }
    }
}
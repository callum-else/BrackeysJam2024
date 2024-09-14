namespace Assets.Global
{
    public class IntroAnimationEventArgs : IIntroAnimationEventArgs
    {
        public GlobalIntroAnimationPhase Phase { get; set; }
        public double StartTime { get; set; }

        public IntroAnimationEventArgs(GlobalIntroAnimationPhase phase, double startTime)
        {
            Phase = phase;
            StartTime = startTime;
        }
    }
}
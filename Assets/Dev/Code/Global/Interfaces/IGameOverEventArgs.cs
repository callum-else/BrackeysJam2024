namespace Assets.Global
{
    public interface IGameOverEventArgs
    {
        float TimeSurvived { get; }
        int SoulsSaved { get; }
        int SoulsLost { get; }
    }
}
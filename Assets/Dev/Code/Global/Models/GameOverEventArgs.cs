namespace Assets.Global
{
    public class GameOverEventArgs : IGameOverEventArgs
    {
        public float TimeSurvived { get; set; }
        public int SoulsSaved { get; set; }
        public int SoulsLost { get; set; }
    }
}
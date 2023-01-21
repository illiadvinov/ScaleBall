using CodeBase.Obstacles;
using CodeBase.Player;
using Zenject;

namespace CodeBase
{
    public class LevelManager
    {
        private readonly TapScaling tapScaling;
        private readonly ObstaclesManager obstaclesManager;

        [Inject]
        public LevelManager(TapScaling tapScaling, ObstaclesManager obstaclesManager)
        {
            this.tapScaling = tapScaling;
            this.obstaclesManager = obstaclesManager;
        }

        public void SubscribeToEvent() =>
            obstaclesManager.OnWin += Win;

        public void UnsubscribeFromEvent() =>
            obstaclesManager.OnWin -= Win;

        private void Win() =>
            tapScaling.MoveBallToFinish();
    }
}
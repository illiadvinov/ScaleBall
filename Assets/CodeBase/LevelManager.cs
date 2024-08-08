using System.Net.Mime;
using CodeBase.Obstacles;
using CodeBase.Player;
using UnityEngine;
using Zenject;

namespace CodeBase
{
    public class LevelManager
    {
        private readonly TapScaling tapScaling;
        private readonly ObstaclesManager obstaclesManager;
        private readonly GameObject restartCanvas;

        [Inject]
        public LevelManager(TapScaling tapScaling, ObstaclesManager obstaclesManager, [Inject(Id = "RestartCanvas")] GameObject restartCanvas)
        {
            this.tapScaling = tapScaling;
            this.obstaclesManager = obstaclesManager;
            this.restartCanvas = restartCanvas;
        }

        public void SubscribeToEvent()
        {
            tapScaling.OnWin += Win;
            tapScaling.OnLose += Lose;
        }

        public void UnsubscribeFromEvent()
        {
            tapScaling.OnWin -= Win;
            tapScaling.OnLose -= Lose;
        }

        private void Win()
        {
            tapScaling.MoveBallToFinish(() => restartCanvas.SetActive(true));
        }

        private void Lose()
        {
            restartCanvas.SetActive(true);
        }
    }
}
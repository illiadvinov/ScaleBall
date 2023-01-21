using System;
using CodeBase.Player;
using UnityEngine;
using Zenject;

namespace CodeBase.Obstacles
{
    public class ObstaclesManager
    {
        public event Action OnWin;
        private readonly Transform obstaclesController;
        private readonly ShootSphere shootSphere;
        private int amountOfObstacles;
        private int disabledObstacles;

        [Inject]
        public ObstaclesManager(ShootSphere shootSphere,[Inject(Id = "ObstaclesController")] Transform obstaclesController)
        {
            this.shootSphere = shootSphere;
            this.obstaclesController = obstaclesController;
        }

        public void Initialize()
        {
            Reset();
        }

        public void SubscribeToEvent() =>
            shootSphere.OnHit += Hit;

        public void UnsubscribeFromEvent() =>
            shootSphere.OnHit -= Hit;

        private void Hit(Collider[] obstacles)
        {
            foreach (Collider obstacle in obstacles)
            {
                if (obstacle.CompareTag("Finish"))
                    continue;

                obstacle.gameObject.SetActive(false);
                disabledObstacles++;
                if (disabledObstacles >= amountOfObstacles)
                {
                    Debug.Log("You won");
                    OnWin?.Invoke();
                }
            }
        }

        private void Reset()
        {
            amountOfObstacles = obstaclesController.childCount;
            disabledObstacles = 0;
        }
    }
}
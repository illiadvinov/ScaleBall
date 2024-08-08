using CodeBase.Obstacles;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers
{
    public class SceneBootstrap : MonoBehaviour
    {
        private  LevelManager levelManager;
        private ObstaclesManager obstaclesManager;

        [Inject]
        public void Construct(LevelManager levelManager, ObstaclesManager obstaclesManager)
        {
            this.levelManager = levelManager;
            this.obstaclesManager = obstaclesManager;
        }

        private void Start()
        {
            EventsSubscription();
        }

        private void OnDestroy()
        {
            EventUnsubscription();
        }

        private void EventsSubscription()
        {
            levelManager.SubscribeToEvent();
            obstaclesManager.SubscribeToEvent();
        }

        private void EventUnsubscription()
        {
            levelManager.UnsubscribeFromEvent();
            obstaclesManager.UnsubscribeFromEvent();
        }
    }
}
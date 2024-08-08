using CodeBase.Obstacles;
using CodeBase.Player;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private ShootSphere shootSphere;
        [SerializeField] private TapScaling tapScaling;
        [SerializeField] private ObstaclesManager obstaclesManager;
        [SerializeField] private GameObject restartCanvas;

        public override void InstallBindings()
        {
            BindMonoBehaviours();
            BindServices();
            BindGameObjects();
        }

        private void BindGameObjects()
        {
            Container.Bind<GameObject>().WithId("RestartCanvas").FromInstance(restartCanvas).AsSingle();
        }


        private void BindServices()
        {
            Container.Bind<LevelManager>().AsSingle();
        }

        private void BindMonoBehaviours()
        {
            Container.Bind<ShootSphere>().FromInstance(shootSphere).AsSingle();
            Container.Bind<TapScaling>().FromInstance(tapScaling).AsSingle();
            Container.Bind<ObstaclesManager>().FromInstance(obstaclesManager).AsSingle();
        }
    }
}
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
        [SerializeField] private Transform obstaclesController;

        public override void InstallBindings()
        {
            BindMonoBehaviours();
            BindServices();
            BindTransforms();
        }

        private void BindTransforms()
        {
            Container.Bind<Transform>().WithId("ObstaclesController").FromInstance(obstaclesController).AsSingle();
        }

        private void BindServices()
        {
            Container.Bind<LevelManager>().AsSingle();
            Container.Bind<ObstaclesManager>().AsSingle();
        }

        private void BindMonoBehaviours()
        {
            Container.Bind<ShootSphere>().FromInstance(shootSphere).AsSingle();
            Container.Bind<TapScaling>().FromInstance(tapScaling).AsSingle();
        }
    }
}
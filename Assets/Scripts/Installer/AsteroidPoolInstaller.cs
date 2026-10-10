using Enemies;
using UnityEngine;
using Zenject;

namespace Installer
{
    public class AsteroidPoolInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _asteroidPrefab;
        [SerializeField] private Transform _asteroidRoot;
        [SerializeField] private int _capacity = 10;

        public override void InstallBindings()
        {
            Container
                .Bind<AsteroidPool>()
                .AsSingle()
                .WithArguments(_asteroidPrefab, _asteroidRoot, _capacity)
                .NonLazy();
        }
    }
}
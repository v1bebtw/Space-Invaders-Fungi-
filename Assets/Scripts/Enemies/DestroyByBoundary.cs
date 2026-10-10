using System;
using UnityEngine;
using Zenject;

namespace Enemies
{
    public class DestroyByBoundary : MonoBehaviour
    {
        private AsteroidPool _asteroidPool;

        [Inject]
        public void Construct(AsteroidPool asteroidPool)
        {
            _asteroidPool = asteroidPool;
        }

        private void OnTriggerExit(Collider other)
        {
            _asteroidPool.Return(new PooledObject(other.gameObject));
        }
    }
}
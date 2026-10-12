using Enemies;
using UnityEngine;

namespace Pool
{
    public class AsteroidPool : MonoBehaviour, IPool<Asteroid>
    {
        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private int _prewarmCount = 10;
        [SerializeField] private int _maxSize = 100;
        [SerializeField] private Transform _container;

        private PooledObject<Asteroid> _pool;

        private void Awake()
        {
            if (!_container)
                _container = transform;

            _pool = new PooledObject<Asteroid>(
                () => CreateAsteroid(),
                asteroid => Destroy(asteroid.gameObject),
                _prewarmCount,
                _maxSize);
        }

        public Asteroid Get()
        {
            return Get(Vector3.zero, Quaternion.identity);
        }

        public Asteroid Get(Vector3 position, Quaternion rotation)
        {
            Asteroid asteroid = _pool.Get();
            asteroid.transform.SetPositionAndRotation(position, rotation);
            asteroid.gameObject.SetActive(true);
            return asteroid;
        }

        public void Release(Asteroid asteroid)
        {
            _pool.Release(asteroid);
        }

        private Asteroid CreateAsteroid()
        {
            Asteroid asteroid = Instantiate(_asteroidPrefab, _container);
            asteroid.OwnerPool = this;
            return asteroid;
        }

        private void OnDestroy()
        {
            _pool?.ReleaseAll();
        }
    }
}

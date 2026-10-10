using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class PooledObject
    {
        public GameObject GameObject { get; private set; }
    
        public PooledObject(GameObject gameObject)
        {
            GameObject = gameObject;
        }
    }

    public class AsteroidPool
    {
        private readonly GameObject _asteroidPrefab;
        private readonly Transform _asteroidRoot;
        private readonly Queue<PooledObject> _pool = new();
    
        public AsteroidPool(GameObject asteroidPrefab, Transform asteroidRoot, int capacity)
        {
            _asteroidPrefab = asteroidPrefab;
            _asteroidRoot = asteroidRoot;

            for (int i = 0; i < capacity; i++)
            {
                var obj = Object.Instantiate(asteroidPrefab, asteroidRoot);
                obj.SetActive(false);
                _pool.Enqueue(new PooledObject(obj));
            }
        }

        public PooledObject Rent(Transform parent)
        {
            PooledObject pooled = _pool.Count > 0 
                ? _pool.Dequeue() 
                : new PooledObject(Object.Instantiate(_asteroidPrefab));
             
            pooled.GameObject.transform.SetParent(parent, false);
            pooled.GameObject.SetActive(true);
            return pooled;
        }

        public void Return(PooledObject pooled)
        {
            pooled.GameObject.SetActive(false);
            pooled.GameObject.transform.SetParent(_asteroidRoot, false);
            _pool.Enqueue(pooled);
        }
    }
}
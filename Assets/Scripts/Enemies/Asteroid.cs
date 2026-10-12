using Pool;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    public class Asteroid : MonoBehaviour, IPoolable
    {
        private AsteroidPool _ownerPool;

        public AsteroidPool OwnerPool
        {
            get => _ownerPool;
            set => _ownerPool = value;
        }

        public void OnSpawned()
        {
        }

        public void OnDespawned()
        {
            if (TryGetComponent(out Rigidbody rb))
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            gameObject.SetActive(false);
        }

        public void ReleaseToPool()
        {
            if (_ownerPool != null)
                _ownerPool.Release(this);
            else
                Destroy(gameObject);
        }
    }
}

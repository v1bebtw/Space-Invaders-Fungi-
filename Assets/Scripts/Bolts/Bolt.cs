using Pool;
using UnityEngine;

namespace Bolts
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bolt : MonoBehaviour, IPoolable
    {
        [SerializeField] private float _speed = 20f;
        [SerializeField] private float _lifeTime = 3f;

        private Rigidbody _rb;
        private float _lifeTimer;
        private BoltPool _ownerPool;

        public BoltPool OwnerPool
        {
            get => _ownerPool;
            set => _ownerPool = value;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _lifeTimer -= Time.deltaTime;
            if (_lifeTimer <= 0f)
                ReleaseToPool();
        }

        public void OnSpawned()
        {
            _lifeTimer = _lifeTime;
        }

        public void OnDespawned()
        {
            _rb.linearVelocity = Vector3.zero;
            gameObject.SetActive(false);
        }

        public void Launch()
        {
            _rb.linearVelocity = transform.forward * _speed;
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

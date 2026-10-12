using Bolts;
using UnityEngine;

namespace Pool
{
    public class BoltPool : MonoBehaviour, IPool<Bolt>
    {
        [SerializeField] private Bolt _boltPrefab;
        [SerializeField] private int _prewarmCount = 20;
        [SerializeField] private int _maxSize = 100;
        [SerializeField] private Transform _container;

        private PooledObject<Bolt> _pool;

        private void Awake()
        {
            if (!_container)
                _container = transform;

            _pool = new PooledObject<Bolt>(
                () => CreateBolt(),
                bolt => Destroy(bolt.gameObject),
                _prewarmCount,
                _maxSize);
        }

        public Bolt Get()
        {
            return Get(Vector3.zero, Quaternion.identity);
        }

        public Bolt Get(Vector3 position, Quaternion rotation)
        {
            Bolt bolt = _pool.Get();
            bolt.transform.SetPositionAndRotation(position, rotation);
            bolt.gameObject.SetActive(true);
            bolt.Launch();
            return bolt;
        }

        public void Release(Bolt bolt)
        {
            _pool.Release(bolt);
        }

        private Bolt CreateBolt()
        {
            Bolt bolt = Instantiate(_boltPrefab, _container);
            bolt.OwnerPool = this;
            return bolt;
        }

        private void OnDestroy()
        {
            _pool?.ReleaseAll();
        }
    }
}

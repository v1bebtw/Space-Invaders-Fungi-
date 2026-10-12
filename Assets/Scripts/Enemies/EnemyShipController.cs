using System.Collections;
using Bolts;
using Player;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyShipController : MonoBehaviour
    {
        [SerializeField] private Boundary _boundary;
        [SerializeField] private Vector2 _startWait;
        [SerializeField] private Vector2 _maneuverTime;
        [SerializeField] private float _dodge;
        [SerializeField] private float _smoothing;
        [SerializeField] private float _tilt;
        [SerializeField] private float _speed;

        [SerializeField] private Bolt _boltPrefab;
        [SerializeField] private Transform _shotSpawn;
        [SerializeField] private float _fireRate = 1.5f;
        [SerializeField] private float _fireDelay = 1f;

        private Rigidbody _rb;
        private Quaternion _startRotation;
        private float _targetManeuver;
        private float _nextFire;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _startRotation = _rb.rotation;
        }

        private void Start()
        {
            _nextFire = Time.time + _fireDelay;
            StartCoroutine(Evade());
        }

        private void Update()
        {
            if (_shotSpawn == null || _boltPrefab == null || Time.time < _nextFire)
                return;

            _nextFire = Time.time + _fireRate;
            Bolt bolt = Instantiate(_boltPrefab, _shotSpawn.position, _shotSpawn.rotation);
            bolt.OnSpawned();
            bolt.Launch();
        }

        private IEnumerator Evade()
        {
            yield return new WaitForSeconds(Random.Range(_startWait.x, _startWait.y));

            while (true)
            {
                _targetManeuver = Random.Range(1, _dodge) * -Mathf.Sign(transform.position.x);
                yield return new WaitForSeconds(Random.Range(_maneuverTime.x, _maneuverTime.y));
                _targetManeuver = 0;
                yield return new WaitForSeconds(Random.Range(_maneuverTime.x, _maneuverTime.y));
            }
        }

        private void FixedUpdate()
        {
            float newManeuver = Mathf.MoveTowards(
                _rb.linearVelocity.x,
                _targetManeuver,
                _smoothing * Time.fixedDeltaTime);

            _rb.linearVelocity = new Vector3(newManeuver, 0f, _speed);

            _rb.position = new Vector3(
                Mathf.Clamp(_rb.position.x, _boundary.xMin, _boundary.xMax),
                0f,
                Mathf.Clamp(_rb.position.z, _boundary.zMin, _boundary.zMax)
            );

            _rb.rotation = _startRotation * Quaternion.Euler(0f, 0f, _rb.linearVelocity.x * -_tilt);
        }
    }
}

using Pool;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private BoltPool _boltPool;
        [SerializeField] private Transform _shotSpawn;
        [SerializeField] private float _fireRate;
        [SerializeField] private PlayerAudio _audio;

        private float _nextFire;
        private InputAction _shotAction;

        private void Awake()
        {
            _shotAction = GetComponent<PlayerInput>().actions["Attack"];
        }

        private void Update()
        {
            if (!_shotAction.triggered || Time.time < _nextFire)
                return;

            _nextFire = Time.time + _fireRate;
            _boltPool.Get(_shotSpawn.position, _shotSpawn.rotation);
            _audio.PlayShot();
        }
    }
}

using Pool;
using Bolts;
using UnityEngine;

namespace Enemies
{
    public class DestroyByContact : MonoBehaviour
    {
        [SerializeField] private GameObject _explosion;
        [SerializeField] private GameObject _playerExplosion;
        public int scorePoint;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Boundary"))
                return;

            if (_explosion)
                Instantiate(_explosion, transform.position, transform.rotation);

            if (other.CompareTag("Player"))
            {
                if (_playerExplosion)
                    Instantiate(_playerExplosion, other.transform.position, other.transform.rotation);

                Destroy(other.gameObject);
            }
            else if (other.GetComponentInParent<Bolt>() is { } bolt)
            {
                bolt.ReleaseToPool();
            }
            else if (other.GetComponentInParent<Asteroid>() is { } otherAsteroid)
            {
                otherAsteroid.ReleaseToPool();
            }
            else
            {
                Destroy(other.gameObject);
            }

            if (TryGetComponent(out Asteroid asteroid))
                asteroid.ReleaseToPool();
            else
                Destroy(gameObject);
        }
    }
}

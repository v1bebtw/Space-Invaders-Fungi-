using System.Collections;
using Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class AsteroidSpawner : MonoBehaviour
    {
        [SerializeField] private AsteroidPool _pool;
        [SerializeField] private float _startWait = 1f;
        [SerializeField] private float _spawnWait = 0.5f;
        [SerializeField] private float _waveWait = 2f;
        [SerializeField] private int _count = 5;
        [SerializeField] private float _xRange = 6f;
        [SerializeField] private float _spawnZ = 16f;
        [SerializeField] private Vector3 _spawnEuler = new(0f, 180f, 0f);

        private void Start()
        {
            StartCoroutine(SpawnWaves());
        }

        private IEnumerator SpawnWaves()
        {
            yield return new WaitForSeconds(_startWait);

            while (true)
            {
                for (int i = 0; i < _count; i++)
                {
                    Vector3 position = new Vector3(Random.Range(-_xRange, _xRange), 0f, _spawnZ);
                    _pool.Get(position, Quaternion.Euler(_spawnEuler));
                    yield return new WaitForSeconds(_spawnWait);
                }

                yield return new WaitForSeconds(_waveWait);
            }
        }
    }
}

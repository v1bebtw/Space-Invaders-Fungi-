using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;

        private void OnEnable()
        {
            GetComponent<Rigidbody>().linearVelocity = transform.forward * Random.Range(_minSpeed, _maxSpeed);
        }
    }
}
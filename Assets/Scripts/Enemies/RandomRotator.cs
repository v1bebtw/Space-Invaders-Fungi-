using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class RandomRotator : MonoBehaviour
    {
        [SerializeField] private float _tumble;

        private void Start()
        {
            GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * _tumble;
        }
    }
}
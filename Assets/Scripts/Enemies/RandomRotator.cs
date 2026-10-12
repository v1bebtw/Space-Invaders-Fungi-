using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class RandomRotator : MonoBehaviour
    {
        [SerializeField] private float _tumble;

        private void OnEnable()
        {
            GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * _tumble;
        }
    }
}
using System;
using UnityEngine;

namespace Background
{
    public class BGScroller : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _tileSizeZ;
        
        private Vector3 _startPosition;

        private void Start()
        {
            _startPosition = transform.position;
            Debug.Log(transform.position);
        }

        private void Update()
        {
            float newPosition = Mathf.Repeat(Time.time * _speed, _tileSizeZ);
            transform.position = _startPosition + Vector3.forward * newPosition;
        }
    }
}
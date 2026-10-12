using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _tilt;
        [SerializeField] private Boundary _boundary;

        private Rigidbody _rb;
        private InputAction _moveAction;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _moveAction = GetComponent<PlayerInput>().actions["Move"];
        }

        private void FixedUpdate()
        {
            Vector2 move = _moveAction.ReadValue<Vector2>();

            Vector3 movement = new Vector3(move.x, 0.0f, move.y);
            _rb.linearVelocity = movement * _speed;

            _rb.position = new Vector3(
                Mathf.Clamp(_rb.position.x, _boundary.xMin, _boundary.xMax),
                0.0f,
                Mathf.Clamp(_rb.position.z, _boundary.zMin, _boundary.zMax)
            );

            _rb.rotation = Quaternion.Euler(0.0f, 0.0f, _rb.linearVelocity.x * -_tilt);
        }
    }

    [Serializable]
    public class Boundary
    {
        public float xMin, xMax, zMin, zMax;
    }
}

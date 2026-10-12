using Pool;
using Bolts;
using UnityEngine;

namespace Enemies
{
    public class DestroyByBoundary : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponentInParent<Bolt>() is { } bolt)
                bolt.ReleaseToPool();
            else if (other.GetComponentInParent<Asteroid>() is { } asteroid)
                asteroid.ReleaseToPool();
        }
    }
}

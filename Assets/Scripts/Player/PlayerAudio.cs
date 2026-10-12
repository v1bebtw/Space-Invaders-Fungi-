using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerAudio : MonoBehaviour
    {
        private AudioSource _shotAudio;

        private void Awake()
        {
            _shotAudio = GetComponent<AudioSource>();
        }

        public void PlayShot()
        {
            _shotAudio.Play();
        }
    }
}

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class GameControllerTween : MonoBehaviour
    {
        [SerializeField] private GameObject _button;

        private void Start()
        {
            _button.transform.DOMove(new Vector3(2, 3, 0), 1);
        }
    }
}
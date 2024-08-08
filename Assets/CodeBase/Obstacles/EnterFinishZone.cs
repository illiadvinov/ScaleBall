using DG.Tweening;
using UnityEngine;

namespace CodeBase.Obstacles
{
    public class EnterFinishZone : MonoBehaviour
    {
        [SerializeField] private Transform door;

        void Start()
        {
            Reset();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Player"))
                door.DOScaleY(0, .5f).SetEase(Ease.Linear);
        }

        private void Reset()
        {
            door.localScale = Vector3.one;
        }
    }
}
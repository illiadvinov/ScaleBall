using System;
using System.Collections;
using CodeBase.Obstacles;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Player
{
    public class ShootSphere : MonoBehaviour
    {
        private const string obstaclesLayerName = "Obstacles";
        private readonly Vector3 shootingPosition = new Vector3(0, 0, 1.7f);
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private Collider collider;
        private int obstaclesLayer;
        private float radius;
        private Action callback;
        private TapScaling tapScaling;
        public event Action<Collider[], Action> OnHit;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            Reset();
            gameObject.SetActive(false);
            obstaclesLayer = 1 << LayerMask.NameToLayer(obstaclesLayerName);
        }

        [Inject]
        public void Construct(TapScaling tapScaling)
        {
            this.tapScaling = tapScaling;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log(collision.gameObject.name);
            if (collision.gameObject.tag.Equals("Finish"))
            {
                tapScaling.Lose();
                return;
            }

            radius = transform.localScale.x * 2;
            Collider[] hits = Physics.OverlapSphere(transform.position, radius, obstaclesLayer);
            OnHit?.Invoke(hits, Reset);
            gameObject.SetActive(false);
        }

        public void EnableShootSphere()
        {
            transform.localScale = new Vector3(.1f, .1f, .1f);
            gameObject.SetActive(true);
        }

        public void Shoot(Action callback)
        {
            rigidbody.isKinematic = false;
            this.callback = callback;
            gameObject.SetActive(true);
            tapScaling.enabled = false;
            rigidbody.AddForce(new Vector3(0, 0, 20), ForceMode.Impulse);
        }

        public void IncreaseScale()
        {
            transform.localScale += StaticInfo.IncreaseAmount;
        }

        public void Reset()
        {
            rigidbody.isKinematic = true;
            radius = transform.localScale.x;
            transform.position = shootingPosition;
            gameObject.SetActive(false);
            callback?.Invoke();
        }
    }
}
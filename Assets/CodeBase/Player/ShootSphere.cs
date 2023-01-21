using System;
using UnityEngine;

namespace CodeBase.Player
{
    public class ShootSphere : MonoBehaviour
    {
        private const string obstaclesLayerName = "Obstacles";
        private int obstaclesLayer;
        private float radius;
        private new Rigidbody rigidbody;
        private Action callback;
        public event Action<Collider[]> OnHit;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            Reset();
            obstaclesLayer = 1 << LayerMask.NameToLayer(obstaclesLayerName);
        }

        private void OnCollisionEnter(Collision collision)
        {
            radius = transform.localScale.x * 2;
            Collider[] hits = Physics.OverlapSphere(transform.position, radius, obstaclesLayer);
            PhysicsDebug.DrawRays(transform.position, radius, 10f);
            Reset();
            OnHit?.Invoke(hits);
            Debug.Log(hits.Length);
        }

        public void EnableShootSphere()
        {
            transform.localScale = new Vector3(.05f, .05f, .05f);
            gameObject.SetActive(true);
        }
        
        public void SetForce(Action callback)
        {
            rigidbody.isKinematic = false;
            this.callback = callback;
            rigidbody.AddForce(new Vector3(0, 0, 20), ForceMode.Impulse);
        }

        public void IncreaseScale()
        {
            transform.localScale += StaticInfo.IncreaseAmount;
        }

        private void Reset()
        {
            gameObject.SetActive(false);
            rigidbody.isKinematic = true;
            transform.position = new Vector3(0, 0, 1.7f);
            radius = transform.localScale.x;
            callback?.Invoke();
        }
    }
}
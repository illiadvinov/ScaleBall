using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Player
{
    public class TapScaling : MonoBehaviour
    {
        private string obstaclesLayerName = "Obstacles";
        public event Action OnWin;
        public event Action OnLose;
        [SerializeField] private Transform playerSphere;
        [SerializeField] private ShootSphere shootSphere;
        [SerializeField] private Transform finish;
        [SerializeField] private Collider playerCollider;
        [SerializeField] private Collider ballCollider;
        private readonly Collider[] collision = new Collider[1];
        private bool isHeld;
        private bool canShoot;
        private int obstaclesLayer;
        private int collisionCount;

        void Start()
        {
            canShoot = true;
            obstaclesLayer = 1 << LayerMask.NameToLayer(obstaclesLayerName);
        }

        private void OnMouseDown()
        {
            if (!enabled)
                return;

            if (canShoot)
            {
                isHeld = true;
                shootSphere.EnableShootSphere();
                StartCoroutine(Scale());
            }
        }

        private void OnMouseUp()
        {
            if (!enabled)
                return;
            if (!isHeld)
                DecreaseScale();
            StopScaling();
            canShoot = false;
            shootSphere.Shoot(() =>
            {
                canShoot = true;
                CheckCollision();
            });
        }

        public Tween MoveBallToFinish(Action callback = null)
        {
            canShoot = false;
            return playerCollider.transform.DOJump(finish.position, 1f, (int) (finish.position.z - transform.position.z) / 2, 3f)
                .SetEase(Ease.Linear).OnComplete(() => callback?.Invoke());
        }

        private void StopScaling()
        {
            isHeld = false;
            StopCoroutine(Scale());
        }

        private void CheckCollision()
        {
            int size = Physics.OverlapBoxNonAlloc(transform.position, ballCollider.bounds.size / 2, collision, Quaternion.identity, obstaclesLayer);
            if (size > 0)
            {
                enabled = true;
            }
            else
            {
                playerCollider.enabled = true;
                enabled = false;
                OnWin?.Invoke();
            }
        }

        private IEnumerator Scale()
        {
            while (isHeld)
            {
                if (IsSmall())
                {
                    StopScaling();
                    shootSphere.Shoot(Lose);
                }

                DecreaseScale();
                shootSphere.IncreaseScale();
                yield return null;
            }
        }

        public void Lose()
        {
            enabled = false;
            OnLose?.Invoke();
        }


        private void DecreaseScale() =>
            playerSphere.localScale -= StaticInfo.DecreaseAmount;

        private bool IsSmall()
        {
            Vector3 localScale = playerSphere.localScale;
            return localScale.x < StaticInfo.MinimumShootBallScale ||
                   localScale.y < StaticInfo.MinimumShootBallScale ||
                   localScale.z < StaticInfo.MinimumShootBallScale;
        }
    }
}
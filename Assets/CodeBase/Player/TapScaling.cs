using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Player
{
    public class TapScaling : MonoBehaviour
    {
        [SerializeField] private Transform playerSphere;
        [SerializeField] private ShootSphere shootSphere;
        [SerializeField] private Transform finish;

        private bool isHeld;

        private bool canShoot;

        void Start()
        {
            canShoot = true;
        }

        private void OnMouseDown()
        {
            if (!enabled)
                return;

            isHeld = true;
            if (canShoot)
            {
                canShoot = false;
                shootSphere.EnableShootSphere();
                StartCoroutine(Scale());
            }
        }


        private void OnMouseUp()
        {
            if (enabled)
                StopScaling();
        }

        public Tween MoveBallToFinish()
        {
            canShoot = false;
            return transform.DOJump(finish.position, 1f, (int) (finish.position.z - transform.position.z) / 2, 3f)
                .SetEase(Ease.Linear);
        }

        private void StopScaling()
        {
            isHeld = false;
            StopCoroutine(Scale());
            shootSphere.SetForce(() => canShoot = true);
        }

        private IEnumerator Scale()
        {
            while (isHeld)
            {
                if (IsSmall())
                {
                    StopScaling();
                    shootSphere.gameObject.SetActive(false);
                    Application.Quit();
                }

                DecreaseScale();
                shootSphere.IncreaseScale();
                yield return null;
            }
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
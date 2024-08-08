using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Player;
using UnityEngine;
using Zenject;

namespace CodeBase.Obstacles
{
    public class ObstaclesManager : MonoBehaviour
    {
        private ShootSphere shootSphere;


        [Inject]
        public void Construct(ShootSphere shootSphere)
        {
            this.shootSphere = shootSphere;
        }

        public void SubscribeToEvent() =>
            shootSphere.OnHit += Hit;

        public void UnsubscribeFromEvent() =>
            shootSphere.OnHit -= Hit;

        private void Hit(Collider[] obstacles, Action callback)
        {
            StartCoroutine(HitCoroutine(obstacles, callback));
        }

        private IEnumerator HitCoroutine(Collider[] obstacles, Action callback)
        {
            foreach (Collider obstacle in obstacles)
            {
                if (obstacle.CompareTag("Finish"))
                    continue;

                obstacle.GetComponent<Renderer>().material.color = Color.yellow;
            }
            yield return new WaitForSeconds(0.25f);
            foreach (Collider obstacle in obstacles)
            {
                if (obstacle.CompareTag("Finish"))
                    continue;

                obstacle.gameObject.SetActive(false);
            }

            callback?.Invoke();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    public class ReflectOnHitSurfaceModifier : BulletModifier
    {
        [SerializeField] private AudioClip onReflectSound;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (bullet.IsDestroyed)
                return;

            if (collider.GetComponent<IEntity>() != null || collider.GetComponentInParent<IEntity>() != null)
                return;

            if (collider.GetComponent<WallModifier>() != null || collider.GetComponentInParent<WallModifier>() != null)
                return;

            if (collider.isTrigger)
                return;

            //Find the closest point between the bullet and the wall and get the directinal vector between them
            var closestPoint = (collider.ClosestPoint(bullet.transform.position));
            var dir = closestPoint - (Vector2)bullet.transform.position;

            //The only reason we are doing raycast is to use its .normal function that comes with raycasthit.
            //TODO: Find a less aids way.
            RaycastHit2D hit;
            if (hit = Physics2D.Raycast(bullet.transform.position, dir, 1))
            {
                var normal = hit.normal;
                bullet.Velocity = normal * bullet.Velocity.magnitude;

                SoundManager.PlayAudioClipAtPoint(onReflectSound, transform.position);
            }

        }

    }
}
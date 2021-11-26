using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpellcastStudios;

namespace SpellcastStudios
{
    public class BouncyWall : WallModifier
    {
        protected override void Affect(Bullet bullet)
        {
            //Find the closest point between the bullet and the wall and get the directinal vector between them
            var closestPoint = (GetComponent<Collider2D>().ClosestPoint(bullet.transform.position));
            var dir = closestPoint - (Vector2)bullet.transform.position;

            //The only reason we are doing raycast is to use its .normal function that comes with raycasthit.
            //TODO: Find a less aids way.
            RaycastHit2D hit;
            if (hit = Physics2D.Raycast(bullet.transform.position, dir, 1))
            {
                var normal = hit.normal;
                var bulletDirection = bullet.Velocity.normalized;
                var w = 2 * (bulletDirection * normal) * normal - bulletDirection;
                bullet.Velocity = -w * bullet.Velocity.magnitude;
            }
        }

    }
}
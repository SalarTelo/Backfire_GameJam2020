using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellcastStudios
{
    public class DuplicatingWall : WallModifier
    {
        [SerializeField] private float hitAllowanceDelay = 0.2f;
        [SerializeField] private PositionalVisualEffect visualOnDuplicate;

        /// <summary>
        /// Holds the last time a specific bullet instance was hit. Used to ensure infinite bullets don't happen :)
        /// </summary>
        /// 
        private Dictionary<Bullet, float> lastHitTimes = new Dictionary<Bullet, float>();

        protected override void Affect(Bullet bullet)
        {
            if (lastHitTimes.ContainsKey(bullet) && lastHitTimes[bullet] + hitAllowanceDelay > Time.time)
                return;

            var closestPoint = (GetComponent<Collider2D>().ClosestPoint(bullet.transform.position));
            var dir = closestPoint - (Vector2)bullet.transform.position;

            RaycastHit2D hit;
            if (hit = Physics2D.Raycast(bullet.transform.position, dir, 1))
            {
                var normal = hit.normal;
                var perp = Vector2.Perpendicular(normal);

                var randomAngle1 = GetRandomAngle(normal, perp) * bullet.Velocity.magnitude;
                bullet.Velocity = randomAngle1;

                //Instantiate bullet from base info, and initialize
                var newBullet = Instantiate(bullet.baseBulletInfo).GetComponent<Bullet>();
                newBullet.transform.position = bullet.transform.position;

                //Apply direction and velocity
                var randomAngle2 = GetRandomAngle(normal, perp);
                newBullet.Init(bullet.baseBulletInfo, randomAngle2, bullet.ShooterTransform, bullet.Shooter);
                newBullet.Velocity = randomAngle2 * bullet.Velocity.magnitude;

                lastHitTimes[bullet] = Time.time;
                lastHitTimes[newBullet] = Time.time;

                visualOnDuplicate.Trigger(newBullet.transform.position);
            }

        }

        Vector2 GetRandomAngle(Vector2 normal, Vector2 perpendicular)
        {
            perpendicular = Random.Range(0f, 1f) > 0.5 ? perpendicular : -perpendicular;

            float xmin = FindLowestNumber(normal.x, perpendicular.x);
            float ymin = FindLowestNumber(normal.y, perpendicular.y);

            float xmax = FindHighestNumber(normal.x, perpendicular.x);
            float ymax = FindHighestNumber(normal.y, perpendicular.y);

            float deltaX = Random.Range(xmin, xmax);
            float deltaY = Random.Range(ymin, ymax);

            return new Vector2(deltaX, deltaY);
        }

        float FindLowestNumber(float value1, float value2)
        {
            return value1 < value2 ? value1 : value2;
        }

        float FindHighestNumber(float value1, float value2)
        {
            return value1 > value2 ? value1 : value2;
        }

    }

}

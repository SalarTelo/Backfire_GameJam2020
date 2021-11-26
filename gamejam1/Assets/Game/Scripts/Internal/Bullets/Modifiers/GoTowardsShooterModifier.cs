using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    public class GoTowardsShooterModifier : BulletModifier
    {
        public float delay;
        public float speed;
        public float maxVelocity;
        public float mass;
        public float maxForce;

        private float startTime = -1;

        public override void Modify(Bullet bullet)
        {
            base.Modify(bullet);

            startTime = Time.time;

        }

        private void Update()
        {
           
            //Don't start following shooter until timer is up
            if (startTime + delay > Time.time)
                return;

            if (bullet.ShooterTransform == null)
                return;

            var desiredVelocity = (Vector2)(bullet.ShooterTransform.position - transform.position);
            desiredVelocity = desiredVelocity.normalized * maxVelocity;

            var steering = desiredVelocity - bullet.Velocity;
            steering = Vector3.ClampMagnitude(steering, maxForce);
            steering /= mass;

            bullet.Velocity = Vector3.ClampMagnitude(bullet.Velocity + steering, maxVelocity);
            
        }
    }
}
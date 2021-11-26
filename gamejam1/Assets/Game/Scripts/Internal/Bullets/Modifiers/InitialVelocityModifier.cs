using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    public class InitialVelocityModifier : BulletModifier
    {
        [SerializeField] private float initialVelocity;
        [SerializeField] private bool addToShooterVelocity = true;

        public override void Modify(Bullet bullet)
        {
            base.Modify(bullet);

            bullet.Velocity += initialVelocity * bullet.InitialDirection;

            if(addToShooterVelocity)
            {
                //Try finding a RigidBodyVelocity helper
                RigidBodyVelocity rigidBodyVelocity = bullet.Shooter.GameObject?.GetComponentInChildren<RigidBodyVelocity>();

                if(rigidBodyVelocity != null)
                {
                    bullet.Velocity += rigidBodyVelocity.Velocity;
                    return;
                }

                //Otherwise try using rigidBody
                Rigidbody2D rigidBody = bullet.Shooter.GameObject?.GetComponentInChildren<Rigidbody2D>();

                if (rigidBody != null)
                    bullet.Velocity += rigidBody.velocity;
            }
        }
    }
}
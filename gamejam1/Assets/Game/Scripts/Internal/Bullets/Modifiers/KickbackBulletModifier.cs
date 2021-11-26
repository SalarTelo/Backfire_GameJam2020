using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
	public class KickbackBulletModifier : BulletModifier
	{
		public float speed;
		public float force;
		public float forceTime;

		private float initialTime;
		private Rigidbody2D shooterRigidBody;

		public override void Modify(Bullet bullet)
		{
			base.Modify(bullet);
			bullet.Velocity = speed * bullet.InitialDirection;

			initialTime = Time.time;

			shooterRigidBody = bullet.ShooterTransform.GetComponent<Rigidbody2D>();

			StartCoroutine(ApplyForce());
		}

		IEnumerator ApplyForce ()
		{
			while (initialTime + forceTime > Time.time)
			{
				if (bullet == null || shooterRigidBody == null)
					yield break;

				shooterRigidBody.AddForce(force * -bullet.InitialDirection);
				yield return null;
			}
		}
	}
}

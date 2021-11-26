using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
	public class ReverseBulletModifier : BulletModifier
	{
		public float delay;
		public float speed;
		public float speedBackwards;

		private float startTime = -1;
		private Vector3 shotPosition;
		private bool isReversed;

		public override void Modify(Bullet bullet)
		{
			base.Modify(bullet);

			startTime = Time.time;

			if(bullet.ShooterTransform != null)
				shotPosition = bullet.ShooterTransform.position;
			
			isReversed = false;
			BulletReverseMovement(isReversed);
		}

		private void FixedUpdate()
		{
			if (startTime + delay < Time.time && isReversed == false)
			{
				isReversed = true;
				BulletReverseMovement(isReversed);
			}
		}

		private void BulletReverseMovement(bool reversed)
		{
			if (reversed == false)
			{
				bullet.Velocity = speed * bullet.InitialDirection;
			}
			else
			{
				bullet.Velocity = speedBackwards * -bullet.InitialDirection;
			}
		}
	}
}

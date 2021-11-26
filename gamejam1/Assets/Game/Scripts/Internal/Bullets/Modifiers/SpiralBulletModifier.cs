using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
	public class SpiralBulletModifier : BulletModifier
	{
		public float delay;
		public float pullAmmount;
		public float mass;
		public float pullDecay;

		private float startTime = -1;
		private Vector3 centerPoint;
		private Vector3 pullDirection;

		public override void Modify(Bullet bullet)
		{
			base.Modify(bullet);

			startTime = Time.time;
			centerPoint = bullet.ShooterTransform.position;
		}

		private void Update()
		{
			//Don't start following shooter until timer is up
			if (startTime + delay > Time.time)
				return;

			Vector3 forceDirection = (transform.position - centerPoint).normalized;
			pullDirection = forceDirection * (pullAmmount * mass / Mathf.Pow(Vector3.Distance(transform.position, centerPoint), 2));

			pullAmmount -= pullDecay;
			bullet.GetComponent<Rigidbody2D>().AddForce(-pullDirection);
		}
	}
}

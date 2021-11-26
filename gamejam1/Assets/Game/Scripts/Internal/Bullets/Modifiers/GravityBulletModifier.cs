using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
	public class GravityBulletModifier : BulletModifier
	{
		public float delay;
		public float pullRadius;
		public float speed;
		public float mass;

		private float startTime = -1;
		private Vector2 pullDirection;
		private Collider2D[] entities;

		public override void Modify(Bullet bullet)
		{
			base.Modify(bullet);

			startTime = Time.time;
			entities = Physics2D.OverlapCircleAll(transform.position, pullRadius);
		}

		private void FixedUpdate()
		{
			//Don't start gravity until timer is up
			if (startTime + delay > Time.time)
				return;

			foreach (Collider2D entity in entities)
			{
				if (entity != null)
				{
					if (entity.gameObject.GetComponent<Rigidbody2D>() == true && entity.gameObject.GetComponent<GravityBulletModifier>() == false)
					{
						Vector3 forceDirection = (transform.position - entity.transform.position).normalized;
						pullDirection = forceDirection * (speed * (mass) / Mathf.Pow(Vector3.Distance(transform.position, entity.transform.position), 2));

						bullet.GetComponent<Rigidbody2D>().AddForce(-pullDirection);
					}
				}
			}
		}
	}
}

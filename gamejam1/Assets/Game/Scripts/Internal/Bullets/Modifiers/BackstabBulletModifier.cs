using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
	public class BackstabBulletModifier : BulletModifier
	{
		public float delay;
		public float speed;
		public float distance;

		[SerializeField] private PositionalVisualEffect visualOldPosition;
		[SerializeField] private PositionalVisualEffect visualNewPosition;

		private float startTime = -1;
		private Vector3 shotPosition;
		private bool isTeleported;
		private Vector2 teleportPosition;

		public override void Modify(Bullet bullet)
		{
			if (GameManager.Instance.player != null)
			{
				base.Modify(bullet);

				startTime = Time.time;
				shotPosition = bullet.ShooterTransform.position;
				isTeleported = false;
				BulletTeleport(isTeleported);
			}
		}

		private void FixedUpdate()
		{
			if (startTime + delay < Time.time && isTeleported == false)
			{
				isTeleported = true;
				BulletTeleport(isTeleported);
				bullet.Velocity *= -1;
			}
		}

		private void BulletTeleport(bool teleport)
		{
			if (bullet.ShooterTransform != null)
			{
				if (teleport == false)
				{
					bullet.Velocity = speed * bullet.InitialDirection;
				}

				else if (!bullet.IsDestroyed)
				{
					Vector3 oldPosition = transform.position;

					teleportPosition = ((Vector2)shotPosition - (Vector2)bullet.ShooterTransform.right * distance);
					bullet.Move(teleportPosition);
					bullet.Velocity = (bullet.ShooterTransform.position - transform.position).normalized * speed;

					visualOldPosition.Trigger(oldPosition);
					visualNewPosition.Trigger(teleportPosition);

				}
			}
		}
	}
}


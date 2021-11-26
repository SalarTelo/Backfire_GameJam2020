using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
	public class ChainLightningBulletModifier : BulletModifier
	{
		public float shockRadius;
		public float damage;
		public GameObject chainCirclePrefab;

		private bool isSchocking = false;

		private void Update()
		{
			if (isSchocking == false)
			{
				isSchocking = true;
				bullet.OnDestroyEvent += ChainLightning;
			}
		}

		void ChainLightning()
		{
			GameObject Circle = (GameObject)Instantiate(chainCirclePrefab, transform.position, Quaternion.identity);
			Circle.GetComponent<ChainLightningCircleScript>().Create(damage);
		}
	}
}

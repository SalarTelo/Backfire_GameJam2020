using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
	public class ChainLightningCircleScript : MonoBehaviour
	{
		public GameObject chainCirclePrefab;
		private float damage;
		private float radius;
		public void Create(float _damage)
		{
			damage = _damage;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.GetComponent<Chained>() == null)
			{
				IDamagable damagable = collision.GetComponent<IDamagable>();
				if (damagable == null)
				return;
				collision.gameObject.AddComponent<Chained>();
				damagable.Damage((int)damage);
				GameObject Circle = (GameObject)Instantiate(chainCirclePrefab, collision.transform.position, Quaternion.identity);
				kill();
			}
		}

		void kill()
		{
			Destroy(gameObject);
		}
	}
}

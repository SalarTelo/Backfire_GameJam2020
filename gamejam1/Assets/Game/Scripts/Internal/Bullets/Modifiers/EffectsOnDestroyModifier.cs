using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    public class EffectsOnDestroyModifier : BulletModifier
    {
        [SerializeField] private PositionalVisualEffect visualOnDestroy;

        public override void Modify(Bullet bullet)
        {
            bullet.OnDestroyEvent += OnBulletDestroy;
        }

        private void OnDisable()
        {
            if(bullet != null)
                bullet.OnDestroyEvent -= OnBulletDestroy;
        }

        private void OnBulletDestroy()
        {
            visualOnDestroy.Trigger(transform.position);
        }

    }
}
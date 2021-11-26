using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    public class SinWaveModifier : BulletModifier
    {
        public float speed;
        public float sinDistance;
        public float sinSpeed;

        private void Update()
        {
            if (bullet.ShooterTransform == null)
                return;

            Vector2 delta = new Vector2(Mathf.Cos(Time.fixedTime * sinSpeed), 0) * sinDistance;

            //Transform delta
            if(delta != Vector2.zero)
                delta = bullet.transform.TransformDirection(delta);

            bullet.MoveDelta(delta + (Vector2)bullet.InitialDirection * speed * Time.deltaTime);
        }
    }
}
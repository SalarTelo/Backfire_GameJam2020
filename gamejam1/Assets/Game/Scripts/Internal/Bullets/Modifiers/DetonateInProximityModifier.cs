using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpellcastStudios;

namespace SpellcastStudios
{
    public class DetonateInProximityModifier : BulletModifier
    {
        [SerializeField] float radius;

        [SerializeField] Color ringColor;
        [SerializeField] GameObject child;
        [SerializeField] GameObject explotion;

        bool isReadyToDetonate;
        float distance;

        void Update()
        {
            if (bullet.IsDestroyed || bullet.ShooterTransform == null)
                return;

            distance = Vector2.Distance(bullet.ShooterTransform.position, bullet.transform.position);
            if (!isReadyToDetonate && distance > radius)
            {
                isReadyToDetonate = true;
                GetComponent<BulletVisual>().SetPrimaryMaterialColor(Color.red);
                GetComponent<BulletVisual>().SetSecondaryMaterialColor(Color.red);
                child.SetActive(true);
            }

            //This is so fucking stupid.
            if (isReadyToDetonate)
            {
                child.GetComponent<SpriteRenderer>().color = new Color(ringColor.r, ringColor.g, ringColor.b, 1 / distance);

                if (distance < radius)
                    Detonate();
            }
        }

        private void Detonate()
        {
            if (bullet == null || bullet.Shooter == null || bullet.ShooterTransform == null)
                return;

            bullet.DestroySelf();
            bullet.Shooter.GameObject.GetComponent<HealthComponent>()?.Damage(1);

            Vector2 xplotionVector = (bullet.transform.position + bullet.ShooterTransform.position) / 2;
            Instantiate(explotion, xplotionVector, Quaternion.identity);

}

    }
}
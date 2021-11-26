using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpellcastStudios;


namespace SpellcastStudios {
    public class FieldsAddifier : FieldsBase
    {
        List<BulletModifier> modifiers;

        private void Awake()
        {
            modifiers = new List<BulletModifier>();
            modifiers.AddRange(GetComponentsInChildren<BulletModifier>());

        }

        protected virtual void Addify(Bullet bullet)
        {
            foreach (var mod in modifiers)
            {
                if (!HasModifier(bullet, mod))
                {

                    onApplyVisual.Trigger(bullet.transform.position);

                    var bulletModifier = Utilities.CopyComponent(mod, bullet.gameObject);
                    bulletModifier.Modify(bullet);
                }
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.GetComponent<Bullet>() != null)
            {
               
                var bullet = collision.GetComponent<Bullet>();

                Addify(bullet);
            }
        }

        protected bool HasModifier(Bullet bullet, BulletModifier modifier)
        {
            return bullet.GetComponent(modifier.GetType()) != null;
        }

    }


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SpellcastStudios;

namespace SpellcastStudios
{
    public class FieldsBase : MonoBehaviour
    {

        //We're going to need a list since, unlike the walls, we will continuously make changes
        //to the bullet instead of only modifying it on impact.
        protected List<Bullet> bullets;

        [SerializeField] protected PositionalVisualEffect onApplyVisual;

        private void Start()
        {
            bullets = new List<Bullet>();
        }

        protected virtual void Update()
        {
            NullCheck();
        }

        protected void NullCheck()
        {
            if (bullets.Count > 0)
            {
                for (int i = 0; i < bullets.Count; i++)
                {
                    if (bullets[i] == null)
                    {
                        bullets.RemoveAt(i);
                    }
                }
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Bullet>() != null)
            {
                var bullet = collision.GetComponent<Bullet>();
                if (!bullets.Contains(bullet))
                {
                    print("test");
                    bullets.Add(bullet);

                    onApplyVisual.Trigger(bullet.transform.position);
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<Bullet>() != null)
            {
                var bullet = collision.GetComponent<Bullet>();
                if (bullets.Contains(bullet))
                {
                    bullets.Remove(bullet);
                }
            }
        }
    }
}
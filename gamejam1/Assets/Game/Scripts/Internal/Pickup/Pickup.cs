using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    [RequireComponent(typeof(SpriteRenderer)), RequireComponent(typeof(Collider2D))]
    public abstract class Pickup : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        [SerializeField] private PositionalVisualEffect visualOnPickup;

        private bool isDestroyed;

        protected SpriteRenderer SpriteRenderer
        {
            get
            {
                if(spriteRenderer == null)
                    spriteRenderer = GetComponent<SpriteRenderer>();

                return spriteRenderer;
            }
        }

        protected abstract bool CanPickup(IEntity entity);

        protected abstract void OnPickup(IEntity entity);

        //Detect trigger if this object has a trigger
        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnObjectEnter(collision.gameObject);
        }

        //Detect collision if this object has a collider
        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnObjectEnter(collision.gameObject);
        }

        private void OnObjectEnter(GameObject otherGameObject)
        {
            if (isDestroyed)
                return;

            IEntity entity = otherGameObject.GetComponent<IEntity>();

            if (entity != null)
            {
                if (!CanPickup(entity))
                    return;

                OnPickup(entity);

                visualOnPickup.Trigger(entity.GameObject.transform.position);

                isDestroyed = true;

                Destroy(gameObject);
            }
        }

    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    /// <summary>
    /// Handles final velocity value of bullet and destruction for bullet
    /// </summary>
    public class Bullet : MonoBehaviour, IEntity
    {
        [SerializeField] private CameraShakeSetting shootShakeSetting;
        [SerializeField] private float rotationOffset;

        [SerializeField] private AudioClip onShootSound;

        private BulletVisual bulletVisual;

        GameObject IEntity.GameObject => gameObject;

        public Transform ShooterTransform { get; private set; }
        public Vector2 InitialDirection { get; private set; }
        public IEntity Shooter { get; private set; }
        public bool IsDestroyed { get; private set; }
        public Bullet baseBulletInfo { get; private set; }
        public BulletVisual Visual
        {
            get
            {
                if(bulletVisual == null)
                {
                    bulletVisual = GetComponent<BulletVisual>();
                    Assert.IsNotNull(bulletVisual);
                }

                return bulletVisual;
            }
        }

        public Vector2 Velocity
        {
            get => rigidBody?.velocity ?? Vector2.zero;
            set { if (rigidBody != null) rigidBody.velocity = value; }
        }

        public Action OnDestroyEvent;

        private Rigidbody2D rigidBody;
        public void Init(Bullet baseBulletInfo, Vector2 direction, Transform shooterTransform = null, IEntity shooter = null)
        {
            this.baseBulletInfo = baseBulletInfo;
            this.InitialDirection = direction;
            this.ShooterTransform = shooterTransform;
            this.Shooter = shooter;

            rigidBody = GetComponent<Rigidbody2D>();
            Assert.IsNotNull(rigidBody);

            foreach (var modifier in GetComponentsInChildren<BulletModifier>())
            {
                modifier.Modify(this);
            }

            if (shootShakeSetting != null)
                shootShakeSetting.ShakeAtPoint(transform.position);

            Visual.Init(this);

            SoundManager.PlayAudioClipAtPoint(onShootSound,transform.position);
        }

        /// <summary>
        /// Moves transform by a delta value
        /// </summary>
        /// <param name="deltaValue"></param>
        public void MoveDelta(Vector2 deltaValue)
        {
            rigidBody.MovePosition((Vector2)transform.position + deltaValue);
        }

        /// <summary>
        /// Set transform position
        /// </summary>
        /// <param name="deltaValue"></param>
        public void Move(Vector2 value)
        {
            rigidBody.MovePosition(value);
        }

        /// <summary>
        /// Sets isDestroyed flag and triggers OnDestroy event
        /// </summary>
        public void DestroySelf()
        {
            if (IsDestroyed)
                return;

            IsDestroyed = true;
            OnDestroyEvent?.Invoke();

            Destroy(gameObject);
        }

        private void Update()
        {
            if (IsDestroyed)
                return;

            if(rigidBody != null)
            {            
                //Update rotation
                float angle = Mathf.Atan2(rigidBody.velocity.y, rigidBody.velocity.x) * Mathf.Rad2Deg + rotationOffset;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }

        }

        private void OnDestroy()
        {
            IsDestroyed = true;
        }
    }
}
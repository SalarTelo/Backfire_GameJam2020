using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace SpellcastStudios
{
    public class SimpleDamagable : MonoBehaviour, IDamagable
    {
        [SerializeField] private int health;
        [SerializeField] private GameObject particlesOnDeath;
        [SerializeField] private AudioClip soundOnDeath;
        [SerializeField] private AudioClip soundOnDamage;
        [SerializeField] UnityEvent OnDeathEvent;

        private bool dead;

        GameObject IDamagable.GameObject => gameObject;

        public void DamagePoint(int damage, Vector2 position, float radius)
        {
            Damage(damage);
        }

        public void Damage(int damage)
        {
            if (dead)
                return;

            health -= damage;

            if (health <= 0)
                Kill();
            else
                SoundManager.PlayAudioClipAtPoint(soundOnDamage, transform.position);
        }

        void Kill()
        {
            dead = true;

            Instantiate(particlesOnDeath, transform.position, Quaternion.identity);
            SoundManager.PlayAudioClipAtPoint(soundOnDeath, transform.position);

            OnDeathEvent?.Invoke();

            Destroy(gameObject);
        }
    }

}
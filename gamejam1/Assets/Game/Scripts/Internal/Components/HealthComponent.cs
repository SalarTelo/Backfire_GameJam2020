using UnityEngine;
using System;
namespace SpellcastStudios
{
    public class HealthComponent : MonoBehaviour, IDamagable
    {
        [Range(0, 40)]
        [SerializeField] private int maxHealth;
        private int currentHealth;

        public int MaxHealth => maxHealth;
        public int Health => currentHealth;

        public Action OnDeath;
        public Action OnHeal;
        public Action OnDamage;

        private bool isDead;

        public bool Dead => isDead;
        public bool Alive => !isDead;

        GameObject IDamagable.GameObject => gameObject;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void AddHealth(int value)
        {
            if (isDead)
                return;

            currentHealth += value;

            OnHeal?.Invoke();
        }
        public void RemoveHealth(int value)
        {
            if (isDead)
                return;

            currentHealth -= value;

            if (currentHealth <= 0)
                Kill();
        }

        public void AddMaxHealth(int value)
        {
            maxHealth += value;
        }

        public void RemoveMaxHealth(int value)
        {
            if(maxHealth > value)
                maxHealth -= value;
        }

        private void Kill()
        {
            isDead = true;
            OnDeath?.Invoke();
        }

        public void Damage(int damage)
        {
            RemoveHealth(damage);
        }

        public void DamagePoint(int damage, Vector2 position, float radius = 0)
        {
            RemoveHealth(damage);
        }
    }
}

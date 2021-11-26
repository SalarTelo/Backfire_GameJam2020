using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    public class HealthPickup : Pickup
    {
        [SerializeField] private int health;

        protected override bool CanPickup(IEntity character)
        {
            HealthComponent healthComp = character.GameObject.GetComponent<HealthComponent>();

            if (healthComp == null)
                return false;

            return healthComp.Health < healthComp.MaxHealth;
        }

        protected override void OnPickup(IEntity character)
        {
            HealthComponent healthComp = character.GameObject.GetComponent<HealthComponent>();

            healthComp.AddHealth(health);
        }

    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    [RequireComponent(typeof(HealthComponent))]
    public class EffectOnDamage : MonoBehaviour
    {
        [SerializeField] private PositionalVisualEffect visualOnDamage;
        [SerializeField] private PositionalVisualEffect visualOnDeath;


        private void Start()
        {
            HealthComponent healthComponent = GetComponent<HealthComponent>();

            healthComponent.OnDamage += OnDamage;
            healthComponent.OnDeath += OnDeath;
        }

        private void OnDeath()
        {
            visualOnDeath.Trigger(transform.position);
        }

        private void OnDamage()
        {
            visualOnDamage.Trigger(transform.position);
        }
    }

}
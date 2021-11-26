using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    [RequireComponent(typeof(HealthComponent))]
    public class DestroyOnDeath : MonoBehaviour
    {
        private void Start()
        {
            HealthComponent healthComponent = GetComponent<HealthComponent>();

            healthComponent.OnDeath += OnDeath;
        }

        private void OnDeath()
        {
            Destroy(gameObject);
        }
    }

}
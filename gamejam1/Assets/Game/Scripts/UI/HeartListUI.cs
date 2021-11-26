using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using TMPro;

namespace SpellcastStudios.UI
{
    public class HeartListUI : MonoBehaviour
    {
        [SerializeField] private GameObject heartUIPrefab;
        [SerializeField] private Transform heartListParent;

        private HealthComponent health;

        private int Health => health?.Health ?? 0;
        private int MaxHealth => health?.MaxHealth?? 0;

        private List<HeartUI> hearts;

        private int lastHealth = -1;
        private int lastMaxHealth = -1;

        private void Start()
        {
            health = GameManager.Instance.player.GetComponent<HealthComponent>();

            UpdateHeartList();
        }

        private void Update()
        {
            //Detect change in max health
            if (lastMaxHealth != MaxHealth)
                UpdateHeartList();

            //Detect change in health
            else if (lastHealth != Health)
                UpdateHeartValues();
        }

        private void UpdateHeartValues()
        {
            for (int i = 0; i < Mathf.CeilToInt(MaxHealth/2); i++)
            {
                int value = Health - (i * 2) - 1;

                if (value > 0)
                    hearts[i].SetFill(2);

                else if (value == 0)
                    hearts[i].SetFill(1);

                else
                    hearts[i].SetFill(0);
            }

            lastHealth = Health;
        }

        //Updates heart objects, and then internally updates the values
        private void UpdateHeartList()
        {
            //Clean up children
            for (int i = 0; i < heartListParent.childCount; i++)
                Destroy(heartListParent.GetChild(i).gameObject);

            hearts = new List<HeartUI>();

            for (int i = 0; i < Mathf.CeilToInt(MaxHealth/2); i++)
            {
                HeartUI heartUI = Instantiate(heartUIPrefab).GetComponent< HeartUI>();
                heartUI.transform.SetParent(heartListParent);
                hearts.Add(heartUI);
            }

            lastMaxHealth = MaxHealth;

            UpdateHeartValues();
        }
    }
}
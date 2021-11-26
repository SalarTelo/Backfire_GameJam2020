using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using TMPro;

namespace SpellcastStudios.UI
{
    public class BulletListInfoUI : MonoBehaviour
    {
        [SerializeField] private BulletInfoUI mainBulletInfo;
        [SerializeField] private Transform otherBulletListParent;

        [Header("Assets")]
        [SerializeField] private GameObject otherBulletInfoPrefab;

        private InventoryComponent inventory;
        private ShooterComponent shooter;

        private BulletCase CurrentBulletCase => inventory?.GetBulletCase(shooter.currentBullet) ?? new BulletCase();

        private void Start()
        {
            inventory = GameManager.Instance.player.GetComponent<InventoryComponent>();
            shooter = GameManager.Instance.player.GetComponent<ShooterComponent>();

            Assert.IsNotNull(inventory);
            Assert.IsNotNull(shooter);

            shooter.OnCurrentBulletChange += OnInventoryChange;

            OnInventoryChange();
        }

        private void OnDestroy()
        {
            if (inventory != null)
                inventory.OnBulletCaseChange -= OnInventoryChange;
        }

        private void OnInventoryChange()
        {
            if (CurrentBulletCase.IsNull)
            {
                mainBulletInfo.SetBulletCase(BulletCase.Null);
                return;
            }

            mainBulletInfo.SetBulletCase(CurrentBulletCase);

            //Update list
            UpdateOtherBulletList();
        }

        private void UpdateOtherBulletList()
        {
            //Clean up children
            for (int i = 0; i < otherBulletListParent.childCount; i++)
                Destroy(otherBulletListParent.GetChild(i).gameObject);

            for(int i = 0; i < inventory.BulletCaseCount;i++)
            {
                //Start with the bullet aftercurrent bullet 
                int index = shooter.currentBullet + 1 + i;

                if (index >= inventory.BulletCaseCount)
                    index -= inventory.BulletCaseCount;

                //Don't draw current bullet
                if(index == shooter.currentBullet)
                    continue;

                GameObject infoObj = Instantiate(otherBulletInfoPrefab);
                infoObj.transform.SetParent(otherBulletListParent.transform);

                BulletInfoUI infoUI = infoObj.GetComponent<BulletInfoUI>();
                infoUI.SetBulletCase(inventory.GetBulletCase(index));
            }
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SpellcastStudios{
    public class InventoryComponent : MonoBehaviour
    {
        [SerializeField] private List<BulletCase> bullets;

        /// <summary>
        /// Triggered when anything in the bulletcase list changes (removal, addition, count change)
        /// </summary>
        public Action OnBulletCaseChange;

        /// <summary>
        /// The number of different bullet cases
        /// </summary>
        public int BulletCaseCount => bullets.Count;

        /// <summary>
        /// Returns a specific bullet case at an index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public BulletCase GetBulletCase(int index)
        {
            if (index < 0 || index >= bullets.Count)
                return BulletCase.Null;

            return bullets[index];
        }


        /// <summary>
        /// Adds a new bullet case to the inventory
        /// </summary>
        /// <param name="bulletCase"></param>
        public void AddBulletCase(BulletCase bulletCase)
        {
            //Check if bullet case of same prefab already exists
            for (int i = 0; i < bullets.Count; i++)
            {
                //If it does, then add the count!
                if (bullets[i].bulletPrefab == bulletCase.bulletPrefab)
                {
                    //Only do it if the original count wasn't infinite
                    if (bullets[i].count >= 0)
                        SetBulletCount(i, bullets[i].count + bulletCase.count);
                    return;
                }
            }

            bullets.Add(bulletCase);
            OnBulletCaseChange?.Invoke();
        }
        /// <summary>
        /// Removes a bullet case from the inventory
        /// </summary>
        /// <param name="index"></param>
        public void RemoveBulletCase(int index)
        {
            if (index < 0 || index >= bullets.Count)
                return;

            bullets.RemoveAt(index);
            OnBulletCaseChange?.Invoke();
        }

        /// <summary>
        /// Sets the count for a certain bullet case
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        public void SetBulletCount(int index, int count)
        {
            if (index < 0 || index >= bullets.Count)
                return;

            BulletCase newCase = new BulletCase(bullets[index]);
            newCase.count = count;
            bullets[index] = newCase;
            OnBulletCaseChange?.Invoke();
        }
    }
}
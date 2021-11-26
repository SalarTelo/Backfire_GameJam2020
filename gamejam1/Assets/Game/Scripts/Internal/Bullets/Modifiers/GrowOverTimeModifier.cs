using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    public class GrowOverTimeModifier : BulletModifier
    {
        [SerializeField] float maxSize;
        [SerializeField] float growSpeed;

        private void Update()
        {
            if (transform.localScale.magnitude < maxSize)
            {
                transform.localScale += Vector3.one * growSpeed * Time.deltaTime;
            }
            else
            {
                bullet.DestroySelf();
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;

namespace SpellcastStudios
{
    /// <summary>
    /// Base class that handles the detection of a tilemap layer that can detect
    /// Bullets
    /// </summary>
    
    [RequireComponent(typeof(Tilemap))]
    public abstract class DynamicTilemap : MonoBehaviour
    {
        protected Tilemap tilemap { get; private set; }
        protected abstract void OnBulletHit(Bullet bullet,Vector3Int tile,Vector3 worldPosition);

        protected virtual void Start()
        {
            tilemap = GetComponent<Tilemap>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Bullet bullet = collision.GetComponent<Bullet>();

            if (bullet == null)
                return;

            CircleCollider2D sphereCollider = bullet.GetComponentInChildren<CircleCollider2D>();

            if(sphereCollider == null)
            {
                Debug.LogError("Dynamic Tilemap requires the bullet to have a sphere collider, oops");
                return;
            }

            foreach(RaycastHit2D raycast2D in Physics2D.CircleCastAll(bullet.transform.position,sphereCollider.radius,Vector2.up,0))
            {
                if (raycast2D.collider.transform == transform)
                {
                    //Found a hit!
                    Vector3Int cellPoint = tilemap.WorldToCell(raycast2D.point);

                    if (tilemap.GetTile(cellPoint))
                        OnBulletHit(bullet, cellPoint, raycast2D.point);

                    return;
                }
            }
        }


    }
}
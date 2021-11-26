using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;

namespace SpellcastStudios
{

    [RequireComponent(typeof(Tilemap))]
    public class DestructableTilemap : DynamicTilemap, IDamagable
    {
        [SerializeField] private PositionalVisualEffect visualOnDestroyTile;

        [SerializeField, Tooltip("This will be played at the center point of damage")]
        private AudioClip audioClipOnDestroyTile;

        GameObject IDamagable.GameObject => gameObject;

        //Basic damage does nothing
        public void Damage(int damage) { }

        public void DamagePoint(int damage, Vector2 position,float radius)
        {
            var tiles = TilesInCircle(position, radius);

            bool didDamage = false;

            foreach (var pos in tiles)
            {
                if (tilemap.GetTile(new Vector3Int(pos.x,pos.y,0)) != null)
                {
                    didDamage = true;
                    OnBulletHit(null, (Vector3Int)pos, tilemap.CellToWorld(new Vector3Int(pos.x, pos.y, 0)));
                }
            }

            if (didDamage)
                SoundManager.PlayAudioClipAtPoint(audioClipOnDestroyTile, position);
        }

        public List<Vector2Int> TilesInCircle(Vector2 worldPosition, float radius)
        {
            List<Vector2Int> inside = new List<Vector2Int>();
            for(int x = tilemap.cellBounds.xMin; x < tilemap.cellBounds.xMax; x++)
            {
                for(int y = tilemap.cellBounds.yMin; y < tilemap.cellBounds.yMax; y++)
                {
                    if (Vector2.Distance(worldPosition, (Vector2)tilemap.CellToWorld(new Vector3Int(x, y,0))) <= radius)
                        inside.Add(new Vector2Int(x, y));
                }
            }
                    
            return inside;
        }

        protected override void OnBulletHit(Bullet bullet, Vector3Int tile, Vector3 worldPosition)
        {
            //Destroy!
            tilemap.SetTile(tile, null);

            visualOnDestroyTile.Trigger(worldPosition);
        }

    }
}
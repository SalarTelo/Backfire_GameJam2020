using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;

namespace SpellcastStudios { 
public class WallModifier : MonoBehaviour
    {
        [SerializeField] protected AudioClip onBulletHitSound;

        protected virtual void Affect(Bullet bullet)
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();

            if (bullet != null)
            {
                Affect(bullet);
                SoundManager.PlayAudioClipAtPoint(onBulletHitSound, transform.position);
            }
        }
    }
}

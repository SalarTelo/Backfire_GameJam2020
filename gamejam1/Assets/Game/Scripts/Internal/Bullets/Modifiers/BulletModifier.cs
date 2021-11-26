
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    public class BulletModifier : MonoBehaviour
    {
        protected Bullet bullet;


        public virtual void Modify(Bullet bullet)
        {
            this.bullet = bullet;
        }

    }

  

}
using SpellcastStudios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellcastStudios
{
    public class ReflectWall : WallModifier
    {
        protected override void Affect(Bullet bullet)
        {
            //For some reason the bullet doesn't seem to always register the collission. -Loran
            bullet.Velocity = Vector3.Reflect(bullet.Velocity, bullet.transform.position - transform.position);
        }

    }
}
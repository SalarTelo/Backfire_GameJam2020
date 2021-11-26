using SpellcastStudios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellcastStudios
{
    public class ReversedWall : WallModifier
    {
        protected override void Affect(Bullet bullet)
        {
            bullet.Velocity = -bullet.Velocity;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpellcastStudios;

namespace SpellcastStudios {

    //This should ONLY be used if the wall has a modifier that makes the bullet bounce somewhere else.
    //Since it makes no sense to modify the speed of a bullet that will explode on impact of the wall.
    public class ModifyBulletSpeedWall : WallModifier
    {
        [Range(1, 4)]
        [SerializeField] float bulletSpeedMultiplier;

        protected override void Affect(Bullet bullet)
        {
            bullet.Velocity *= bulletSpeedMultiplier;
        }
    }
}

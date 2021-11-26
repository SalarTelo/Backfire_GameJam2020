using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellcastStudios
{
    public interface IDamagable
    {
        GameObject GameObject { get; }
        void Damage(int damage);
        void DamagePoint(int damage,Vector2 position,float radius = 0);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpellcastStudios;

namespace SpellcastStudios
{
    public class SpeedIncreaseModifier : FieldsModifier
    {
        //TODO: FIND A BETTER FUCKING NAME
        [SerializeField] float speedDecreaser;
        protected override void Modify()
        {
            if(bullets.Count > 0)
            {
                foreach(var bullet in bullets)
                {
                    bullet.Velocity += bullet.Velocity.normalized * speedDecreaser * Time.deltaTime;
                }
            }
        }
        protected override void Update()
        {
            base.Update();
            Modify();
        }
    }
}
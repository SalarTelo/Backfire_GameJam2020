using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpellcastStudios;

namespace SpellcastStudios {

    public class DelayedFieldAddifier : FieldsAddifier
    {
        [Range(0.1f ,4)]
        [SerializeField] float minTimer;
        [Range(0.1f , 4)]
        [SerializeField] float maxTimer;

        [SerializeField] float timer;

        [SerializeField] bool isRandom;

        protected override void Addify(Bullet bullet)
        {
            if (isRandom)
            {
                timer = Random.Range(minTimer, maxTimer);
            }
            StartCoroutine(delay(timer, bullet));
        }

        IEnumerator delay(float time, Bullet bullet)
        {
            yield return new WaitForSeconds(time);
            if (bullet != null)
            {
                base.Addify(bullet);
            }
        }
    }
}
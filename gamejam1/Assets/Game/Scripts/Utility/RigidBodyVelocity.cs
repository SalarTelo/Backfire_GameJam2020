using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellcastStudios
{
    /// <summary>
    /// Calculates rigid body velocity internally, instead of relying on rigidbody calculation.
    /// This is because if MovePosition is used, velocity is not touched.
    /// </summary>
    public class RigidBodyVelocity : MonoBehaviour
    {
        private Vector2 previousPosition;

        public Vector2 Velocity { get; private set; }

        private void Start()
        {
            previousPosition = transform.position;
        }

        private void Update()
        {
            Velocity = ((Vector2)transform.position - previousPosition) / Time.deltaTime;
            previousPosition = transform.position;
        }
    }
}


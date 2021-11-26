using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace SpellcastStudios
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MobilityComponent : MonoBehaviour
    {
        [SerializeField] float movementSpeed;

        private Rigidbody2D rigidBod;

        public Vector2 Velocity { get => rigidBod.velocity;}
        public Rigidbody2D RigidBod { get => rigidBod; set => rigidBod = value; }

        private void Start()
        {
            RigidBod = GetComponent<Rigidbody2D>();
        }


        /// <summary>
        /// Uses Rigidbody2D to move towards a specific destination.
        /// </summary>
        /// <param name="destination">Destination for rigidbody to move towards</param>
        public void MoveDestination(Vector2 destination)
        {
            var direction = destination - (Vector2)transform.position;
            RigidBod.MovePosition(RigidBod.position + direction * movementSpeed * Time.fixedDeltaTime);
        }
        /// <summary>
        /// Uses Rigidbody2D to move towards a specific destination.
        /// </summary>
        /// <param name="destination">Destination for rigidbody to move towards</param>
        /// <param name="speed">Only use if you want the entity to move with a specific speed</param>
        public void MoveDestination(Vector2 destination, float speed)
        {
            var direction = destination - (Vector2)transform.position;
            RigidBod.MovePosition(RigidBod.position + direction * speed * Time.fixedDeltaTime);
        }

        /// <summary>
        /// Uses Rigidbody2D to move towards a specific direction.
        /// </summary>
        /// <param name="direction">Direction for rigidbody to move towards</param>
        public void MoveDirection(Vector2 direction)
        {
            RigidBod.MovePosition((Vector2)(RigidBod.position + direction * movementSpeed * Time.fixedDeltaTime));
        }
        /// <summary>
        /// Uses Rigidbody2D to move towards a specific destination.
        /// </summary>
        /// <param name="direction">Direction for rigidbody to move towards</param>
        /// <param name="speed">Only use if you want the entity to move with a specific speed</param>
        public void MoveDirection(Vector2 direction, float speed)
        {
            RigidBod.MovePosition((Vector2)(RigidBod.position + direction * speed * Time.fixedDeltaTime));
        }

        /// <summary>
        /// Uses Rigidbody2D to rotate towards a specific rotation.
        /// </summary>
        /// <param name="angle">Angle for rigidbody to rotate towards  (0, 360)</param>
        /// <param name="speed">Speed of rotation</param>
        public void RotateTowardsAngle(float angle, float speed = 10)
        {
            var rot = Mathf.Lerp(RigidBod.rotation, angle, speed * Time.fixedDeltaTime);
            RigidBod.MoveRotation(rot);
        }
        /// <summary>
        /// Uses Rigidbody2D to set the rotation.
        /// </summary>
        /// <param name="angle">New angle (0, 360)</param>
        public void RotateToAngle(float angle)
        {
            RigidBod.SetRotation(angle);
        }

        /// <summary>
        /// Uses Rigidbody2D to rotate towards a specific rotation.
        /// </summary>
        /// <param name="destination">Automatically calculate the rotation by giving a point</param>
        /// <param name="speed">Speed of rotation</param>
        public void RotateTowardsAngle(Vector2 destination, float speed = 10)
        {
            var dir = destination - (Vector2)transform.position;
            var rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            var newRot = Mathf.Lerp(RigidBod.rotation, rot, speed * Time.fixedDeltaTime);
            RigidBod.MoveRotation(newRot);
        }
        /// <summary>
        /// Uses Rigidbody2D to set the rotation.
        /// </summary>
        /// <param name="angle">New angle (0, 360)</param>
        public void RotateToAngle(Vector2 destination)
        {
            var dir = destination - (Vector2)transform.position;
            var rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            RigidBod.SetRotation(rot);
        }
    }
}

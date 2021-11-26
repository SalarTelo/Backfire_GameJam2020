using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    [RequireComponent(typeof(Rigidbody2D),typeof(InventoryComponent),typeof(ShooterComponent))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;

        [SerializeField] private AudioClip onBulletSwitchSound;

        private Rigidbody2D rigidBody;
        private InventoryComponent inventory;
        private ShooterComponent shooter;

        private void Start()
        {
            inventory = GetComponent<InventoryComponent>();
            shooter = GetComponent<ShooterComponent>();
            rigidBody = GetComponent<Rigidbody2D>();
        }

        //Use fixedupdate due to rigidbody being used
        private void FixedUpdate()
        {
            //Movement
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");

            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rigidBody.MovePosition(rigidBody.position + (movement * movementSpeed * Time.fixedDeltaTime));

        }

        private void Update()
        {
            //Aim
            Vector3 aimPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 direction = ((Vector2)(Camera.main.ScreenToWorldPoint(aimPosition) - transform.position)).normalized;

            //Update rotation with aim as well
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            //Shoot
            if (Input.GetKeyDown(KeyCode.Mouse0))
                shooter.ShootBullet();

            //Change bullet forward
            if(Input.mouseScrollDelta.y != 0)
            {
                int currentBullet = shooter.currentBullet;

                if (Input.mouseScrollDelta.y < 0)
                    currentBullet++;
                else
                    currentBullet--;

                if (currentBullet >= inventory.BulletCaseCount)
                    currentBullet = 0;

                if (currentBullet < 0)
                    currentBullet = inventory.BulletCaseCount - 1;

                shooter.SetCurrentBullet(currentBullet);

                SoundManager.PlayAudioClip(onBulletSwitchSound);
            }

        }

    }

}

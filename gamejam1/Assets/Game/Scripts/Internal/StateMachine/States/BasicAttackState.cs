using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    public class BasicAttackState : MonoBehaviour, IState
    {
        [SerializeField] private ShooterComponent shooter;
        [SerializeField] private float hipFireMin = 0.5f;
        [SerializeField] private float hipFireMax = 1f;

        private float timer;

        public StateManager manager { get; set; }

        private void Awake()
        {
            Assert.IsNotNull(shooter);

            timer = Random.Range(hipFireMin,hipFireMax);
        }
        public void Action()
        {
        }
        public void Input()
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                shooter.ShootBullet();
                manager.SetValue<bool>("AttackPlayer", false);
                timer = Random.Range(hipFireMin, hipFireMax);
            }
        }
        public void Enter()
        {
        }
        public void Exit()
        {
        }

    }
}
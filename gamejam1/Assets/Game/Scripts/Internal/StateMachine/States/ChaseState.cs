using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    public class ChaseState : MonoBehaviour, IState
    {
        [SerializeField] private MobilityComponent mobility;

        //How far away should the player be until the enemy breaks
        [Range(0, 30)]
        public float breakDistanceToTarget;

        private Vector2 desiredLocation;
        public StateManager manager { get; set; }

        private GameObject player => GameManager.Instance.player;

        private void Start()
        {
            Assert.IsNotNull(mobility);
        }

        public void Action()
        {
            if (!player)
                return;

            if (!IsInCloseProximity())
            {
                mobility.MoveDestination(desiredLocation);
                mobility.RotateTowardsAngle(player.transform.position);
            }
        }
        public void Input()
        {
            if (!player)
                return; 

            desiredLocation = player.transform.position;
            if (IsInCloseProximity())
                manager.SetValue<bool>("AttackPlayer", true);
        }

        public void Enter()
        {
            print("Entering Attack State");
        }
        public void Exit()
        {
            print("Exiting Attack State");
        }

        bool IsInCloseProximity()
        {
            if (!player)
                return false;

            var distance = Vector2.Distance(transform.position, player.transform.position);
            return distance <= breakDistanceToTarget;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, breakDistanceToTarget);
        }
    }
}
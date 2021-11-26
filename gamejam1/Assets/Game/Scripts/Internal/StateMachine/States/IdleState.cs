using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    public class IdleState : MonoBehaviour, IState
    {
        [SerializeField] private MobilityComponent mobility;

        [Range(1,10)]
        [SerializeField] private float wanderRadius;

        [Range(1, 10)]
        [SerializeField] private float maxWanderTimer;

        [Range(1, 10)]
        [SerializeField] private float minWanderTimer;

        private float wanderTimer;
        private Vector2 desiredLocation;
        public StateManager manager { get; set; }


        private void Start()
        {
            Assert.IsNotNull(mobility);
        }


        public void Action()
        {
            mobility.MoveDestination(desiredLocation);
            mobility.RotateTowardsAngle(desiredLocation);
        }
        public void Input()
        {
            wanderTimer -= Time.deltaTime;
            if(wanderTimer <= 0)
            {
                desiredLocation = GetRandomPosition();
                wanderTimer = GetRandomWanderTimer();
            }
        }


        public void Enter()
        {
            wanderTimer = GetRandomWanderTimer();
            print("Entering Idle State");
        }
        public void Exit()
        {
            print("Exiting Idle State");
        }


        private float GetRandomWanderTimer()
        {
            return Random.Range(minWanderTimer, maxWanderTimer);
        }
        private Vector2 GetRandomPosition()
        {
            Vector2 temp = Vector2.zero;
            do
            {
                temp = transform.position + Random.insideUnitSphere * Random.Range(wanderRadius/2, wanderRadius);
            } 
            while (!CheckForObstacle(temp));

            return temp;
        }

        //Returns true if obstacle is in the way
        //TODO: The raycast is being wierd.
        private bool CheckForObstacle(Vector2 destination)
        {
            return true;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, wanderRadius);
        }
    }
}
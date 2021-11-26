using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellcastStudios {

    public class SM_BasicEnemy : StateManager
    {

        private void Start()
        {
            AddValue<bool>("SeeEnemy", false);
            AddValue<bool>("AttackPlayer", false);

            ChangeState(GetComponent<IdleState>());
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }
        protected override void Update()
        {
            base.Update();

            if (GetValue<bool>("SeeEnemy") == true)
            {
                if (GetValue<bool>("AttackPlayer") == true)
                {
                    ChangeState(GetComponent<BasicAttackState>());
                }
                else
                {
                    ChangeState(GetComponent<ChaseState>());
                }
            }
            else
            {
                ChangeState(GetComponent<IdleState>());
            }

        }

    }
}
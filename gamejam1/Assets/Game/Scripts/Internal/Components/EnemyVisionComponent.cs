using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellcastStudios
{
    [RequireComponent(typeof(StateManager))]
    public class EnemyVisionComponent : MonoBehaviour
    {
        [Range(0, 20)]
        [SerializeField] private float proximityRadius;

        private StateManager manager;

        private void Start()
        {
            manager = GetComponent<StateManager>();
        }

        void Update()
        {
            if (!GameManager.Instance.player)
                return;

            var distance = Vector2.Distance(transform.position, GameManager.Instance.player.transform.position);
            var checker = distance < proximityRadius;
            manager.SetValue<bool>("SeeEnemy", checker);
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, proximityRadius);

        }
    }
}
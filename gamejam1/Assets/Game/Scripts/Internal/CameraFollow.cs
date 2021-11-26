using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellcastStudios
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private bool enableLerp = true;

        [SerializeField] private float lerpSpeed;

        void LateUpdate()
        {
            if (GameManager.Instance.player == null)
                return;

            Vector3 target = GameManager.Instance.player.transform.position;

            //The player stutters alot. We need to find a way to make the movement smooth
            var goal = new Vector3(target.x, target.y, transform.position.z);

            if(enableLerp)
                transform.position = Vector3.Lerp(transform.position, goal, lerpSpeed * Time.deltaTime);
            else
                transform.position = goal;
        }
    }
}
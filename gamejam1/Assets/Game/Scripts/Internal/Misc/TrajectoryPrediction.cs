using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryPrediction : MonoBehaviour
{
    [SerializeField] GameObject sprite;
    [SerializeField] float lerpRingSpeed;
    private void Start()
    {
        sprite.SetActive(false);
    }

    private void Update()
    {
        RaycastHit2D hit;

        if (hit = Physics2D.Raycast(transform.position + transform.up * 0.5f, transform.up, 1000))
        {
            if (!sprite.activeSelf)
                sprite.SetActive(true);
            sprite.transform.position = Vector2.Lerp(sprite.transform.position, hit.point, lerpRingSpeed* Time.deltaTime);
        }
    }


}

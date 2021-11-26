using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayAbility : MonoBehaviour
{

    [SerializeField] float destroyTimer;

    private void Start()
    {
        transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = ($"Added \nmodifier!");
    }

    private void Update()
    {
        destroyTimer -= Time.deltaTime;
        if (destroyTimer < 0)
            Destroy(gameObject);

    }



}

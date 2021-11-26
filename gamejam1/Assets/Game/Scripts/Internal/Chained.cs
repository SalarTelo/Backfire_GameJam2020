using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chained : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine("wait");
    }

    IEnumerator wait()
	{
		yield return new WaitForSeconds(0.5f);
		kill();
	}

	void kill()
	{
		Destroy(this);
	}
}

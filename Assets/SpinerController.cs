using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinerController : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {

        Vector3 rotator = new Vector3(0, 3, 0);

        transform.Rotate(rotator);
	}
}

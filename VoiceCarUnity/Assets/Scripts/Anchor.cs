using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour {

    public Transform anchor;

	void Update () {
        if(anchor) {
            transform.position = new Vector3(anchor.position.x, anchor.position.y, transform.position.z);
        }
	}
}

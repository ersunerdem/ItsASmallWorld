using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gear : MonoBehaviour {

    public float rotation = 1f;

    void FixedUpdate () {
        transform.Rotate(new Vector3(0, 0, rotation));
	}
}

using UnityEngine;
using System.Collections;

public class Baboon : MonoBehaviour {
    public float floatForce = 0.04f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    GetComponent<Rigidbody>().AddForceAtPosition(Vector3.up * floatForce, transform.up + transform.position, ForceMode.Acceleration);
	}
}

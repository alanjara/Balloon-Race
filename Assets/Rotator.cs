using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    public float rotatespeed = 500f;
	// Update is called once per frame
	void Update () {
        transform.eulerAngles += new Vector3(0, rotatespeed * Time.deltaTime, 0.5f*rotatespeed * Time.deltaTime);
	}
}

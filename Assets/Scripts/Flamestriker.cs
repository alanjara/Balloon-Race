using UnityEngine;
using System.Collections;

public class Flamestriker : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(transform.gameObject,2f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * 20f * Time.deltaTime);
	}
}

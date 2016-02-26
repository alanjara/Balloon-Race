using UnityEngine;
using System.Collections;

public class AutoTrack : MonoBehaviour {
    public GameObject target;
	// Use this for initialization
	void Start () {
        target = GameObject.Find("Balloon 2");
	}
	
	// Update is called once per frame
	void Update () {
        float value;
        Vector3 dir;
        dir = (target.transform.position - transform.position).normalized;
        value = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        gameObject.GetComponent<Rigidbody>().velocity = value * dir;

    }
}

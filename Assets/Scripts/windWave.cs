using UnityEngine;
using System.Collections;

public class windWave : MonoBehaviour {
   
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 2f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale +=  100*Time.deltaTime * Vector3.one;
	}
}

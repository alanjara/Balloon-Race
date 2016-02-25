using UnityEngine;
using System.Collections;

public class Poof : MonoBehaviour {
    public float speed = 1f;
	// Use this for initialization
	void Start () {
        targetScale = new Vector3(4f, 1.5f, 4f);
	}



    public Vector3 targetScale;
	// Update is called once per frame
	void Update () {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);
	}
}

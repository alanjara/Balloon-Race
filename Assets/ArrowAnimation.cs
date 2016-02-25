using UnityEngine;
using System.Collections;

public class ArrowAnimation : MonoBehaviour {

    bool goingup = true;
    public float offset = 0.3f, speed = 2f;
    Vector3 uploc, downloc;
	// Use this for initialization
	void Start () {
        uploc = transform.position + Vector3.up * offset;
        downloc = transform.position - Vector3.up * offset;
	}
	
	// Update is called once per frame
	void Update () {
        if (goingup)
        {
            transform.position = Vector3.Lerp(transform.position, uploc, Time.deltaTime * speed);
            if((transform.position - uploc).magnitude < 0.1)
            {
                goingup = false;
            }
        } else
        {
            transform.position = Vector3.Lerp(transform.position, downloc, Time.deltaTime * speed);
            if ((transform.position - downloc).magnitude < 0.1)
            {
                goingup = true;
            }
        }
	}
}

using UnityEngine;
using System.Collections;

public class Whirligig : MonoBehaviour {
    Transform focus;
    Rigidbody rb;
    public float SPEED = 0.1f;
    public bool RotInXZ = true;
    public bool RotInXY = false;
    public bool RotInZY = false;
	// Use this for initialization
	void Start () {
        focus = transform.parent;
        rb = GetComponent<Rigidbody>();
	}
	
    void FixedUpdate()
    {
        if (RotInXZ)
        {

            float newx = focus.position.x + (transform.position.x - focus.position.x) * Mathf.Cos(SPEED) - (transform.position.z - focus.position.z) * Mathf.Sin(SPEED);
            float newz = focus.position.z + (transform.position.x - focus.position.x) * Mathf.Sin(SPEED) + (transform.position.z - focus.position.z) * Mathf.Cos(SPEED);

            transform.position = new Vector3(newx, transform.position.y, newz);
        } else if (RotInXY)
        {
            float newx = focus.position.x + (transform.position.x - focus.position.x) * Mathf.Cos(SPEED) - (transform.position.y - focus.position.y) * Mathf.Sin(SPEED);
            float newz = focus.position.y + (transform.position.x - focus.position.x) * Mathf.Sin(SPEED) + (transform.position.y - focus.position.y) * Mathf.Cos(SPEED);

            transform.position = new Vector3(newx, newz, transform.position.z);
        }
        else
        {
            float newx = focus.position.y + (transform.position.y - focus.position.y) * Mathf.Cos(SPEED) - (transform.position.z - focus.position.z) * Mathf.Sin(SPEED);
            float newz = focus.position.z + (transform.position.y - focus.position.y) * Mathf.Sin(SPEED) + (transform.position.z - focus.position.z) * Mathf.Cos(SPEED);

            transform.position = new Vector3(transform.position.x, newx, newz);
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}

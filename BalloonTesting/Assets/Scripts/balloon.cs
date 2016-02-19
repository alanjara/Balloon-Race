using UnityEngine;
using System.Collections;

public class balloon : MonoBehaviour {
	Rigidbody rb;
	public float verticalThrust;
	public float horizontalThrust;
	public bool mouse;
	public float v, h;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		mouse = Input.GetMouseButton (0);
		if (mouse)
			rb.AddRelativeForce (Vector3.forward * verticalThrust, ForceMode.Acceleration);
		//else
			//rb.AddRelativeForce (-Vector3.forward / 10f, ForceMode.Force);
			//transform.position = Vector3.Lerp (transform.position, transform.position - Vector3.up * .01f, Time.deltaTime);
		v = Input.GetAxis("Vertical");
		h = Input.GetAxis("Horizontal");

		if(v > 0) rb.AddRelativeForce (Vector3.up * -horizontalThrust, ForceMode.Acceleration);
		else if (v < 0) rb.AddRelativeForce (Vector3.up * horizontalThrust, ForceMode.Acceleration);
		if(h > 0) rb.AddRelativeForce (Vector3.right * horizontalThrust, ForceMode.Acceleration);
		else if (h < 0) rb.AddRelativeForce (Vector3.right * -horizontalThrust, ForceMode.Acceleration);
	}
}

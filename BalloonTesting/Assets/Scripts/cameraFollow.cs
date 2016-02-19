using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {
	public GameObject target;
	public float FollowDistance = 7.5f;
	public float speed = 2f;
	public float distThres = .01f;
	public Vector3 newPos;
	public static cameraFollow S;
	void Awake()
	{
		S = this;
	}
	void Start()
	{
		newPos = Camera.main.transform.position;
		newPos.y = target.transform.position.y + 4f; ;
		newPos.z = target.transform.position.z - FollowDistance;
		Camera.main.transform.position = newPos;
	}
	void FixedUpdate()
	{
		newPos = Camera.main.transform.position;
		newPos.x = target.transform.position.x;
		newPos.y = target.transform.position.y + 4f;
		newPos.z = target.transform.position.z - FollowDistance;

		if (Mathf.Abs(newPos.z - Camera.main.transform.position.z) > distThres)
		{
			Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, newPos, Time.deltaTime * speed + Mathf.Abs(target.GetComponent<Rigidbody>().velocity.z) / 30f);
		}
		else
		{
			Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, newPos, Time.deltaTime * speed);
		}
		transform.LookAt (target.transform);
	}

}
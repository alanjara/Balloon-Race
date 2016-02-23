using UnityEngine;
using System.Collections;

public class RotationDamper : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    void AlignUpwards()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(new Vector3(270f, 0, 0)), Time.deltaTime * 10f);
        //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(270f, 0f, 0f), Time.deltaTime * 4f);
    }
    // Update is called once per frame
    void Update () {
        AlignUpwards();
	}
}

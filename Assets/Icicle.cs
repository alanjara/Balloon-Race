using UnityEngine;
using System.Collections;

public class Icicle : MonoBehaviour {
    Rigidbody rb;
    Vector3 initpos;
    Quaternion initquat;
    Collider col;
    public float respawntime = 10f;
    public float shaketime = 2f;
    public float stilltime = 2f;
    public float displacement = 0.1f;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        initpos = transform.position;
        initquat = transform.rotation;
        col = GetComponent<Collider>();
        StartCoroutine(doanim());
	}
    bool shaking = false;
    IEnumerator doanim()
    {
        while (true)
        {
            col.enabled = false;
            // still
            rb.isKinematic = true;
            shaking = false;
            transform.position = initpos;
            transform.rotation = initquat;
            yield return new WaitForSeconds(stilltime);
            // shake
            rb.isKinematic = true;
            shaking = true;
            yield return new WaitForSeconds(shaketime);
            col.enabled = true;
            // fall
            shaking = false;
            rb.isKinematic = false;
            yield return new WaitForSeconds(respawntime);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (shaking)
        {
            float xrand = Random.Range(-displacement, displacement);
            float yrand = Random.Range(-displacement, displacement);
            Vector3 newpos = new Vector3(initpos.x + xrand, initpos.y, initpos.z + yrand);
            transform.position = newpos;
        }
	}
}

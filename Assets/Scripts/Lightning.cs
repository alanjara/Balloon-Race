using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour {
    GameObject lightaa;
    Collider col;
    public float interval = 3f;
    public float duration = 0.5f;
    Light lite;
	// Use this for initialization
	void Start () {
        lightaa = transform.GetChild(0).gameObject;
        lite = lightaa.GetComponent<Light>();
        col = lightaa.GetComponent<Collider>();
        StartCoroutine(lightning());
        //light.SetActive(true);
        // col.enabled = false;
    }
	
    IEnumerator lightning()
    {
        while (true)
        {
            lightaa.SetActive(true);
            col.enabled = false;
            lite.enabled = true;

            yield return new WaitForSeconds(duration * 2);

            col.enabled = true;
            yield return new WaitForSeconds(duration);

            col.enabled = false;

            lite.enabled = false;

            lightaa.SetActive(false);
            yield return new WaitForSeconds(interval);
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}

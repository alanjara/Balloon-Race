using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour {
    GameObject light;
    public float interval = 3f;
    public float duration = 0.5f;
	// Use this for initialization
	void Start () {
        light = transform.GetChild(0).gameObject;
        StartCoroutine(lightning());
	}
	
    IEnumerator lightning()
    {
        while (true)
        {
            light.SetActive(true);

            yield return new WaitForSeconds(duration);
            light.SetActive(false);

            yield return new WaitForSeconds(interval);
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}

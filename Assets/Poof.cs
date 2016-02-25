using UnityEngine;
using System.Collections;

public class Poof : MonoBehaviour {
    public float speed = 1f;
	// Use this for initialization
	void Start () {
        targetScale = new Vector3(4f, 1.5f, 4f);
        fading = gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material;
        StartCoroutine(killyourself());
    }
    Material fading;


    IEnumerator killyourself()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }


    public Vector3 targetScale;
	// Update is called once per frame
	void Update () {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);
        Color nicememe = fading.GetColor("_Color");
        nicememe.a -= Time.deltaTime * 0.5f;
        fading.SetColor("_Color",nicememe);
	}
}

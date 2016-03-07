using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DangerFlash : MonoBehaviour {
    Image i;
	// Use this for initialization
	void Start () {
        i = GetComponent<Image>();
        StartCoroutine(animate());
	}

    IEnumerator animate()
    {
        for(int c = 0; c < 5; ++c)
        {
            i.enabled = true;
            yield return new WaitForSeconds(0.25f);
            i.enabled = false;
            yield return new WaitForSeconds(0.25f);
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

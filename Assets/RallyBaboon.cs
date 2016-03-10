using UnityEngine;
using System.Collections;

public class RallyBaboon : MonoBehaviour {
    Animator anm;
	// Use this for initialization
	void Start () {
        anm = GetComponent<Animator>();
        StartCoroutine(animate());
	}

    IEnumerator animate()
    {
        while (true)
        {
            float rand = Random.Range(1f, 3f);
            yield return new WaitForSeconds(rand);
            int rand2 = Mathf.CeilToInt(Random.Range(0.1f, 3f));
            anm.SetTrigger("Cheer" + rand2);
        }
    }
	
	// Update is called once per frame
	void Update () {
	   
	}
}

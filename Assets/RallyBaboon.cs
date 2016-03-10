using UnityEngine;
using System.Collections;

public class RallyBaboon : MonoBehaviour {
    Animator anm;
    //public bool top = false;
    Quaternion startrot;
	// Use this for initialization
	void Start () {
        anm = GetComponent<Animator>();
        StartCoroutine(animate());
        startrot = transform.rotation;
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
       /*
        if (top == true) {
            transform.position = transform.parent.transform.position + Vector3.up;
         //   transform.rotation = startrot;

        }
        */
	}
}

using UnityEngine;
using System.Collections;

public class ThrowFlame : MonoBehaviour {

    // Use this for initialization
    public GameObject fire;
    public float cooldown = 5f;
    public float start_time = 5f;
    private float timer;
    public GameObject flameStriker;
	void Start () {
        timer = start_time;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <=0)
        {
            timer = cooldown;
            Instantiate(fire, transform.position, transform.rotation);
            Invoke("fireStrike", 0.2f);
            Invoke("fireStrike", 0.4f);
            Invoke("fireStrike", 0.6f);
            Invoke("fireStrike", 0.8f);
        }
	}

    void fireStrike()
    {

        Instantiate(flameStriker, transform.position, transform.rotation);
    }
}

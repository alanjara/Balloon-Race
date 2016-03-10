using UnityEngine;
using System.Collections;

public class ThrowMetorSwarm : MonoBehaviour {

    // Use this for initialization
    public GameObject fire;
    public float cooldown = 5f;
    public float start_time = 5f;
    private float timer;
    void Start()
    {
        timer = start_time;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = cooldown;
            Instantiate(fire, transform.position, Quaternion.identity);
        }
    }

    
}

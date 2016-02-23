using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PowerUp {rocket_boost,wind_blast,fire_ball,fuel,none };

public class balloon_base : MonoBehaviour
{
    //powerup gameobjects
    public GameObject firebolt;
    public GameObject spawn;
    public GameObject wind_wave;

    //wind blast consts
    private const float wind_power = 0.2f;
    private const float wind_radius = 50f;

    Rigidbody rb;
    float boost_count = 0;
    protected bool fire;
    public float verticalThrust;
    public float horizontalThrust;
    public float up;
    public float v, h;
    protected PowerUp powerup;
    protected int _fuel, _life, _speed;
    public Vector3 LastCheckpoint = new Vector3(-5f, 5f, -22f);
    bool deadBaloon = false;
    public ParticleSystem flameup;
    public GameObject bleed;
    // set get classes
    public int fuel
    {
        get
        {
            return _fuel;
        }
        set
        {
            _fuel = value;
        }
    }
    public int life
    {
        get
        {
            return _life;
        }
        set
        {
            _life = value;
            if (_life == 0) 
            {
                deadBaloon = true;
                StartCoroutine(dieAnimation());
            }
        }
    }
    public int speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        life = 2;
        rb = GetComponent<Rigidbody>();
        powerup = PowerUp.wind_blast;
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (deadBaloon)
        {
            return;
        }
        if (collision.gameObject.tag == "Dangerous")
        {
            life--;
            for (int c = 0; c < collision.contacts.Length; ++c)
            {
                GameObject g = Instantiate(bleed, collision.contacts[c].point, transform.rotation) as GameObject;
                g.transform.SetParent(this.transform);
                if (life != 0)
                    Destroy(g, 2f);
            }
        }
    }

    public GameObject respawn;
    IEnumerator dieAnimation()
    {
        yield return new WaitForSeconds(5f);
        //GameObject g = Instantiate(respawn, LastCheckpoint, Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
        // cameraFollow.S.target = g.transform.GetChild(0).gameObject;
        //  Destroy(this.transform.parent.gameObject);
        Application.LoadLevel(Application.loadedLevel);
    }
    void AlignUpwards()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(new Vector3(270f, 0, 0)), Time.deltaTime * 10f);
        //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(270f, 0f, 0f), Time.deltaTime * 4f);
    }

    void FixedUpdate()
    {
        if (boost_count > 0)
        {
            boost_count -= Time.deltaTime;
            if (boost_count <0)
            {
                boost_count = 0;
                horizontalThrust /= 5;
            }

        }
        if (deadBaloon)
        {
            return;
        }
        if (up > 0)
        {
            rb.AddRelativeForce(Vector3.forward * verticalThrust, ForceMode.Acceleration);
            flameup.enableEmission = true;
            up = 0;
        }
        else
        {
            flameup.enableEmission = false;
        }
        if (v > 0) rb.AddRelativeForce(Vector3.up * -horizontalThrust, ForceMode.Acceleration);
        else if (v < 0) rb.AddRelativeForce(Vector3.up * horizontalThrust, ForceMode.Acceleration);
        if (h > 0) rb.AddRelativeForce(Vector3.right * horizontalThrust, ForceMode.Acceleration);
        else if (h < 0) rb.AddRelativeForce(Vector3.right * -horizontalThrust, ForceMode.Acceleration);
        if (fire )
            usePowerup();
        AlignUpwards();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    //manage power_ups
    public void pickupPowerup (PowerUp powerup)
    {
        this.powerup = powerup;
    }

    private void shockWave()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, wind_radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (hit.gameObject == gameObject)
                continue;
            if (rb != null)
                rb.AddExplosionForce(wind_power, explosionPos, wind_radius, 3.0F);
        }
    }
    public void usePowerup()
    {
        switch (powerup)
        {
            case PowerUp.fire_ball:
                //shoot a series of fireballs
                Instantiate(firebolt, spawn.transform.position, spawn.transform.rotation);
                break;
            case PowerUp.fuel:
                break;
            case PowerUp.rocket_boost:
                ////speed up for a while
                boost_count = 5;
                horizontalThrust *= 5;
                powerup = PowerUp.none;
                break;
            case PowerUp.wind_blast:
                //a force pushing everything away
                for (int i = 0; i < 100; i++)
                {
                    Invoke("shockWave", i/100f);
                }

                Instantiate(wind_wave, transform.position, Quaternion.identity);

                break;
            default:
                //dont own a powerup
                break;
        }
        
    }

    public void OnTriggerEnter (Collider coll)
    {
        if (coll.tag == "Pickups")
        {
            float dice = Random.value;
            if (dice < 0.33)
            {
                pickupPowerup(PowerUp.fire_ball);
            }
            else if(dice<0.66)
            {
                pickupPowerup(PowerUp.rocket_boost);
            }
            else
            {
                pickupPowerup(PowerUp.wind_blast);
            }
            Destroy(coll.gameObject);
        }
    }
}

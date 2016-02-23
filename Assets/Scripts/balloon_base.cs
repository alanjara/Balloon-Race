using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class balloon_base : MonoBehaviour {
    //powerup gameobjects
    public GameObject firebolt;
    Rigidbody rb;
    public GameObject wind_wave;

    //wind blast consts
    private const float wind_power = 0.2f;
    private const float wind_radius = 50f;
    public GameObject spawn;
    public float verticalThrust;
    public float horizontalThrust;
    float boost_count = 0;
    public float horizontalThrustBoosted;
    public float risingForce;
    protected bool fire;
    public float v, h;
    float floatForce;
    float horT, verT;
    Vector3 size;
    public float expansion;
    public float max_height_modifier = 300;
    public Vector3 LastCheckpoint = new Vector3(-5f, 5f, -22f);
    public float up;
    bool deadBaloon = false;
    public ParticleSystem flameup;
    public ParticleSystem speedboost;
    protected PowerUp powerup;
    protected int _fuel, _life, _speed;
    public GameObject bleed;
    public LayerMask balloon_layer;
    public GameObject minilightning;

    public void OnCollisionEnter(Collision collision) {
        if (deadBaloon) {
            return;
        }
        if (collision.gameObject.name == "SKY") {
            deadBaloon = true;
            Instantiate(minilightning, collision.contacts[0].point + Vector3.up * -1f, Quaternion.Euler(new Vector3(0, -90f, 0)));
            for (int c = 0; c < collision.contacts.Length; ++c) {
                GameObject g = Instantiate(bleed, collision.contacts[c].point, transform.rotation) as GameObject;
                g.transform.SetParent(this.transform);
            }
            StartCoroutine(dieAnimation());
            return;
        }
        if (collision.gameObject.tag == "Dangerous") {
            life--;
            for (int c = 0; c < collision.contacts.Length; ++c) {
                GameObject g = Instantiate(bleed, collision.contacts[c].point, transform.rotation) as GameObject;
                g.transform.SetParent(this.transform);
                if (life != 0)
                    Destroy(g, 2f);
            }
        }
    }
    // set get classes
    public int fuel {
        get {
            return _fuel;
        }
        set {
            _fuel = value;
        }
    }

    public GameObject respawn;
    public int life {
        get {
            return _life;
        }
        set {
            _life = value;
            if (_life == 0) {
                deadBaloon = true;
                StartCoroutine(dieAnimation());
            }
        }
    }

    IEnumerator dieAnimation() {
        yield return new WaitForSeconds(5f);
        
        //GameObject g = Instantiate(respawn, LastCheckpoint, Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
        // cameraFollow.S.target = g.transform.GetChild(0).gameObject;
        //  Destroy(this.transform.parent.gameObject);
        Application.LoadLevel(Application.loadedLevel);
    }
    public int speed {
        get {
            return speed;
        }
        set {
            speed = value;
        }
    }

    // Use this for initialization
    void Start() {
        life = 2;
        floatForce = risingForce;
        rb = GetComponent<Rigidbody>();
        horT = horizontalThrust;
        verT = verticalThrust;
        speedboost.enableEmission = false;
        size = transform.localScale;
        powerup = PowerUp.fire_ball;
    }


    void AlignUpwards() {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(new Vector3(270f, 0, 0)), Time.deltaTime * 10f);
        //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(270f, 0f, 0f), Time.deltaTime * 4f);
    }

    void FixedUpdate() {
        if (boost_count > 0) {
            boost_count -= Time.deltaTime;
            if (boost_count < 0) {
                boost_count = 0;
                horizontalThrust /= 5;
            }

        }
        if (deadBaloon) {
            return;
        }
        /*
        Vector3 modifier = new Vector3( rb.velocity.y *size.x, size.y, rb.velocity.y*size.z);
        transform.localScale = Vector3.Lerp(transform.localScale, modifier,Time.deltaTime);
        */
        if (floatForce > risingForce)
            floatForce -= verticalThrust * Time.deltaTime;
        if (floatForce < risingForce)
            floatForce = risingForce;
        if (up > 0) {
            // rb.AddForceAtPosition(Vector3.up * verticalThrust, transform.up+transform.position,ForceMode.Acceleration);
            floatForce += verticalThrust;
            flameup.enableEmission = true;
            up = 0;
        } else {
            flameup.enableEmission = false;
        }
        if (v > 0)
            rb.AddForceAtPosition(Vector3.forward * horT, transform.position, ForceMode.Acceleration);
        else if (v < 0)
            rb.AddForceAtPosition(Vector3.forward * -horT, transform.position, ForceMode.Acceleration);
        if (h > 0)
            rb.AddForceAtPosition(Vector3.right * horT, transform.position, ForceMode.Acceleration);
        else if (h < 0)
            rb.AddForceAtPosition(Vector3.right * -horT, transform.position, ForceMode.Acceleration);

        if (floatForce > risingForce + verticalThrust)
            floatForce = risingForce + verticalThrust;
        floatForce = floatForce * (1 - transform.position.y / max_height_modifier) * (1 - transform.position.y / max_height_modifier);
        rb.AddForceAtPosition(Vector3.up * floatForce, transform.up + transform.position, ForceMode.Acceleration);

        // AlignUpwards();
        if (fire)
            usePowerup();
    }

    // Update is called once per frame
    public virtual void Update() {

    }

    Coroutine boostingcr;

    IEnumerator getboost()
    {
        horT = horizontalThrustBoosted;
        speedboost.enableEmission = true;
        yield return new WaitForSeconds(3f);
        horT = horizontalThrust;
        speedboost.enableEmission = false;
    }

    public void OnTriggerEnter(Collider coll) {
        if(coll.tag == "Boost")
        {
            if(boostingcr != null)
            {
                StopCoroutine(boostingcr);
            }
            boostingcr = StartCoroutine(getboost());
        }
        if (coll.tag == "Pickups") {
            float dice = Random.value;
            if (dice < 0.33) {
                pickupPowerup(PowerUp.fire_ball);
            } else if (dice < 0.66) {
                pickupPowerup(PowerUp.rocket_boost);
            } else {
                pickupPowerup(PowerUp.wind_blast);
            }
            Destroy(coll.gameObject);
        }
    }

    //manage power_ups
    public void pickupPowerup(PowerUp powerup) {
        this.powerup = powerup;
    }

    private void shockWave() {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, wind_radius,balloon_layer);
        foreach (Collider hit in colliders) {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (hit.gameObject == gameObject)
                continue;
            if (rb != null)
                rb.AddExplosionForce(wind_power, explosionPos, wind_radius, 3.0F);
        }
    }
    public void usePowerup() {
        switch (powerup) {
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
                for (int i = 0; i < 100; i++) {
                    Invoke("shockWave", i / 100f);
                }

                Instantiate(wind_wave, transform.position, Quaternion.identity);

                break;
            default:
                //dont own a powerup
                break;
        }

    }
}

public enum PowerUp { rocket_boost, wind_blast, fire_ball, fuel, none };

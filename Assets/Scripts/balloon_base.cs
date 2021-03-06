﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class balloon_base : MonoBehaviour {
    public aimingControl aim_control;
    public GameObject UICANVAS;
    //powerup gameobjects
    public GameObject firebolt;
    public GameObject absorb_wave;
    Rigidbody rb;
    public GameObject wind_wave;
    public int my_number;
    //wind blast consts
    private const float wind_power = 1.5f;
    private const float wind_radius = 200f;
    GameObject spawn;
    public float verticalThrust = 60;
    public float horizontalThrust = 20;
    float boost_count = 0;
    public float horizontalThrustBoosted = 40;
    public float risingForce = 5;
    protected bool fire;
    protected bool fire_pressed = false;
    public float fire_float;
    public float v, h;
    float floatForce;
    float horT, verT;
    Vector3 size;
    public float expansion;
    public float max_height_modifier = 300;
    public Vector3 LastCheckpoint = new Vector3(-5f, 5f, -22f);
    public float up;
    bool deadBaloon = false;
    ParticleSystem flameup;
    ParticleSystem speedboost;
    public PowerUp powerup;
    protected int _fuel, _life, _speed;
    public GameObject bleed;
    public LayerMask balloon_layer;
    public GameObject minilightning;
    public GameObject dangerIcon;
    public Vector3 race_forward = new Vector3(0, 0, 1);
    public Vector3 race_right = new Vector3(1, 0, 0);
    Vector3 forward_control = new Vector3(0, 0, 1);
    public GameObject powerupUI;
    public Vector3 race_up = new Vector3(0, 1, 0);
    Transform poweruptransform;
    BalloonBaboon bb;
    GameObject basket;
    public void setControlDirection(Transform setter) {
        race_forward = setter.forward;
        race_right = setter.right;
        race_up = setter.up;
        forward_control = race_forward;
        forward_control.y = 0;
        forward_control.Normalize();
    }
    Image powerupimage;
    public Sprite iconFire, iconWind, iconSpeed, iconSuck;
    controls my_inputs;
    

    public GameObject LEFTPOWERUP, RIGHTPOWERUP;
    
    public void AlignUIPowerup() {
        if(my_number == 1)
        {
            poweruptransform.position = LEFTPOWERUP.transform.position;
        } else if(RIGHTPOWERUP!=null)
        {
            poweruptransform.position = RIGHTPOWERUP.transform.position;
        }
    }

    struct controls {
        public string up, vert, hor;
        public string fire;
    };
    public void OnCollisionEnter(Collision collision) {
        if (deadBaloon || immune) {
            return;
        }
        if (collision.gameObject.name == "SKY") {
            deadBaloon = true;
            Instantiate(minilightning, collision.contacts[0].point + Vector3.up * -1f, Quaternion.Euler(new Vector3(0, -90f, 0)));
            for (int c = 0; c < collision.contacts.Length; ++c) {
                GameObject g = Instantiate(bleed, collision.contacts[c].point, transform.rotation) as GameObject;
                g.transform.SetParent(this.transform);
                explosions.Add(g);
            }
            StartCoroutine(dieAnimation());
            return;
        }
        if (collision.gameObject.tag == "Dangerous") {
            // life--;
            for (int c = 0; c < collision.contacts.Length; ++c) {
                GameObject g = Instantiate(bleed, collision.contacts[c].point, transform.rotation) as GameObject;
                g.transform.SetParent(this.transform);
                explosions.Add(g);
                //if (life != 0)
                //Destroy(g, 2f);
            }

            deadBaloon = true;
            StartCoroutine(dieAnimation());
        }
    }
    List<GameObject> explosions = new List<GameObject>();
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
    public bool immune = false;
    IEnumerator becomeImmune(float secs) {
        immune = true;
        yield return new WaitForSeconds(secs);
        immune = false;
    }

    IEnumerator dieAnimation() {
        PLAYCLIP("die", 1f);
        bb.Damaged();
        GameObject g = Instantiate(dangerIcon) as GameObject;
        g.transform.SetParent(UICANVAS.transform);
        Vector3 where = Camera.allCameras[my_number - 1].WorldToScreenPoint(transform.position);
        g.transform.position = where;
        yield return new WaitForSeconds(3f);
        StartCoroutine(becomeImmune(1f));
        deadBaloon = false;
        for (int c = 0; c < explosions.Count; ++c) {
            Destroy(explosions[c]);

        }
        explosions.Clear();
        //GameObject g = Instantiate(respawn, LastCheckpoint, Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
        // cameraFollow.S.target = g.transform.GetChild(0).gameObject;
        //  Destroy(this.transform.parent.gameObject);
        //Application.LoadLevel(Application.loadedLevel);

    }

    public GameObject poofs;
    Dictionary<GameObject, bool> pooferinos = new Dictionary<GameObject, bool>();
    IEnumerator makepoofs() {
        while (true) {
            yield return new WaitForSeconds(2f);
            GameObject didem = Instantiate(poofs, transform.position - race_forward * 2f, Quaternion.LookRotation(race_right, race_forward)) as GameObject; //don't think i didn't see that -90
            pooferinos[didem] = true;
        }
    }
    public int speed {
        get {
            return speed;
        }
        set {
            speed = value;
        }
    }

    void setCorrectPowerupImage() {
        powerupimage.enabled = true;
        switch (powerup) {
            case PowerUp.fire_ball:
                powerupimage.sprite = iconFire;
                break;
            case PowerUp.Wind_suck:
                powerupimage.sprite = iconSuck;
                break;
            case PowerUp.rocket_boost:
                powerupimage.sprite = iconSpeed;
                break;
            case PowerUp.wind_blast:
                powerupimage.sprite = iconWind;
                break;
            default:
                powerupimage.enabled = false;
                break;
        }
    }

    // Use this for initialization
    void Start() {
        UICANVAS = GameObject.FindGameObjectWithTag("UI");
        GameObject g = Instantiate(powerupUI) as GameObject;
        g.transform.SetParent(UICANVAS.transform);
        poweruptransform = g.transform;
        powerupimage = g.GetComponent<Image>();

        LEFTPOWERUP = GameObject.Find("LEFTPOWERUP");
        RIGHTPOWERUP = GameObject.Find("RIGHTPOWERUP");

        my_inputs.up = string.Format("Up{0}", my_number);
        my_inputs.vert = string.Format("Vertical{0}", my_number);
        my_inputs.hor = string.Format("Horizontal{0}", my_number);
        my_inputs.fire = string.Format("Fire{0}", my_number);
        print(my_inputs.up);
        print(my_number);
        flameup = gameObject.transform.Find("Flame").gameObject.GetComponent<ParticleSystem>();
        speedboost = gameObject.transform.Find("Speed").gameObject.GetComponent<ParticleSystem>();
        spawn = gameObject.transform.Find("spawn").gameObject;
        balloon_layer = LayerMask.GetMask("Balloon");
        basket = transform.parent.Find("basket").gameObject;
        life = 1;
        floatForce = risingForce;
        rb = GetComponent<Rigidbody>();
        horT = horizontalThrust;
        verT = verticalThrust;
        speedboost.enableEmission = false;
        size = transform.localScale;
        powerup = PowerUp.rocket_boost;
        bb = transform.FindChild("RallyingBaboon").gameObject.GetComponent<BalloonBaboon>();

        StartCoroutine(makepoofs());
        forward_control = race_forward;
        setCorrectPowerupImage();

        aim_control.ifAim = false;
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
            floatForce += verticalThrust*up + (horT-horizontalThrust)*4;
            flameup.enableEmission = true;
            up = 0;
        } else {
            if (rb.velocity.y < 0)
                flameup.enableEmission = false;
        }
        if (v > 0)
            rb.AddForceAtPosition(forward_control * horT, transform.position, ForceMode.Acceleration);
        else if (v < 0)
            rb.AddForceAtPosition(forward_control * -horT, transform.position, ForceMode.Acceleration);
        if (h > 0)
            rb.AddForceAtPosition(race_right * horT, transform.position, ForceMode.Acceleration);
        else if (h < 0)
            rb.AddForceAtPosition(race_right * -horT, transform.position, ForceMode.Acceleration);

        if (floatForce > risingForce + verticalThrust)
            floatForce = risingForce + verticalThrust;
        floatForce = floatForce * (1 - transform.position.y / max_height_modifier) * (1 - transform.position.y / max_height_modifier);
        rb.AddForceAtPosition(Vector3.up * floatForce, transform.up + transform.position, ForceMode.Acceleration);
        RaycastHit[] chasers;
        chasers = Physics.SphereCastAll(transform.position, 1, Vector3.Normalize(-1 * rb.velocity), rb.velocity.magnitude * 10, balloon_layer);
        /*
        Debug.DrawLine(transform.position, Vector3.Normalize(-1 * rb.velocity) * rb.velocity.magnitude * 10, Color.red);
        for (int i = 0; i < chasers.Length; i++) {
            if (chasers[i].distance > 0.1)
                chasers[i].collider.gameObject.GetComponent<balloon_base>().draft(rb.velocity * 3 * (1 - chasers[i].distance / (rb.velocity.magnitude * 10)));
        }
        */
        if (fire)
            usePowerup();

        AlignUIPowerup();
    }

    // Update is called once per frame
    void Update() {
        up = Input.GetAxis(my_inputs.up);
        v = Input.GetAxis(my_inputs.vert);
        h = Input.GetAxis(my_inputs.hor);
        fire_float = Input.GetAxis(my_inputs.fire);
        if (fire_float > 0)
            fire_float = Input.GetAxisRaw(my_inputs.fire);
        if (fire_float > 0 && !fire_pressed) {
            fire = true;
            fire_pressed = true;
        } else {
            fire = false;
        }

        if (fire_float <= 0) {
            fire_pressed = false;
        }
    }

    Coroutine boostingcr;

    IEnumerator getboost() {
        horT = horizontalThrustBoosted;
        speedboost.enableEmission = true;
        yield return new WaitForSeconds(3f);
        horT = horizontalThrust;
        speedboost.enableEmission = false;
    }

    public static void PLAYCLIP(string clip, float vol)
    {
        GameObject go = new GameObject("Bubby");
        go.transform.position = Camera.main.transform.position;
        go.AddComponent<AudioSource>();
        AudioSource asrc = go.GetComponent<AudioSource>();
        asrc.spatialBlend = 0.1f;
        asrc.clip = Resources.Load(clip) as AudioClip;
        asrc.volume = 1f;
        asrc.Play();
        Destroy(go, asrc.clip.length);
    }

    public void boost() {
        if (boostingcr != null) {
            StopCoroutine(boostingcr);
        }
        bb.Crouch();
        PLAYCLIP("boost", 1f);
        boostingcr = StartCoroutine(getboost());
    }
    /*
    public void draft(Vector3 tailwind) {
        rb.AddForceAtPosition(tailwind, transform.position, ForceMode.Acceleration);
    }
     */
    public void OnTriggerEnter(Collider coll) {
        if (coll.tag == "Boost") {
            if (pooferinos.ContainsKey(coll.gameObject.transform.parent.gameObject)) {
                if (pooferinos.Count > 100) {
                    pooferinos.Clear();
                }
                print("HIt your own poof");
                return;
            }
            boost();
        }
        if (coll.tag == "Pickups") {
            float dice = Random.value;
            if (dice < 0.25) {
                pickupPowerup(PowerUp.fire_ball);
            } else if (dice < 0.50) {
                pickupPowerup(PowerUp.rocket_boost);
            } else if(dice < 0.75){
                pickupPowerup(PowerUp.wind_blast);
            } else
            {
                pickupPowerup(PowerUp.Wind_suck);
            }
            Destroy(coll.gameObject);

            setCorrectPowerupImage();
        }
        if (coll.gameObject.tag == "Dangerous") {
            // life--;
            for (int c = 0; c < 3; ++c) {
                GameObject g = Instantiate(bleed, transform.position + transform.up, transform.rotation) as GameObject;
                g.transform.SetParent(this.transform);
                explosions.Add(g);
                //if (life != 0)
                //Destroy(g, 2f);
            }

            deadBaloon = true;
            StartCoroutine(dieAnimation());
        }
        if (coll.tag == "DirectionChanger") {

            setControlDirection(coll.transform.parent.Find("Direction").transform);
        }
    }

    //manage power_ups
    public void pickupPowerup(PowerUp powerup) {
        PLAYCLIP("pickuppowerup", 1f);
        this.powerup = powerup;
        if (powerup == PowerUp.fire_ball)
        {
            aim_control.setIfAim(true);
        }
    }

    private void shockWave() {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, wind_radius, balloon_layer);
        foreach (Collider hit in colliders) {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (hit.gameObject == gameObject)
                continue;
            if (rb != null)
                rb.AddExplosionForce(wind_power, explosionPos, wind_radius, 3.0F);
        }
    }

    private void suckWave() {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, wind_radius, balloon_layer);
        foreach (Collider hit in colliders) {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (hit.gameObject == gameObject)
                continue;
            if (rb != null)
                rb.AddExplosionForce(-wind_power, explosionPos, wind_radius, 3.0F);
        }
    }

    public void usePowerup() {
        switch (powerup) {
            case PowerUp.fire_ball:
                //shoot a series of fireballs
                if (aim_control.ifAim && aim_control.angleSmall)
                {
                    PLAYCLIP("fireball", 1f);
                    GameObject fire_bolt = Instantiate(firebolt, transform.position+race_forward*2, spawn.transform.rotation) as GameObject;
                    fire_bolt.GetComponent<DigitalRuby.PyroParticles.FireProjectileScript>().setTarget(aim_control.target);
                    aim_control.ifAim = false;
                    powerup = PowerUp.none;

                }
                break;
            case PowerUp.Wind_suck:
                // a force pulling everything together
                for (int i = 0; i < 100; i++) {
                    Invoke("suckWave", i / 100f);
                }
                PLAYCLIP("blow", 1f);
                Instantiate(absorb_wave, transform.position, Quaternion.identity);

                aim_control.ifAim = false;
                powerup = PowerUp.none;
                break;
            case PowerUp.rocket_boost:
                ////speed up for a while
                boost();

                aim_control.ifAim = false;
                powerup = PowerUp.none;
                break;
            case PowerUp.wind_blast:
                //a force pushing everything away
                for (int i = 0; i < 100; i++) {
                    Invoke("shockWave", i / 100f);
                }

                PLAYCLIP("blow", 1f);
                Instantiate(wind_wave, transform.position, Quaternion.identity);

                aim_control.ifAim = false;
                powerup = PowerUp.none;
                break;
            default:
                aim_control.ifAim = false;
                //dont own a powerup
                break;
        }
        bb.UsedItem();
        setCorrectPowerupImage();
    }
}

public enum PowerUp { rocket_boost, wind_blast, fire_ball, Wind_suck, none };


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class balloon_base_AI : MonoBehaviour {
    public GameObject UICANVAS;
    //powerup gameobjects
    public GameObject firebolt;
    public GameObject absorb_wave;
    Rigidbody rb;
    public GameObject wind_wave;
    public int my_number;
    //wind blast consts
    private const float wind_power = 0.2f;
    private const float wind_radius = 50f;
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
    protected PowerUp powerup;
    protected int _fuel, _life, _speed;
    public GameObject bleed;
    public LayerMask balloon_layer;
    public GameObject minilightning;

    public Vector3 race_forward = new Vector3(0, 0, 1);
    public Vector3 race_right = new Vector3(1, 0, 0);
    Vector3 forward_control = new Vector3(0, 0, 1);
    public GameObject powerupUI;
    public Vector3 race_up = new Vector3(0, 1, 0);
    Transform poweruptransform;
    GameObject Ring;
    public void setControlDirection(Transform setter) {
        race_forward = setter.forward;
        race_right = setter.right;
        race_up = setter.up;
        forward_control = race_forward;
        forward_control.y = 0;
        forward_control.Normalize();
    }
    Image powerupimage;

    int ring = 1;

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



    // Use this for initialization
    void Start() {
        UICANVAS = GameObject.FindGameObjectWithTag("UI");

        flameup = gameObject.transform.Find("Flame").gameObject.GetComponent<ParticleSystem>();
        speedboost = gameObject.transform.Find("Speed").gameObject.GetComponent<ParticleSystem>();
        spawn = gameObject.transform.Find("spawn").gameObject;
        balloon_layer = LayerMask.GetMask("Balloon");
        Ring = transform.parent.transform.parent.Find("Ring1").gameObject;
        life = 1;
        floatForce = risingForce;
        rb = GetComponent<Rigidbody>();
        horT = horizontalThrust;
        verT = verticalThrust;
        speedboost.enableEmission = false;
        size = transform.localScale;
        powerup = PowerUp.wind_blast;

        StartCoroutine(makepoofs());

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
        if (transform.position.y < Ring.transform.position.y) {
            // rb.AddForceAtPosition(Vector3.up * verticalThrust, transform.up+transform.position,ForceMode.Acceleration);
            floatForce += verticalThrust + (horT - horizontalThrust) * 4;
            flameup.enableEmission = true;
            up = 0;
        } else {
            if (rb.velocity.y < 0)
                flameup.enableEmission = false;
        }
        Vector3 dir = Ring.transform.position - transform.position;
        dir.y = 0;
        dir.Normalize();
        rb.AddForceAtPosition(dir * horT /50, transform.position, ForceMode.VelocityChange);
        

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
  
    }

    // Update is called once per frame
    void Update() {

    }

    Coroutine boostingcr;

    IEnumerator getboost() {
        horT = horizontalThrustBoosted;
        speedboost.enableEmission = true;
        yield return new WaitForSeconds(3f);
        horT = horizontalThrust;
        speedboost.enableEmission = false;
    }
    public void boost() {
        if (boostingcr != null) {
            StopCoroutine(boostingcr);
        }
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
            if (coll.transform.parent.name == Ring.name) {
                ring++;
                Ring = transform.parent.transform.parent.Find(string.Format("Ring{0}", ring)).gameObject;
            }
            boost();
        }
        

    }

    //manage power_ups
    public void pickupPowerup(PowerUp powerup) {
        this.powerup = powerup;
    }




}


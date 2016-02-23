using UnityEngine;
using System.Collections;

public class balloon : MonoBehaviour
{
    Rigidbody rb;
    public float verticalThrust;
    public float horizontalThrust;
    public float horizontalThrustBoosted;
    public float risingForce;

    public bool mouse;
    public float v, h;
    float floatForce;
    float horT, verT;
    Vector3 size;
    public float expansion;
    public float max_height_modifier=300;
    public Vector3 LastCheckpoint = new Vector3(-5f, 5f, -22f);

    // Use this for initialization
    void Start()
    {
        floatForce = risingForce;
        rb = GetComponent<Rigidbody>();
        horT = horizontalThrust;
        verT = verticalThrust;
        speedboost.enableEmission = false;
        size = transform.localScale;
    }
    public bool deadBaloon = false;
    public ParticleSystem flameup;
    public ParticleSystem speedboost;
    public GameObject bleed;
    public GameObject minilightning;
    public void OnCollisionEnter(Collision collision)
    {
        if (deadBaloon)
        {
            return;
        }
        if(collision.gameObject.name == "SKY")
        {
            deadBaloon = true;
            Instantiate(minilightning, collision.contacts[0].point + Vector3.up * -1f, Quaternion.Euler(new Vector3(0,-90f,0)));
            for (int c = 0; c < collision.contacts.Length; ++c)
            {
                GameObject g = Instantiate(bleed, collision.contacts[c].point, transform.rotation) as GameObject;
                g.transform.SetParent(this.transform);
            }
            StartCoroutine(dieAnimation());
            return;
        }
        if (collision.gameObject.tag == "Dangerous")
        {
            deadBaloon = true;
            for (int c = 0; c < collision.contacts.Length; ++c)
            {
                GameObject g = Instantiate(bleed, collision.contacts[c].point, transform.rotation) as GameObject;
                g.transform.SetParent(this.transform);
            }
            StartCoroutine(dieAnimation());
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
        if (deadBaloon)
        {
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
        if (mouse)
        {
           // rb.AddForceAtPosition(Vector3.up * verticalThrust, transform.up+transform.position,ForceMode.Acceleration);
            floatForce += verticalThrust;
            flameup.enableEmission = true;
        }
        else
        {
            flameup.enableEmission = false;
        }
        if (v > 0)
            rb.AddForceAtPosition(Vector3.forward * horT,  transform.position, ForceMode.Acceleration);
        else if (v < 0)
            rb.AddForceAtPosition(Vector3.forward * -horT,  transform.position, ForceMode.Acceleration);
        if (h > 0)
            rb.AddForceAtPosition(Vector3.right * horT,  transform.position, ForceMode.Acceleration);
        else if (h < 0)
            rb.AddForceAtPosition(Vector3.right * -horT,  transform.position, ForceMode.Acceleration);

        if (floatForce > risingForce + verticalThrust)
            floatForce = risingForce + verticalThrust;
        floatForce = floatForce*(1 - transform.position.y / max_height_modifier) * (1 - transform.position.y / max_height_modifier);
        rb.AddForceAtPosition(Vector3.up * floatForce, transform.up + transform.position, ForceMode.Acceleration);

       // AlignUpwards();
    }

    // Update is called once per frame
    void Update()
    {
        mouse = Input.GetMouseButton(0);
        //else
        //rb.AddForceAtPosition (-Vector3.forward / 10f, ForceMode.Force);
        //transform.position = Vector3.Lerp (transform.position, transform.position - Vector3.up * .01f, Time.deltaTime);
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");

    }
    Coroutine boostcr;

    IEnumerator boost()
    {
        horT = horizontalThrustBoosted;
        speedboost.enableEmission = true;
        yield return new WaitForSeconds(3f);
        horT = horizontalThrust;
        speedboost.enableEmission = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Boost")
        {
            if(boostcr != null)
            {
                StopCoroutine(boostcr);
            }
            boostcr = StartCoroutine(boost());
        }
    }
}

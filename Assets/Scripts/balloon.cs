using UnityEngine;
using System.Collections;

public class balloon : MonoBehaviour
{
    Rigidbody rb;
    public float verticalThrust;
    public float horizontalThrust;
    public float horizontalThrustBoosted;
    public bool mouse;
    public float v, h;
    float horT, verT;
    public Vector3 LastCheckpoint = new Vector3(-5f, 5f, -22f);

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        horT = horizontalThrust;
        verT = verticalThrust;
        speedboost.enableEmission = false;
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
        if (mouse)
        {
            rb.AddRelativeForce(Vector3.forward * verticalThrust, ForceMode.Acceleration);
            flameup.enableEmission = true;
        }
        else
        {
            flameup.enableEmission = false;
        }
        if (v > 0) rb.AddRelativeForce(Vector3.up * -horT, ForceMode.Acceleration);
        else if (v < 0) rb.AddRelativeForce(Vector3.up * horT, ForceMode.Acceleration);
        if (h > 0) rb.AddRelativeForce(Vector3.right * horT, ForceMode.Acceleration);
        else if (h < 0) rb.AddRelativeForce(Vector3.right * -horT, ForceMode.Acceleration);
        AlignUpwards();
    }

    // Update is called once per frame
    void Update()
    {
        mouse = Input.GetMouseButton(0);
        //else
        //rb.AddRelativeForce (-Vector3.forward / 10f, ForceMode.Force);
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

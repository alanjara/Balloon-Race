using UnityEngine;
using System.Collections;

public class MovingDanger : MonoBehaviour {
    public float Speed = 3f;
    public float threshold = 1f;
    public Transform patrolPos1, patrolPos2;
    public float changeTime = 0.5f;
    public bool isBird = true;
    Quaternion rotfor, rotback;
	// Use this for initialization
	void Start () {
        rotfor = transform.localRotation;
        rotback = Quaternion.Euler(new Vector3(0,-180f,0));
	}


    bool moving = true;
    IEnumerator changin()
    {
        moving = false;
        yield return new WaitForSeconds(changeTime);
        moving = true;
    }

    public bool goingto1 = true;
    void FixedUpdate()
    {
        if (!moving)
        {
            return;
        }
        if (goingto1)
        {
            if (isBird)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, rotfor, Time.deltaTime * 6f);
            }
            transform.position += (patrolPos1.position - transform.position).normalized * Speed;
            if ((transform.position - patrolPos1.position).magnitude <= threshold)
            {
                goingto1 = false;
                StartCoroutine(changin());
            }
        } else
        {
            if (isBird)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, rotback, Time.deltaTime * 6f);
            }
            transform.position += (patrolPos2.position - transform.position).normalized * Speed;
            if((transform.position - patrolPos2.position).magnitude <= threshold)
            {
                goingto1 = true;
                StartCoroutine(changin());
            }
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class balloon1 : balloon_base
{
   
    public override void Update()
    {
        up = Input.GetAxis("Up1");
        //else
        //rb.AddRelativeForce (-Vector3.forward / 10f, ForceMode.Force);
        //transform.position = Vector3.Lerp (transform.position, transform.position - Vector3.up * .01f, Time.deltaTime);
        v = Input.GetAxis("Vertical1");
        h = Input.GetAxis("Horizontal1");
        fire = Input.GetKeyDown(KeyCode.F);

    }
}

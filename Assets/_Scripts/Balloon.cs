using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour {
    public float rise_force = 150;
    Rigidbody body;
    public float forward;
    bool turn = false;
    Vector3 torque;
    // Use this for initialization
    void Start() {
        body = GetComponent<Rigidbody>();
        Vector3 thrust = transform.forward * forward;
        body.AddForce(thrust);
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector3 float_force = Vector3.up;
        float_force = float_force * rise_force * (1 - transform.position.y / 300) * (1 - transform.position.y / 300);
        body.AddForceAtPosition(float_force,transform.position);
        if (turn) {
            body.AddTorque(torque);
            turn = false;
        }
    }
    public void thrustRotate(bool clockwise) {

        torque = Vector3.up * 400;
        if (!clockwise)
            torque = torque * -1;

        turn = true;
    }
    public void rotationForce(bool clockwise) {
        Vector3 side_force = Vector3.right * 50 * body.mass*Time.deltaTime;
        if (!clockwise)
            side_force = side_force * -1;
        body.AddForceAtPosition(side_force, transform.position + transform.forward * transform.localScale.z);
        body.AddForceAtPosition(side_force*-1, transform.position - transform.forward * transform.localScale.z);
    }
}

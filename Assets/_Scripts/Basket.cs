using UnityEngine;
using System.Collections;

public class Basket : MonoBehaviour {
    public float max_forward = 100;
    float forward;
    float rotate;
    Rigidbody body;
    Vector3 torque;
    bool turn = false;
    public GameObject my_balloon;
    // Use this for initialization
    void Start() {
        body = GetComponent<Rigidbody>();
        forward = 0;
        rotate = 0;
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector3 thrust = transform.forward * forward;
        body.AddForce(thrust);
        my_balloon.GetComponent<Balloon>().forward = forward * 1.6f;
        if (turn == true) {
            body.AddTorque(torque);
            turn = false;
        }

    }
    public void thrustUp() {
        if (forward < max_forward)
            forward += max_forward / 5;
        if (forward > max_forward)
            forward = max_forward;
    }
    public void thrustDown() {
        if (forward > -max_forward / 2)
            forward -= max_forward / 5;
        if (forward < -max_forward / 2)
            forward = -max_forward / 2;
    }
    public void thrustRotate(bool clockwise) {

        torque = Vector3.up * 400;
        if (!clockwise)
            torque = torque * -1;

        turn = true;
        my_balloon.GetComponent<Balloon>().thrustRotate(clockwise);
        /*
        clockwise = clo

        rotationForce(clockwise);
        my_balloon.GetComponent<Balloon>().rotationForce(clockwise);
        /*
        Vector3 side_force = transform.right*1500;
        if (!clockwise)
            side_force = side_force * -1;
        body.AddForce(side_force);
         * */
    }
    void rotationForce(bool clockwise) {
        Vector3 side_force = Vector3.right * 50 * body.mass * Time.deltaTime;
        if (!clockwise)
            side_force = side_force * -1;
        body.AddForceAtPosition(side_force, transform.position + transform.forward * transform.localScale.z);
        body.AddForceAtPosition(side_force * -1, transform.position - transform.forward * transform.localScale.z);
    }
}

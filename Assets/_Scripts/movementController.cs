using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class movementController : MonoBehaviour {
    public static movementController moveControl;
    KeyCode LEFT = KeyCode.LeftArrow;
    KeyCode RIGHT = KeyCode.RightArrow;
    KeyCode THRUST_UP = KeyCode.UpArrow;
    KeyCode THRUST_DOWN = KeyCode.DownArrow;
    KeyCode D_KEY = KeyCode.D;
    KeyCode R_KEY = KeyCode.R;
    KeyCode F_KEY = KeyCode.F;
    KeyCode S_KEY = KeyCode.S;
    KeyCode SPACE = KeyCode.Space;
    KeyCode ONE = KeyCode.Alpha1;
    KeyCode TWO = KeyCode.Alpha2;
    KeyCode THREE = KeyCode.Alpha3;
    KeyCode FOUR = KeyCode.Alpha4;
    float wait_left = 0;
    float thrust_delay = 0.2f; 
    public GameObject basket;
    // Use this for initialization
    void Start() {
        moveControl = this;
    }
    void Update() {
        if (wait_left > 0)
            wait_left -= Time.deltaTime;
        if (wait_left < 0)
            wait_left = 0;
        handleMoveInput();
    }
    void handleMoveInput() {
        if (wait_left == 0 && Input.GetKey(THRUST_UP)) {
            basket.GetComponent<Basket>().thrustUp();
            wait_left = thrust_delay;
        }
        if (wait_left == 0 && Input.GetKey(THRUST_DOWN)) {
            basket.GetComponent<Basket>().thrustDown();
            wait_left = thrust_delay;
        }
        if (Input.GetKeyDown(RIGHT))
            basket.GetComponent<Basket>().thrustRotate(true);
        if (Input.GetKeyDown(LEFT))
            basket.GetComponent<Basket>().thrustRotate(false);
    }
}


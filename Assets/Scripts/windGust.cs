﻿using UnityEngine;
using System.Collections;

public class windGust : MonoBehaviour {
    public float density = 1f;
    public float strength = 1;
    float height, width;
    LayerMask balloon_parts;
    Vector3 topleft;
	// Use this for initialization
	void Start () {
        balloon_parts = LayerMask.GetMask("Balloon", "Basket");
        height = transform.localScale.y;
        width = height = transform.localScale.x;
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float xdensity = width*density;
        float ydensity = height*density;
        for (int x = 0; x < xdensity; x++) {
            for (int y = 0; y < ydensity; y++) {
                return;
            }
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsMove : MonoBehaviour {
    public float moveForce;
    Rigidbody rig;
    float moveX;
    float moveY;
	// Use this for initialization
	void Start () {
        rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        transform.Translate( (Vector3.forward * moveY + Vector3.right * moveX)*moveForce*Time.fixedDeltaTime);
	}
}

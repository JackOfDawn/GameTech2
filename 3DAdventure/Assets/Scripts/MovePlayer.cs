﻿using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

    Rigidbody rb;
    float speed;
    float yRot;
    float xRot;
    float zRot;
    AudioSource audio;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        checkInput();	rotate();
	}

    void FixedUpdate()
    {
        move();
        
    }

    void rotate()
    {
        transform.Rotate(new Vector3(xRot, 0, zRot));
    }

    private void move()
    {
        rb.AddForce(transform.forward * speed);
        
    }
    private void checkInput()
    {
        speed = .5f * 100;
        if(Input.GetKey(KeyCode.Space))
        {
            speed = 100f;
            audio.pitch = 2f;
            xRot = Input.GetAxis("Vertical") * 100 * Time.deltaTime;
            zRot = -Input.GetAxis("Horizontal") * 500 * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.LeftShift))
        {

        }
        else
        {
            audio.pitch = 1f;
            xRot = Input.GetAxis("Vertical") * 100 * Time.deltaTime;
            zRot = -Input.GetAxis("Horizontal") * 100 * Time.deltaTime;
        }

        //xRot = Input.GetAxis("MouseY") * Time.deltaTime * 100;
        //zRot = -Input.GetAxis("MouseX") * Time.deltaTime * 100;
        
        
        Debug.Log(speed);

    }

    
}

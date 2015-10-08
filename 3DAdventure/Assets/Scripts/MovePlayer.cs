using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

    public bool inputEnabled = true;
    

    Rigidbody rb;
    float speed;
    float yRot;
    public float yaw;
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
        transform.Rotate(new Vector3(xRot, yRot, zRot));
    }

    private void move()
    {
        rb.AddForce(transform.forward * speed);
        
    }
    private void checkInput()
    {
        speed = .5f * 100;
        if (inputEnabled)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                speed *= 2;
                audio.pitch = 2f;
                xRot = Input.GetAxis("Vertical") * 200 * Time.deltaTime;
                zRot = -Input.GetAxis("Horizontal") * 200 * Time.deltaTime;
                //xRot = Input.GetAxis("MouseY") * Time.deltaTime * 200;
                //zRot = -Input.GetAxis("MouseX") * Time.deltaTime * 200;
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                speed *=.5f;
                audio.pitch = .5f;
                xRot = Input.GetAxis("Vertical") * 200 * Time.deltaTime;
                zRot = -Input.GetAxis("Horizontal") * 200 * Time.deltaTime;
            }
            else
            {
                audio.pitch = 1f;
                xRot = Input.GetAxis("Vertical") * 100 * Time.deltaTime;
                zRot = -Input.GetAxis("Horizontal") * 100 * Time.deltaTime;
                //xRot = Input.GetAxis("MouseY") * Time.deltaTime * 100;
                //zRot = -Input.GetAxis("MouseX") * Time.deltaTime * 100;
            }

            yRot = Input.GetAxis("Yaw") * yaw * Time.deltaTime;
        }


        
        

    }

    
}

using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

    Rigidbody rb;
    float speed;
    float yRot;
    float xRot;
    float zRot;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();	
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
        xRot = Input.GetAxis("Vertical") * 100 * Time.deltaTime;
        zRot = -Input.GetAxis("Horizontal") * 100 * Time.deltaTime;
        
        
        Debug.Log(speed);

    }
}

using UnityEngine;
using System.Collections;



public enum PlayerID
{
    P1,
    P2
}
public class MovePlayer : MonoBehaviour {

    public bool inputEnabled = true;

    public PlayerID pID;
    Rigidbody rb;
    float speed;
    float yRot;
    public float yaw;
    float xRot;
    float zRot;
    //AudioSource audio;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        //audio = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
            if (Input.GetAxis(pID + "_Boost") > 0)
            {
                speed *= 2;
                //audio.pitch = 2f;
                xRot = Input.GetAxis(pID + "_Vertical") * 100 * Time.deltaTime;
                zRot = -Input.GetAxis(pID + "_Yaw") * 100 * Time.deltaTime;
                //xRot = Input.GetAxis("MouseY") * Time.deltaTime * 200;
                //zRot = -Input.GetAxis("MouseX") * Time.deltaTime * 200;
            }
            else if ((Input.GetAxis(pID + "_Boost") < 0))
            {
                speed *=.5f;
                //audio.pitch = .5f;
                xRot = Input.GetAxis(pID + "_Vertical") * 100 * Time.deltaTime;
                zRot = -Input.GetAxis(pID + "_Yaw") * 100 * Time.deltaTime;
            }
            else
            {
                //audio.pitch = 1f;
                xRot = Input.GetAxis(pID + "_Vertical") * 100 * Time.deltaTime;
                zRot = -Input.GetAxis(pID + "_Yaw") * 100 * Time.deltaTime;

                //xRot = Input.GetAxis("MouseY") * Time.deltaTime * 100;
                //zRot = -Input.GetAxis("MouseX") * Time.deltaTime * 100;
            }
            
            yRot = Input.GetAxis(pID + "_Horizontal") * yaw * Time.deltaTime;
        }


        
        

    }

    
}

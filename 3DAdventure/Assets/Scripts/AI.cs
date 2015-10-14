using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

	// Use this for initialization
    Rigidbody rb;
    public float speed = 50;
    public float maxRotDegrees = 60;
    float yRot;
    public float yaw;
    float xRot;
    float zRot;

    public Transform target;
	void Start () {
        if (!target)
            target = GameObject.Find("PlayerOne").transform;
        if (!rb)
            rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 direction = target.position - transform.position;
        //Vector3.Lerp(transform.forward, direction, Time.deltaTime);
        
        transform.rotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.LookRotation(direction), maxRotDegrees * Time.deltaTime);
       
	}

    void FixedUpdate()
    {
        rb.AddForce(transform.forward * speed);

    }
}

using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {

    Rigidbody rb;
    bool collided = false;
    Animator explosionAnim;
	// Use this for initialization
	void Start () 
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;

        explosionAnim = GetComponentInChildren<Animator>();
	}

    void Update()
    {
        if(!rb.isKinematic)
            transform.LookAt( transform.position + rb.velocity);
    }

    public void Release(Vector3 velocity)
    {
        rb.isKinematic = false;
        rb.velocity = velocity;
        transform.parent = null;
    }

    void OnTriggerEnter(Collider collider)
    {
        if(!collided)
        {
            explosionAnim.SetTrigger("EXPLODE");
            rb.isKinematic = true;
        }
        collided = true;


    }
	
}

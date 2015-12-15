using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {

    Rigidbody rb;
    bool collided = false;
    Animator explosionAnim;
    Explosion explosionCheck;
    Transform shell;
	// Use this for initialization
	void Start () 
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;

        explosionAnim = GetComponentInChildren<Animator>();
        explosionCheck = GetComponentInChildren<Explosion>();
        shell = transform.GetChild(0);

	}

    void Update()
    {
        if(!rb.isKinematic)
            transform.LookAt( transform.position + rb.velocity);
        if (explosionCheck.explosionOver)
            Destroy(this.gameObject);

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
            shell.gameObject.SetActive(false);
            explosionAnim.SetTrigger("EXPLODE");
            rb.isKinematic = true;
        }
        collided = true;


    }
	
}

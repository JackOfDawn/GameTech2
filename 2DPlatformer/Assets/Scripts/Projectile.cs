using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	// Use this for initialization
    public float power = 100;
    Rigidbody2D rd;

    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    public void fire(Vector2 position, Vector2 direction)
    {
        rd.velocity = Vector2.zero;
        transform.position = position;
        rd.AddForce(direction.normalized * power);
    }
}

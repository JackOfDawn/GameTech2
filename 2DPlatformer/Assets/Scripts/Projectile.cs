using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    public enum TYPE
    {
        Zero = 0,
        Anti,
        Normal
    };
	// Use this for initialization
    public float power = 100;
    Rigidbody2D rd;

    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    public void fire(Vector2 position, Vector2 direction)
    {
        rd = GetComponent<Rigidbody2D>();
        rd.velocity = Vector2.zero;
        transform.position = position;
        rd.AddForce(direction.normalized * power);
    }

    float timeRemaining = 5;
     void Update()
    {
        timeRemaining -= Time.deltaTime;
         if(timeRemaining < 0)
         {
             Destroy(this.gameObject);
         }
    }
}

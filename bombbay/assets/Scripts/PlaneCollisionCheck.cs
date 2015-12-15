using UnityEngine;
using System.Collections;

public class PlaneCollisionCheck : MonoBehaviour {

    Health health;
	void Start ()
    {
        health = GetComponent<Health>();	
	}

    void Update()
    {
        if (!health.IsAlive())
            transform.parent.gameObject.SetActive(false);
    }

	void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("ShellExplosion"))
        {
            Debug.Log("Shell hit");
            health.TakeDamage(1);
        }
        if (collider.CompareTag("BombExplosion"))
        {
            health.TakeDamage(1);
            Debug.Log("BOMBHIT");
        }
    }
}

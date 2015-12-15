using UnityEngine;
using System.Collections;

public class TowerCollisionCheck : MonoBehaviour {

	// Use this for initialization
    Health health;
	void Start ()
    {
        health = GetComponentInChildren<Health>();	
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

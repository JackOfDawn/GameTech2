using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {


    public float MAX_HEALTH;
    public float currentHealth { get; private set; }
    bool dead = false;
	// Use this for initialization
	void Start () 
    {
        currentHealth = MAX_HEALTH;
	
	}

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            dead = true;
        }
    }

    public bool IsAlive() { return !dead; }
}

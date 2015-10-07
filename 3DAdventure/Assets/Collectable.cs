using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Collectable : MonoBehaviour {

    public bool collected = false; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}


    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}

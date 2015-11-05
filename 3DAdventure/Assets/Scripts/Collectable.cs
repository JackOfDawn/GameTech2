using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Collectable : MonoBehaviour {

    public bool collected = false;
    AudioSource src;
	// Use this for initialization
	void Start () {
        src = GetComponent<AudioSource>();	
	}
	
	// Update is called once per frame
	void Update () {
        
	}


    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            StartCoroutine(waitForDestruction());
        }
    }

    IEnumerator waitForDestruction()
    {
        src.Play();
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}

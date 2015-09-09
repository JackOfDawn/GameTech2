using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour {

	// Use this for initialization

    public Transform spawnPoint;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        collider.transform.position = spawnPoint.position;
    }
}

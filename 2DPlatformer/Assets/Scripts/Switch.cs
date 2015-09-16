using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Switch : MonoBehaviour {

    public UnityEvent switches;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        switches.Invoke();     
    }
}

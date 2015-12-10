using UnityEngine;
using System.Collections;

public class Propeller : MonoBehaviour {

	// Use this for initialization

    public float scale = 10000;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    transform.Rotate(new Vector3( scale * Time.deltaTime, 0,0));
	}
}

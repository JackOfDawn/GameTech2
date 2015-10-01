using UnityEngine;
using System.Collections;

public class Propeller : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    transform.Rotate(new Vector3( 10000 * Time.deltaTime, 0,0));
	}
}

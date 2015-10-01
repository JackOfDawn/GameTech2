using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	// Use this for initialization
    public Transform target;
    public Vector3 distance;

	void Start () {
        distance = this.transform.position - target.position;	
	}
	
	// Update is called once per frame
	void Update () {
        //this.transform.position = -target.forward * distance.magnitude;

        this.transform.position = Vector3.Lerp(transform.position, target.position + distance, Time.deltaTime);
        //this.transform.LookAt(target);
	}
}

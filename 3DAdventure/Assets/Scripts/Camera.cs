using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	// Use this for initialization
    public Transform target;
    public Vector3 distance;
    public float rotTime = 50;
    public float followTime = 10;

	void Start () {
        distance = this.transform.position - target.position;	
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.DrawRay( target.position, target.forward * distance.z + (target.up * distance.y) );
        Vector3 newPos = target.position + (target.forward * distance.z + target.up * distance.y);
        this.transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * followTime);

        Vector3 targetRotation = target.rotation.eulerAngles;
        //transform.LookAt(target);
        //targetRotation.z = 0;
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, target.rotation, Time.deltaTime * rotTime);//(Vector3.Lerp(this.transform.rotation.eulerAngles, targetRotation, Time.deltaTime));
        
        //this.transform.LookAt(target);
	}
}

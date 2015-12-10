using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour {

	public enum RotateAxes{MouseXandY = 0, MouseX = 1, MouseY = 2}
	public RotateAxes axis = RotateAxes.MouseXandY;
	public float sensitivityX = 15f;
	public float sensitivityY = 15f;
	public float minY = -60f;
	public float maxY = 60f;

	float rotationY = 0f;

	// Use this for initialization
	void Start () {
		if(GetComponent<Rigidbody>()) GetComponent<Rigidbody>().freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
		lookAtMouse();
	}

	void lookAtMouse(){
		if(axis == RotateAxes.MouseX){
			transform.Rotate(0,Input.GetAxis("MouseX")*sensitivityX, 0);

		}else if(axis==RotateAxes.MouseY){
			rotationY+= Input.GetAxis("MouseY") *sensitivityY;
			rotationY = Mathf.Clamp(rotationY, minY, maxY);
			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}else{

			float rotationX = transform.localEulerAngles.y + Input.GetAxis("MouseX")* sensitivityX;
			rotationY+= Input.GetAxis("MouseY") *sensitivityY;
			rotationY = Mathf.Clamp(rotationY, minY, maxY);
			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);

		}

	}
}

using UnityEngine;
using System.Collections;

public class CameraTopDown : MonoBehaviour {

    public Transform target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 newPos = target.position;
        newPos.y += 300;
        this.transform.position = newPos;
        
	    
	}
}

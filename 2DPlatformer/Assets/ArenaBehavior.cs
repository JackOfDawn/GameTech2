using UnityEngine;
using System.Collections;

public class ArenaBehavior : MonoBehaviour {

	// Use this for initialization
    public Transform spawn;
	void Start () {

        GameObject.Find("Player").transform.position = spawn.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour {

    public Camera cam;
    public Transform target;
    public GameObject camTexture;

	// Use this for initialization
	void Start () {
        if(!cam)
            cam = GetComponent<Camera>();
        //cam.enabled = false;
        //border.enabled = false;

        DontDestroyOnLoad(this.transform.parent.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = target.position + new Vector3(0, 0, -34);


        if(Input.GetKeyDown(KeyCode.M))
        {
            cam.enabled = !cam.enabled;
            camTexture.SetActive(cam.enabled);
        }
	}
}

using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	// Use this for initialization
    LookAtMouse lookAtMouse;
    Camera cam;

    Animator LeftGun;
    Animator RightGun;

    Transform plane;

    ShellScript leftShell;
    ShellScript rightShell;

    bool firedLeft = false;

    public bool activeTurret { get; private set; }
	void Start () 
    {
        if (!plane)
            plane = GameObject.Find("PlayerOne").transform;
        activeTurret = false;
        if (!cam)
            cam = transform.GetComponentInChildren<Camera>();
        if (!lookAtMouse)
            lookAtMouse = GetComponent<LookAtMouse>();

        if (!LeftGun)
            LeftGun = transform.FindChild("Left Gun").GetComponent<Animator>();
        if (!RightGun)
            RightGun = transform.FindChild("Right Gun").GetComponent<Animator>();

        DeactivateTurret();
	
	}
    void Update()
    {
        if(!activeTurret)
            transform.LookAt(plane);
    }
    public void Fire(bool left)
    {
        if (firedLeft)
        {
            LeftGun.SetTrigger("FIRE");
            leftShell = ((GameObject)Instantiate(GameManager.Instance.GetShellPrefab(), LeftGun.transform.position, Quaternion.identity)).GetComponent<ShellScript>();
            leftShell.setForward(transform.forward);
        }
        else
        {
            RightGun.SetTrigger("FIRE");
            rightShell = ((GameObject)Instantiate(GameManager.Instance.GetShellPrefab(), RightGun.transform.position, Quaternion.identity)).GetComponent<ShellScript>();
            rightShell.setForward(transform.forward);
        }
        firedLeft = !firedLeft;
    }

    public void Release(bool left)
    {
        if(left)
        {
            //Destroy(leftShell.gameObject);
        }
        else
        {
            //Destroy(rightShell.gameObject);
        }
    }



    public void ActivateTurret()
    {
        cam.enabled = true;
        lookAtMouse.enabled = true;
        activeTurret = true;
    }

    public void DeactivateTurret()
    {
        cam.enabled = false;
        lookAtMouse.enabled = false;
        activeTurret = false;
    }

}

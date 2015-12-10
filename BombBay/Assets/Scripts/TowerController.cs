using UnityEngine;
using System.Collections;

public class TowerController : MonoBehaviour {

    Tower[] turrets;
    Tower activeTurret;

	// Use this for initialization
	void Start () {
        turrets = GameObject.FindObjectsOfType<Tower>();
        activeTurret = turrets[1];
        activeTurret.ActivateTurret();

	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonDown(1))
            activeTurret.Fire(false);
        if (Input.GetMouseButtonDown(0))
            activeTurret.Fire(true);
        if (Input.GetMouseButtonUp(1))
            activeTurret.Release(false);
        if (Input.GetMouseButtonUp(0))
            activeTurret.Release(true);

        if(Input.GetKeyDown(KeyCode.Alpha1))
            SetActiveTurret(1);
        if(Input.GetKeyDown(KeyCode.Alpha2))
            SetActiveTurret(2);
        if(Input.GetKeyDown(KeyCode.Alpha3))
            SetActiveTurret(3);
        if(Input.GetKeyDown(KeyCode.Alpha4))
            SetActiveTurret(4);
        if(Input.GetKeyDown(KeyCode.Alpha5))
            SetActiveTurret(5);
	
	}

    void SetActiveTurret(int num)
    {
        activeTurret.DeactivateTurret();
        activeTurret = turrets[num - 1];
        activeTurret.ActivateTurret();

    }
}

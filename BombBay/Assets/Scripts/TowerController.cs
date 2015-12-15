using UnityEngine;
using System.Collections;

public class TowerController : MonoBehaviour {

    Tower[] turrets;
    public Health[] turretHealth;
    Tower activeTurret;

	// Use this for initialization
	void Start () {
        turrets = GameObject.FindObjectsOfType<Tower>();
        turretHealth = GetComponentsInChildren<Health>();
        activeTurret = turrets[1];
        activeTurret.ActivateTurret();

	}
	
	// Update is called once per frame
	void Update () 
    {

        //if (Input.GetMouseButtonDown(1))
            //activeTurret.Fire();
        if (Input.GetMouseButtonDown(0))
            activeTurret.Fire();
        //if (Input.GetMouseButtonUp(1))
            //activeTurret.Release(false);
        if (Input.GetMouseButtonUp(0))
            activeTurret.Release(true);

        if(Input.GetKeyDown(KeyCode.Alpha1) && turretHealth[0].IsAlive() )
            SetActiveTurret(1);
        if(Input.GetKeyDown(KeyCode.Alpha2) && turretHealth[1].IsAlive() )
            SetActiveTurret(2);
        if(Input.GetKeyDown(KeyCode.Alpha3) && turretHealth[2].IsAlive() )
            SetActiveTurret(3);
        if(Input.GetKeyDown(KeyCode.Alpha4) && turretHealth[3].IsAlive() )
            SetActiveTurret(4);
        if(Input.GetKeyDown(KeyCode.Alpha5) && turretHealth[4].IsAlive() )
            SetActiveTurret(5);
	
	}

    void SetActiveTurret(int num)
    {
        activeTurret.DeactivateTurret();
        activeTurret = turrets[num - 1];
        activeTurret.ActivateTurret();

    }
}

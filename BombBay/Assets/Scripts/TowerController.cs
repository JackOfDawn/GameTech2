using UnityEngine;
using System.Collections;

public class TowerController : MonoBehaviour {

    Tower[] turrets;
    public Health[] turretHealth;
    Tower activeTurret;
    public int activeNum;
    bool lockTurret = false;
	// Use this for initialization
	void Start () {
        turrets = GameObject.FindObjectsOfType<Tower>();
        turretHealth = new Health[5];
        for (int i = 0; i < turrets.Length; i++)
        {
            turretHealth[i] = turrets[i].health;
        }
        SetActiveTurret(1);

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

        if (!lockTurret)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && turretHealth[0].IsAlive())
                SetActiveTurret(1);
            if (Input.GetKeyDown(KeyCode.Alpha2) && turretHealth[1].IsAlive())
                SetActiveTurret(2);
            if (Input.GetKeyDown(KeyCode.Alpha3) && turretHealth[2].IsAlive())
                SetActiveTurret(3);
            if (Input.GetKeyDown(KeyCode.Alpha4) && turretHealth[3].IsAlive())
                SetActiveTurret(4);
            if (Input.GetKeyDown(KeyCode.Alpha5) && turretHealth[4].IsAlive())
                SetActiveTurret(5);
        }
	}

    void SetActiveTurret(int num)
    {
        if(activeTurret)
            activeTurret.DeactivateTurret();
        if (num != -1)
        {
            activeTurret = turrets[num - 1];
            activeTurret.ActivateTurret();
            activeNum = num - 1;
        }
        else
        {
            activeNum = -1;
        }

    }
    public void DisableCamera()
    {
        lockTurret = true;
        SetActiveTurret(-1);
    }
    public bool AllTowersDisabled()
    {
        foreach (Health health in turretHealth)
        {
            if (health.IsAlive())
                return false;
        }
        return true;
    }

    public Camera GetActiveCam()
    {
        lockTurret = true;
        return activeTurret.gameObject.GetComponentInChildren<Camera>();
    }
}

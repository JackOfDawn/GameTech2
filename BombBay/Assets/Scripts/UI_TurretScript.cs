using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_TurretScript : MonoBehaviour {

    Image[] turretHealthUI;
    public TowerController towerController; 
	// Use this for initialization
	void Start () 
    {
        turretHealthUI = GetComponentsInChildren<Image>();
        
        if(!towerController)
            towerController = transform.Find("Towers").GetComponent<TowerController>();

	}
	
	// Update is called once per frame
	void Update () 
    {
        for (int i = 0; i < turretHealthUI.Length; i++)
        {
            if(towerController)
                turretHealthUI[i].fillAmount = towerController.turretHealth[i].currentHealth / towerController.turretHealth[i].MAX_HEALTH;
        }	
	}
}

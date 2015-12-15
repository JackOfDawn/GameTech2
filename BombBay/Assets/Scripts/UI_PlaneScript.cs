using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_PlaneScript : MonoBehaviour {

	// Use this for initialization
    Image planeHealthUI;
    public Health planeHealth;
	void Start () 
    {
        planeHealthUI = GetComponentInChildren<Image>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(planeHealthUI && planeHealth)
        {
            planeHealthUI.fillAmount = planeHealth.currentHealth / planeHealth.MAX_HEALTH;
        }
	}
}

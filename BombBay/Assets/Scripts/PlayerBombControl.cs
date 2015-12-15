using UnityEngine;
using System.Collections;

public class PlayerBombControl : MonoBehaviour {

	// Use this for initialization
    public GameObject bombPrefab;
    Transform bombDropLocation;
    bool prevButtonPressed = false;
    BombScript bomb;
    Rigidbody rb;

	void Start () 
    {
        bombDropLocation = GameObject.Find("BOMB_SPAWN").transform;
        rb = GetComponent<Rigidbody>();


	}
	
	// Update is called once per frame
	void Update () 
    {
        bool buttonPressed = Input.GetAxis("P1_Bomb") > 0;

        if(buttonPressed == true && prevButtonPressed == false) //pressed
        {
            bomb = ((GameObject)Instantiate(bombPrefab, bombDropLocation.position, Quaternion.identity)).GetComponent<BombScript>();
            bomb.transform.parent = this.transform;
            bomb.transform.LookAt(bombDropLocation.position + bombDropLocation.transform.forward);
        }

        if(buttonPressed == false && prevButtonPressed == true) //released
        {
            if(bomb != null)
                bomb.Release(rb.velocity);
            
        }

        prevButtonPressed = buttonPressed;
	}
} 
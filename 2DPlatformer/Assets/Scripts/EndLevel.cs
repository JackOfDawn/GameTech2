using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour {

	// Use this for initialization
    public string NextLevel;

    //public Transform spawnPoint;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
            Application.LoadLevel(NextLevel);
    }

    public void End()
    {
        Application.LoadLevel(NextLevel);
    }

    public void ResetGame()
    {
        Destroy(GameObject.Find("Player"));
        Destroy(GameObject.Find("HUD"));
        End();
    }
}

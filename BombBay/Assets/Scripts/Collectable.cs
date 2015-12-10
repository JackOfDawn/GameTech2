using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Collectable : MonoBehaviour 
{

    public bool collected;
    AudioSource src;
	// Use this for initialization
	void Start () 
    {
        src = GetComponent<AudioSource>();
        collected = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}


    void OnTriggerEnter(Collider collider)
    {
        if (!collected)
        {
            if (collider.CompareTag("Player1"))
            {
                StartCoroutine(waitForDestruction());
                collected = true;
                GameManager.Instance.collected(0);
            }
            else if (collider.CompareTag("Player2"))
            {
                StartCoroutine(waitForDestruction());
                collected = true;
                GameManager.Instance.collected(1);
            }
        }
    }

    IEnumerator waitForDestruction()
    {
        src.Play();
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}

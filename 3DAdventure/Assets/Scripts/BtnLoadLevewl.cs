using UnityEngine;
using System.Collections;

public class BtnLoadLevewl : MonoBehaviour {

    public string Level;
	// Use this for initialization
    public void Pressed()
    {
        Application.LoadLevel(Level);
    }
}

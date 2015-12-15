using UnityEngine;
using System.Collections;

public class DRAW_GIZMO : MonoBehaviour {

	// Use this for initialization
    public float size = 10;

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, size);
        //Gizmos.DrawSphere(transform.position, 5);
    }
}

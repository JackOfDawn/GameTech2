using UnityEngine;
using System.Collections;

public class ShellScript : MonoBehaviour {

    public float speed;
    public Vector3 direction = Vector3.forward;
    bool explode = false;
    bool exploding = false;
    float explosionScale;
    float time = 2;
    public bool remove = false;
	// Use this for initialization
    Animator anim;

    float distance = 10;
    Transform target;

	void Start () 
    {
        anim = GetComponent<Animator>();
        target = GameManager.Instance.PlaneTargetTransform;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(!explode)
        {
            transform.Translate(direction * speed * Time.deltaTime);
            if(Vector3.SqrMagnitude(target.position - transform.position) <= distance * distance)
                explode = true;
                
            time -= Time.deltaTime;
            if (time <= 0)
            {
                explode = true;
            }

        }
        else if(!exploding)
        {
            exploding = true;
            //StartCoroutine("EXPLODE");
            anim.SetTrigger("EXPLODE");

        }
        if(remove)
        {
            Remove();
        }
	
	}

    IEnumerator EXPLODE()
    {
        yield return null;
        Destroy(this.gameObject);
    }

    public void setForward(Vector3 forward)
    {
        direction = forward;
    }

    public void Remove()
    {
        Destroy(this.gameObject);
    }
}

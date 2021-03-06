﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    const float ANGLE = .52f;
    public float dist =.2f;

    public GameObject antiGrav;
    public GameObject zeroGrav;
    public GameObject normalGrav;
    public Projectile.TYPE projType;


    public enum DashType
    {
        StandardDash = 1,
        QJetDash = 2,
        MixDash
    }

    public void setType(int type)
    {
        projType = (Projectile.TYPE)type;
    }

    public float maxSpeed = 10f;
    public float jumpForce = 700;


    
    bool facingRight = true;
    public Rigidbody2D rigid2D;

    //animation
    public Animator legAnim;
    public Animator bodyAnim;
    public Animator headAnim;

    //controls
    float move = 0;
    float aim = 0;

    //Dashing
    public DashType dashType = DashType.StandardDash;
    bool _oldDashPressed = false;
    bool _isDashing = false;
    Vector2 _dashDirection;

    //Cannon
    Vector3 CannonAngle;

    //Falling shizzles
    bool grounded = false;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    float gravityScale;

	// Use this for initialization
	void Start () {
        if (legAnim == null)
            legAnim = transform.FindChild("Legs").GetComponent<Animator>();
        if (bodyAnim == null)
            bodyAnim = transform.FindChild("Body").GetComponent<Animator>();
        if (rigid2D == null)
            rigid2D = gameObject.GetComponent<Rigidbody2D>();
        gravityScale = rigid2D.gravityScale;

        DontDestroyOnLoad(this.gameObject);
        

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        legAnim.SetBool("Grounded", grounded);
        legAnim.SetFloat("vSpeed", rigid2D.velocity.y);

        //controlMovement
        if (!_isDashing)
        {
            legAnim.SetFloat("Speed", Mathf.Abs(move));

            rigid2D.velocity = new Vector2(move * maxSpeed, rigid2D.velocity.y);
        }

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

        //control aiming;
        //bodyAnim.SetFloat("vAim", aim);

	}

    void Update()
    {
        //get key presses
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            legAnim.SetBool("Grounded", false);
            rigid2D.AddForce(new Vector2(0, jumpForce));
        }
        move = Input.GetAxis("Horizontal");
        aim = Input.GetAxis("Vertical");
        bodyAnim.SetFloat("vAim", aim);


        //Get aiming Vector
        CannonAngle = new Vector3(1,0,0);
        if(aim > .1f)
        {
            CannonAngle.x = Mathf.Cos(ANGLE);
            CannonAngle.y = Mathf.Sin(ANGLE); 
        }
        else if(aim < -.1f)
        {
            CannonAngle.x = Mathf.Cos(ANGLE);
            CannonAngle.y = -Mathf.Sin(ANGLE);
        }

        if (!facingRight) CannonAngle.x *= -1;
        bodyAnim.transform.localEulerAngles = CannonAngle;
        //Debug.Log(bodyAnim.transform.localEulerAngles);
        //Debug.DrawRay(bodyAnim.transform.position, angle);


        _dashDirection.x = move;
        _dashDirection.y = aim;

        

        //Firing
        if (Input.GetKeyDown(KeyCode.X))
        {
            bodyAnim.SetTrigger("Fire");

           

            Vector2 pos = bodyAnim.transform.position + (CannonAngle.normalized * dist);
            Debug.DrawLine(bodyAnim.transform.position, pos, Color.green, 2f);

            // Change depending on what is pressed;
            Projectile projectile;
            switch(projType)
            {
                case Projectile.TYPE.Anti:
                    projectile = (Instantiate(antiGrav, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<Projectile>();
                    break;
                case Projectile.TYPE.Normal:
                    projectile = (Instantiate(normalGrav, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<Projectile>();
                    break;
                default: 
                    projectile = (Instantiate(zeroGrav, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<Projectile>();
                    break;
            }
            projectile.fire(pos, CannonAngle);

        }

        //Dashing
        bool newDashPressed = false;// Input.GetKeyDown(KeyCode.Z);
        if(newDashPressed && !_oldDashPressed)
        {
            _isDashing = true;
            switch(dashType)
            {
                case DashType.StandardDash:
                    HandleStandardDash();
                    break;
            }
        }
            _oldDashPressed = newDashPressed;
        
    }

    #region StandardDash
    public float dashSeconds;
    public float dashPower;
    public float dashDistance;
    void HandleStandardDash()
    {
        StopCoroutine("StandardDashing");
        StartCoroutine("StandardDashing");
    }
     
    IEnumerator StandardDashing()
    {
        _isDashing = true;
        _dashDirection.Normalize();
        Vector3 destination = transform.position + (Vector3)(_dashDirection * dashDistance);
        Vector3 startPos = transform.position;
        startPos.z = 0;

        float t = dashSeconds;
        rigid2D.gravityScale = 0;
        while(t > 0)
        {
            //startPos = transform.position;
            t -= Time.deltaTime;
            Vector3 newPos = Vector3.Lerp(transform.position, destination, Time.deltaTime * dashPower);
            //Debug.Log("newPos:" + newPos + ", StartPos: " + startPos);
            //rigid2D.velocity = (Vector2)(newPos - transform.position) * dashPower;
            rigid2D.MovePosition(newPos);

            //Debug.Log(rigid2D.velocity);
            
            rigid2D.velocity = Vector3.zero;
            yield return null;
        }
        rigid2D.gravityScale = gravityScale; 
        _isDashing = false;
    }
    #endregion


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void Test()
    {
        rigid2D.velocity *= 10;
    }
}

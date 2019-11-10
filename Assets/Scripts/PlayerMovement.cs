using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float moveSpeed = 5f;
    public float accSpeed = 5f;
    public float accSpeedFall = 5f;
    public GameObject rope;
    public GameObject[] sideWall;
    public GameObject[] topdownWall;
    // public Animator animator;
    //
    // float hMove = 0f;
    // bool isJump = false;
    // bool isCrouch = false;
    // bool isGrounded = false;
    private float width;
    private float height;
    Rigidbody2D r;
    Vector3 point = Vector3.zero;
    //
    private float m_MoveStep = 0f;
    private bool m_IsArrived = true;
    private Vector3 m_Destination = new Vector3(0, 0, 0);
    private Vector3 tempPos = Vector3.zero;
    private double m_MoveAngle = 0;
    private float tempSpeedX = 0;
    private float tempSpeedY = 0;
    private bool isTouchSide = false;
    private bool isTouchTop = false;
    private double mDistance = 0;
    private float mBackRopeSpeed = 1.5f;
    private STATE mState;
    enum STATE {
        ROPE,
        NORMAL,
        NONE,
    };
    Vector2[] points = new Vector2[4] {
        new Vector2((float)(-7.61), (float)5.75), 
        new Vector2((float)(-7.61), (float)(-7.63)), 
        new Vector2((float)7.65, (float)(-7.63)), 
        new Vector2((float)7.65, (float)5.75)
        };
    // Start is called before the first frame update
    void Start()
    {
        r = controller.GetRigid();
        // isGrounded = controller.IsLanded();
        //
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;
        mState = STATE.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        // hMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        // r = controller.GetRigid();
        // isGrounded = controller.IsLanded();
        // //
        // // animator.SetBool("Grounded", isGrounded);
        // // animator.SetFloat("Move Speed", Math.Abs(hMove));
        // // animator.SetFloat("Verticle Velocity", r.velocity.y);
        // //
        // if (Input.GetButtonDown("Jump"))
        //     isJump = true;
        // //
        // if (Input.GetButtonDown("Crouch"))
        //     isCrouch = true;
        // else if (Input.GetButtonUp("Crouch"))
        //     isCrouch = false;
        //
        // if (!m_IsArrived)
            // transform.position = tempPos;

        if (Input.GetButtonDown("Fire1"))
        {
            StopHandler(false, false);
            MoveHandler();
            // point.z = 0;
            // Debug.Log(point);
            // transform.position = point;
        }

        switch (mState)
        {
            case STATE.NORMAL:
                if (!m_IsArrived)
                {
                    tempPos.x = transform.position.x + tempSpeedX*(float)Math.Cos(m_MoveAngle*Math.PI/180)*Time.deltaTime;
                    if (r.gravityScale == 0f)
                    {
                        // Debug.Log("here");
                        tempPos.y +=  tempSpeedY*(float)Math.Sin(m_MoveAngle*Math.PI/180)*Time.deltaTime;
                    }
                    else 
                    {
                        tempPos.y = transform.position.y;
                        tempPos.y += tempSpeedY*(float)Math.Sin(m_MoveAngle*Math.PI/180)*Time.deltaTime;
                    }
                    transform.position = tempPos;
                    if (rope.transform.localScale.x > 0)
                    {
                        // Debug.Log("doing");
                        mBackRopeSpeed += 0.02f*Time.deltaTime;
                        rope.transform.localScale -= new Vector3(mBackRopeSpeed*Time.deltaTime, 0, 0);
                    }
                    // else rope.transform.localScale = new Vector3((float)0.01, 0, 0);
                }
            break;

            case STATE.ROPE:
                rope.transform.localScale += new Vector3((float)4*Time.deltaTime, 0, 0);
                float length = 0.5f*rope.GetComponent<CircleCollider2D>().radius*rope.transform.localScale.x;
                // Debug.Log(length);
                if (length >= mDistance)
                    mState = STATE.NORMAL;
            break;
        }
    }

    void MoveHandler()
    {
        // StopHandler(false, false);
        tempPos = transform.position;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
        point = ray.origin + (ray.direction * Camera.main.transform.position.z * -1);
        m_Destination = point;
        m_IsArrived = false;
        // m_MoveAngle = 90 - (double)Vector2.Angle(transform.position, (Vector2)point);
        // if (point.x < transform.position.x)
        //     m_MoveAngle = 180 - m_MoveAngle;
        tempSpeedX = moveSpeed;
        tempSpeedY = moveSpeed;
        isTouchSide = false;
        r.velocity = Vector2.zero;
        mBackRopeSpeed = 1.5f;
        //
        m_MoveAngle = GetAngle(transform.position, (Vector2)point);
        rope.transform.eulerAngles = new Vector3(
            rope.transform.eulerAngles.x,
            rope.transform.eulerAngles.y,
            (float)(m_MoveAngle)
        );
        controller.SetGravity(0f);
        if (PointInTriangle((Vector2)point, transform.position, points[0], points[1]))
        {
                Debug.Log("left");
                mDistance = Math.Abs(sideWall[0].transform.position.x - transform.position.x)/Math.Cos(Math.PI - (m_MoveAngle*Math.PI/180));
        }
        else 
        {
            if (PointInTriangle((Vector2)point, transform.position, points[1], points[2]))
            {
                Debug.Log("down");
                mDistance = Math.Abs(topdownWall[0].transform.position.y - transform.position.y)/Math.Cos(m_MoveAngle*Math.PI/180 + 0.5*Math.PI);
            }
            else 
            {
                if (PointInTriangle((Vector2)point, transform.position, points[2], points[3]))
                {
                    Debug.Log("right");
                    mDistance = Math.Abs(sideWall[1].transform.position.x - transform.position.x)/Math.Cos(m_MoveAngle*Math.PI/180);
                }
                else 
                {
                    if (PointInTriangle((Vector2)point, transform.position, points[3], points[0]))
                    {
                        Debug.Log("top");
                        mDistance = Math.Abs(topdownWall[1].transform.position.y - transform.position.y)/Math.Cos(m_MoveAngle*Math.PI/180 - 0.5*Math.PI);
                    }
                }
            }
        }
        // Debug.Log(mDistance);
        // mDistance = (transform.position.x > point.x) 
        //         ? Math.Abs(sideWall[0].transform.position.x - transform.position.x)/Math.Cos(Math.PI - (m_MoveAngle*Math.PI/180))
        //         : Math.Abs(sideWall[1].transform.position.x - transform.position.x)/Math.Cos(m_MoveAngle*Math.PI/180);
        // Debug.Log(Math.Abs(sideWall[1].transform.position.x - transform.position.x)/Math.Cos(m_MoveAngle*Math.PI/180));
        // Vector3 scale = rope.transform.localScale;
        // scale.Set((float)0.1,0,0);
        rope.transform.localScale = new Vector3(0, 1, 1);
        mState = STATE.ROPE;
    }

    void StopHandler(bool side, bool top)
    {
        // m_MoveStep = 0f;
        // tempSpeed = moveSpeed;
        rope.transform.localScale = new Vector3(0, 1, 1);
        switch (side)
        {
            case false:
                m_Destination = Vector3.zero;
                m_IsArrived = true;
                controller.SetGravity(1f);
                isTouchSide = false;
            break;

            case true:
                tempSpeedX = moveSpeed;
                // tempSpeedY = moveSpeed;
                tempSpeedX *= -1;
                controller.SetGravity(1f);
                isTouchSide = true;
                Debug.Log("touch");
            break;
        }
    }

    float sign (Vector2 p1, Vector2 p2, Vector2 p3)
    {
        return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
    }

    bool PointInTriangle (Vector2 pt, Vector2 v1, Vector2 v2, Vector2 v3)
    {
        float d1, d2, d3;
        bool has_neg, has_pos;

        d1 = sign(pt, v1, v2);
        d2 = sign(pt, v2, v3);
        d3 = sign(pt, v3, v1);

        has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
        has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);
        // Debug.Log("here");

        return !(has_neg && has_pos);
    }

    double GetAngle(Vector2 p1, Vector2 p2)
    {
        double dy = p2.y - p1.y;
        double dx = p2.x - p1.x;
        double theta = Math.Atan2(dy, dx); // range (-PI, PI]
        theta *= 180 / Math.PI; // rads to degs, range (-180, 180]
        //if (theta < 0) theta = 360 + theta; // range [0, 360)
        return theta;
    }

    void FixedUpdate()
    {
        switch (mState)
        {
            case STATE.ROPE:
            break;

            case STATE.NORMAL:
                if (!m_IsArrived)
                {
                    // m_MoveStep += 1*Time.fixedDeltaTime;
                    // transform.position = Vector2.MoveTowards(transform.position, (Vector2)m_Destination, m_MoveStep);
                    // Vector2 p;
                    if (!isTouchSide)
                    {
                        tempSpeedX += accSpeed*Time.fixedDeltaTime;
                        tempSpeedY += accSpeed*Time.fixedDeltaTime;
                    }
                    else
                    {
                        tempSpeedX = Math.Min(0, tempSpeedX + accSpeedFall*Time.fixedDeltaTime);
                    }
                    //
                }
            break;
        }
        // if (Vector2.Distance(transform.position, (Vector2)m_Destination) < 2)
        // {
        //     StopHandler();
        // }
        // controller.Move(hMove * Time.fixedDeltaTime, isCrouch, isJump);
        // //
        // if (isJump)
        //     isJump = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Debug.Log("OnCollisionEnter2D");
        if (col.gameObject.tag == "Wall")
        {
            StopHandler(false, false);
        }
        if (col.gameObject.tag == "LeftWall" || col.gameObject.tag == "RightWall")
        {
            StopHandler(true, false);
        }
    }
}

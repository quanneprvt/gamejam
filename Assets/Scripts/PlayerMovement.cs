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
    // public Animator animator;
    //
    // float hMove = 0f;
    // bool isJump = false;
    // bool isCrouch = false;
    // bool isGrounded = false;
    private float width;
    private float height;
    Rigidbody2D r;
    Vector2 v;
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
    // Start is called before the first frame update
    void Start()
    {
        r = controller.GetRigid();
        // isGrounded = controller.IsLanded();
        //
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;
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
        }
    }

    void MoveHandler()
    {
        // StopHandler(false, false);
        tempPos = transform.position;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
        Vector3 point = ray.origin + (ray.direction * Camera.main.transform.position.z * -1);
        m_Destination = point;
        m_IsArrived = false;
        // m_MoveAngle = 90 - (double)Vector2.Angle(transform.position, (Vector2)point);
        // if (point.x < transform.position.x)
        //     m_MoveAngle = 180 - m_MoveAngle;
        tempSpeedX = moveSpeed;
        tempSpeedY = moveSpeed;
        isTouchSide = false;
        r.velocity = Vector2.zero;
        //
        m_MoveAngle = GetAngle(transform.position, (Vector2)point);
        controller.SetGravity(0f);
        Debug.Log(r.gravityScale);
    }

    void StopHandler(bool side, bool top)
    {
        // m_MoveStep = 0f;
        // tempSpeed = moveSpeed;
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
            break;
        }
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
        if (col.gameObject.tag == "SideWall")
        {
            StopHandler(true, false);
        }
    }
}

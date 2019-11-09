using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float moveSpeed = 5f;
    public float accSpeed = 5f;
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
    private double m_MoveAngle = 0;
    private float tempSpeed = 0;
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

        if (Input.GetButtonDown("Fire1"))
        {
            MoveHandler();
            // point.z = 0;
            // Debug.Log(point);
            // transform.position = point;
        }
    }

    void MoveHandler()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
        Vector3 point = ray.origin + (ray.direction * Camera.main.transform.position.z * -1);
        m_Destination = point;
        m_IsArrived = false;
        // m_MoveAngle = 90 - (double)Vector2.Angle(transform.position, (Vector2)point);
        // if (point.x < transform.position.x)
        //     m_MoveAngle = 180 - m_MoveAngle;
        tempSpeed = moveSpeed;
        m_MoveAngle = GetAngle(transform.position, (Vector2)point);
        Debug.Log(m_MoveAngle);
        controller.SetGravity(0f);
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

    void StopHandler()
    {
        m_MoveStep = 0f;
        // tempSpeed = moveSpeed;
        m_Destination = Vector3.zero;
        m_IsArrived = true;
        controller.SetGravity(1f);
    }

    void FixedUpdate()
    {
        if (!m_IsArrived)
        {
            // m_MoveStep += 1*Time.fixedDeltaTime;
            // transform.position = Vector2.MoveTowards(transform.position, (Vector2)m_Destination, m_MoveStep);
            tempSpeed += accSpeed*Time.fixedDeltaTime;
            Vector2 p;
            p.x = transform.position.x + tempSpeed*(float)Math.Cos(m_MoveAngle*Math.PI/180)*Time.fixedDeltaTime;
            p.y = transform.position.y + tempSpeed*(float)Math.Sin(m_MoveAngle*Math.PI/180)*Time.fixedDeltaTime;
            transform.position = p;
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
            StopHandler();
        }
    }
}

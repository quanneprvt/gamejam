using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float moveSpeed = 50f;
    // public Animator animator;
    //
    float hMove = 0f;
    bool isJump = false;
    bool isCrouch = false;
    bool isGrounded = false;
    private float width;
    private float height;
    Rigidbody2D r;
    Vector2 v;
    //
    private Vector3 savedStat;
    // Start is called before the first frame update
    void Start()
    {
        savedStat = transform.position;
        r = controller.GetRigid();
        isGrounded = controller.IsLanded();
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
            Vector3 point = ray.origin + (ray.direction * Camera.main.transform.position.z * -1);
            point.z = 0;
            Debug.Log(point);
            transform.position = point;
        }
    }

    void FixedUpdate()
    {
        controller.Move(hMove * Time.fixedDeltaTime, isCrouch, isJump);
        //
        if (isJump)
            isJump = false;
    }

    void Reset()
    {
        transform.position = savedStat;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Debug.Log(col.gameObject.name);
        if (col.gameObject.tag == "dead")
        {
            this.Reset();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    public int State;
    private Rigidbody2D myRig;
    private float jumpForce = 10;
    public float Type ;
    public void SetState(int state)
    {
        State = state;
        switch (state)
        {
         
            case GameConstants.ITEM_STATE_FALLING:

                Type = GameConstants.ITEM_TYPE_BULLET;
                break;
            case GameConstants.ITEM_STATE_GROUND:
                Type = GameConstants.ITEM_TYPE_GOOD;
                break;
            case GameConstants.ITEM_STATE_TAKING:
              
                break;
           

        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (Type != GameConstants.ITEM_TYPE_GOOD)
        {
            if (other.gameObject.tag == "LeftWall")
            {

                myRig.velocity = Vector2.right * Random.Range(1f, 2.5f);
            }
            else
             if (other.gameObject.tag == "RightWall")
            {

                myRig.velocity = Vector2.left * Random.Range(2.5f, 3.5f);
            }
            else
             if (other.gameObject.tag == "FlatForm")
            {

                myRig.velocity = Vector3.zero;
                SetState(GameConstants.ITEM_STATE_GROUND);
            }
            else
             if (other.gameObject.tag == "SideWall")
            {
                myRig.velocity = Vector3.zero;
                SetState(GameConstants.ITEM_STATE_GROUND);
            }
            else
             if (other.gameObject.tag == "SideWall")
            {
                myRig.velocity = Vector3.zero;
                SetState(GameConstants.ITEM_STATE_GROUND);
            }
            else
             if (other.gameObject.tag == "Bullet")
            {
                if (other.gameObject.GetComponent<Item>().Type == GameConstants.ITEM_TYPE_GOOD)
                {
                    myRig.velocity = Vector3.zero;
                    SetState(GameConstants.ITEM_STATE_GROUND);
                }
              //  other.gameObject.GetComponent<Item>().Type = GameConstants.ITEM_TYPE_GOOD;
            }
        }
    }
    void Awake()
    {

        myRig = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
      //  myRig.velocity = (Vector2)(Quaternion.Euler(0, 0, 50f) * Vector2.right) * jumpForce;
        // myRig.AddForce(new Vector2(0.2f, 0.2f));
        //  Debug.Log(Quaternion.Euler(0, 0, 60) * Vector2.right);

    }
    public void Throw(float angle , bool isUp)
    {
       
        Vector2 vector = new Vector2();

        if (angle < 0)
            vector = Vector2.left;
        else vector = Vector2.right;
        if (isUp)
        {
          
            vector += Vector2.up*1.5f;
        }
       
        myRig.velocity = (Vector2)(Quaternion.Euler(0, 0, angle) * vector) * jumpForce;
        SetState(GameConstants.ITEM_STATE_FALLING);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

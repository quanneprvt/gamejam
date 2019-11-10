using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    public int State;
    private Rigidbody2D myRig;
    private float jumpForce = 10;
    
    private Animator animator;
    public float Type ;
    public void SetState(int state)
    {
        State = state;
        switch (state)
        {
         
            case GameConstants.ITEM_STATE_FALLING:
                animator.SetInteger("state", 0);
                Type = GameConstants.ITEM_TYPE_BULLET;
                break;
            case GameConstants.ITEM_STATE_CHANGING:
                animator.SetInteger("state", 1);
                Type = GameConstants.ITEM_TYPE_GOOD;
                break;
            case GameConstants.ITEM_STATE_GROUND:
                animator.SetInteger("state", Random.Range(2,6));
                Type = GameConstants.ITEM_TYPE_GOOD;
                GameMgr.Instance.AddPolutionScore(1f);
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
                SetState(GameConstants.ITEM_STATE_CHANGING);
            }
            else
             if (other.gameObject.tag == "Wall")
            {
                myRig.velocity = Vector3.zero;
                SetState(GameConstants.ITEM_STATE_CHANGING);
                
            }
            else
             if (other.gameObject.tag == "Enemy")
            {
                myRig.velocity = Vector3.zero;
                SetState(GameConstants.ITEM_STATE_CHANGING);
               
            }
            else
             if (other.gameObject.tag == "Bullet")
            {
                if (other.gameObject.GetComponent<Item>().Type == GameConstants.ITEM_TYPE_GOOD)
                {
                    myRig.velocity = Vector3.zero;
                    SetState(GameConstants.ITEM_STATE_CHANGING);
                }
              //  other.gameObject.GetComponent<Item>().Type = GameConstants.ITEM_TYPE_GOOD;
            }
        }
    }
    void Awake()
    {

        myRig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
      //  myRig.velocity = (Vector2)(Quaternion.Euler(0, 0, 50f) * Vector2.right) * jumpForce;
        // myRig.AddForce(new Vector2(0.2f, 0.2f));
        //  Debug.Log(Quaternion.Euler(0, 0, 60) * Vector2.right);

    }
    public void DestroyObject()
    {
        ItemMgr.Instance.RemoveItem(this);
        Destroy(this.gameObject);
        GameMgr.Instance.AddPolutionScore(-1f);
    }
    public void Throw(float angle , bool isUp ,float rotate)
    {
      //  
         Vector2 vector_horizon = new Vector2();
        Vector2 vector_verticle = new Vector2();
        Vector2 vector = new Vector2();

        if (angle < 0)
            vector_horizon = Vector2.left;
        else vector_horizon = Vector2.right;

        if (isUp)
        {

            vector_verticle = Vector2.up;
        }
       
        vector = vector_horizon+ (vector_verticle  * GameConstants.ENEMY2_SHOTING_FORCE);
        myRig.velocity = (Vector2)(Quaternion.Euler(0, 0, angle+rotate) * vector) * jumpForce;
        SetState(GameConstants.ITEM_STATE_FALLING);
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {

            case GameConstants.ITEM_STATE_FALLING:
               
                break;
            case GameConstants.ITEM_STATE_CHANGING:
                if(GameDefine.Instance.IsDoneAnim(animator,"item"))
                {
                    SetState(GameConstants.ITEM_STATE_GROUND);
                }
                break;
            case GameConstants.ITEM_STATE_GROUND:
                
                break;
            case GameConstants.ITEM_STATE_TAKING:

                break;


        }
    }
}

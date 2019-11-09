using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float distance;
    private bool movingRight = true;

    public Transform groundDetection;
    
    public int State;
    private Rigidbody2D myRig;
    private float timer;
    [SerializeField]
    private List<GameObject> item_prefab;
    [SerializeField]
    private Transform parentItem;
  

    public void SetState(int state)
    {
        State = state;
        switch (state)
        {
            case GameConstants.ENEMY_STATE_MOVING:
                timer = GameConstants.TIMER_ENEMY_MOVING;
                break;
            case GameConstants.ENEMY_STATE_THROW_BULLET:
                StartCoroutine(SpawnBullet());
                timer = GameConstants.TIMER_ENEMY_BULLET;
                break;
            case GameConstants.ENEMY_STATE_MOVING_2:
                timer = GameConstants.TIMER_ENEMY_MOVING_2;
                break;
            case GameConstants.ENEMY_STATE_HIDDEN:
                timer = GameConstants.TIMER_ENEMY_HIDDEN;
                break;
            case GameConstants.ENEMY_STATE_SHOWING:
                timer = GameConstants.TIMER_ENEMY_SHOWING;
                break;


        }
    }
    void Start()
    {
        SetState(GameConstants.ENEMY_STATE_MOVING);
    }

    public IEnumerator SpawnBullet()
    {
        for(int i=0;i<GameConstants.ITEM_SPAWN_NUMER;i++)
        {
            GameObject item= Instantiate(item_prefab[Random.Range(0, item_prefab.Count)]);
            item.transform.position =  transform.position + new Vector3(0,0.5f,0);
            item.transform.SetParent(parentItem);
            float angle = 0;
            if (movingRight)
            {
                angle = Random.Range(30f, 60f);
            }
            else
            {
                angle = -Random.Range(30f, 60f);
            }
            item.GetComponent<Item>().Throw(angle);
            yield return new WaitForSeconds(0.5f);
        }
    }
    void Moving()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

        switch (State)
        {
            case GameConstants.ENEMY_STATE_MOVING:
                if(timer>0)
                {
                    timer -= Time.deltaTime;
                    Moving();
                    if(timer<0)
                    SetState(GameConstants.ENEMY_STATE_THROW_BULLET);
                }
                
                break;
            case GameConstants.ENEMY_STATE_THROW_BULLET:

                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                    if (timer < 0)
                        SetState(GameConstants.ENEMY_STATE_MOVING_2);
                }
                break;
            case GameConstants.ENEMY_STATE_MOVING_2:
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                    Moving();
                    if (timer < 0)
                        SetState(GameConstants.ENEMY_STATE_HIDDEN);
                }
                break;
            case GameConstants.ENEMY_STATE_HIDDEN:
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                   
                    SetState(GameConstants.ENEMY_STATE_SHOWING);
                }
                break;
            case GameConstants.ENEMY_STATE_SHOWING:
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                    Moving();
                    if (timer < 0)
                        SetState(GameConstants.ENEMY_STATE_MOVING);
                }
                break;


        }
       
    }
}

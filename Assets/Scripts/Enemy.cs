using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private float distance=2;
    public bool movingRight = true;
    private bool isClean = false;
    public Transform groundDetection;
    
    public int State;
    private Rigidbody2D myRig;
    private float timer;
    [SerializeField]
    private int type;
    [SerializeField]
    private List<GameObject> item_prefab;
    [SerializeField]
    private Transform parentItem;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Hint hint;
    [SerializeField]
    private List<ParticleSystem> fire_particle ;
   


    public int GetType()
    {
        return type;
    }
    public bool  GetisClean()
    {
        return isClean;
    }
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
            case GameConstants.ENEMY_STATE_WAITING:
                timer = GameConstants.TIMER_ENEMY_WAITING;
                break;
            case GameConstants.ENEMY_STATE_SHOTING:
                StartCoroutine(ShootBullet());

                timer = GameConstants.TIMER_ENEMY_SHOTING;
                break;
            case GameConstants.ENEMY_STATE_CLEAN:
                
                break;


        }
    }
    void Start()
    {
        if(type== GameConstants.ENEMY_TYPE1)
        SetState(GameConstants.ENEMY_STATE_MOVING);
        else
        {
            //water_particle.startRotation +=  transform.rotation.eulerAngles.z -90f;
            //polution_particle.startRotation += transform.rotation.eulerAngles.z ;
            //  polution_particle.enableEmission  = false;
          
            SetState(GameConstants.ENEMY_STATE_WAITING);
        }
       //
    }

    public IEnumerator SpawnBullet()
    {
        for(int i=0;i<GameConstants.ITEM_SPAWN_NUMER;i++)
        {
            GameObject item= Instantiate(item_prefab[Random.Range(0, item_prefab.Count)]);
            item.transform.position =  transform.position + new Vector3(0,1f,0);
            item.transform.SetParent(parentItem);
            float angle = 0;
            if (movingRight)
            {
                angle = -Random.Range(GameConstants.SPAWN_ANGLE_OF_ENEMY1_MIN, GameConstants.SPAWN_ANGLE_OF_ENEMY1_MAX);
            }
            else
            {
                angle = Random.Range(GameConstants.SPAWN_ANGLE_OF_ENEMY1_MIN, GameConstants.SPAWN_ANGLE_OF_ENEMY1_MAX);
            }
            ItemMgr.Instance.AddItem(item.GetComponent<Item>());
            item.GetComponent<Item>().Throw(angle,false,transform.localEulerAngles.z);
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void DestroyObject()
    {
        EnemyMgr.Instance.RemoveItem(this);
        Destroy(this.gameObject);
    }
    public IEnumerator ShootBullet()
    {
        for (int i = 0; i < GameConstants.ITEM_SPAWN_NUMER2; i++)
        {
            GameObject item = Instantiate(item_prefab[Random.Range(0, item_prefab.Count)]);
            Vector3 point = new Vector3(0, 0.5f, 0);
            if (transform.localEulerAngles.z == 90f)
            {
                point = new Vector3(0f, 1f, 0);
            }
            else if (transform.localEulerAngles.z == 0f)
            {
                point = new Vector3(1f, -0f, 0);
            }
            else if (transform.localEulerAngles.z == 180f)
            {
                point = new Vector3(-1f, 0f, 0);
            }
            else if (transform.localEulerAngles.z == -90f)
            {
                point = new Vector3(0f, -1f, 0);
            }

            item.transform.position = transform.position + point;
            item.transform.SetParent(parentItem);
            ItemMgr.Instance.AddItem(item.GetComponent<Item>());
            float angle = 0;

            angle =  Random.Range(GameConstants.SPAWN_ANGLE_OF_ENEMY2_MIN, GameConstants.SPAWN_ANGLE_OF_ENEMY2_MAX);
            if(Random.Range(0,1)==0)
            {
                angle *= -1;
            }
          //  Debug.Log(transform.rotation +" "+ transform.);
            item.GetComponent<Item>().Throw(angle,true,transform.localEulerAngles.z-90f);
            yield return new WaitForSeconds(0.5f);
        }
       
    }
    public void Clean()
    {
        isClean = true;
        animator.SetBool("isclean", true);
        SetState(GameConstants.ENEMY_STATE_CLEAN);
        hint.SetHappy();
        if (type == GameConstants.ENEMY_TYPE1)
            foreach(ParticleSystem p in fire_particle)
            {
                p.enableEmission = false;
            }
        else
        {
            
            //polution_particle.enableEmission  = false;
            //water_particle.enableEmission = true;
            SetState(GameConstants.ENEMY_STATE_WAITING);
        }
        //  transform.localScale = new Vector3(1,1,1);

    }
    void Moving()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
          
            if (groundInfo.collider == false || groundInfo.collider && groundInfo.collider.tag == "Player")
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
        //else
        //{
        //    Debug.Log(groundInfo.collider.tag);
        //}
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
                    
                    if (timer < 0)
                        SetState(GameConstants.ENEMY_STATE_MOVING);
                }
                break;
            case GameConstants.ENEMY_STATE_WAITING:
                if (timer > 0)
                {
                    timer -= Time.deltaTime;

                    if (timer < 0)
                        SetState(GameConstants.ENEMY_STATE_SHOTING);
                }
                break;
            case GameConstants.ENEMY_STATE_SHOTING:
                if (timer > 0)
                {
                    timer -= Time.deltaTime;

                    if (timer < 0)
                        SetState(GameConstants.ENEMY_STATE_WAITING);
                }
                break;
            case GameConstants.ENEMY_STATE_CLEAN:
                if(type== GameConstants.ENEMY_TYPE1)
                {
                    Moving();
                }
                break;


        }
       
    }
}

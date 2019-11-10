using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAnimMgr : Singleton<FadeAnimMgr>
{
    // Start is called before the first frame update
    private Animator animator;
    [SerializeField]
    private Canvas canvas;
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        animator = canvas.GetComponent<Animator>();
    }
    public int State { get; set; }
  
    public void SetState(int state)
    {
        State = state;
        switch (state)
        {
            case GameConstants.GAME_STATE_FADEIN:
              
                animator.SetInteger("state", GameConstants.GAME_STATE_FADEIN);
               
                break;
            case GameConstants.GAME_STATE_FADEOUT:
               
                animator.SetInteger("state", GameConstants.GAME_STATE_FADEOUT);

                break;
            case GameConstants.GAME_STATE_DEACTIVE:
                animator.SetInteger("state", GameConstants.GAME_STATE_DEACTIVE);
                break;

        }
    }

    public bool IsActive()
    {
       
        return  State != GameConstants.GAME_STATE_DEACTIVE;
    }
    public bool IsDoneFadeIn()
    {
        return GameDefine.Instance.IsDoneAnim(animator, "fadein") && State == GameConstants.GAME_STATE_FADEIN;
    }
    public bool IsDoneFadeOut()
    {
        
        return GameDefine.Instance.IsDoneAnim(animator, "fadeout") && State == GameConstants.GAME_STATE_FADEOUT;
    }
    public void FadeIn()
    {
        SetState(GameConstants.GAME_STATE_FADEIN);
    }
    public void FadeOut()
    {
        SetState(GameConstants.GAME_STATE_FADEOUT);
    }
    void Start()
    {
        
     //   animator = GetComponent<Animator>();        SetState(GameConstants.GAME_STATE_DEACTIVE);
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case GameConstants.GAME_STATE_FADEIN:
                
                break;
            case GameConstants.GAME_STATE_FADEOUT:
                
                if(IsDoneFadeOut())
                {
                    SetState(GameConstants.GAME_STATE_DEACTIVE);
                }

                break;
            case GameConstants.GAME_STATE_DEACTIVE:
             
                break;

        }
    }
}

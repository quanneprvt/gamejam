﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : Singleton<GameMgr>
{
    // Start is called before the first frame update
    [SerializeField]
    private SpriteMask enegry_bar;
    [SerializeField]
    private SpriteMask polution_bar;




    [SerializeField]
    private Animator fade_animator;
    [SerializeField]
    private List<GameObject> level_prefab;
    private int current_level = -1;





    public float speedGame { get; set; }
    private float score_enegry = 0;
    private float score_polution = 0;
    private float timer = 0;
    private float timer_tutorial2 = 0;
    private float frenzy_score = 0;
    private float offset = 0;
    private bool isTouchDown;
    private int previousTime;
    public int State { get; set; }

    public void SetState(int state)
    {
       
        State = state;
        switch (state)
        {

            case GameConstants.GAME_STATE_NONE:
              
                break;
            case GameConstants.GAME_STATE_PLAY:
                fade_animator.SetInteger("state", 0);
                break;
            case GameConstants.GAME_STATE_CHANGE_FADEIN:
                fade_animator.SetInteger("state", 1);
                break;
            case GameConstants.GAME_STATE_CHANGE_FADEOUT:
                fade_animator.SetInteger("state", 2);
                break;
            case GameConstants.GAME_STATE_FINISH:

                break;


        }
    }
    public void InitTutorial()
    {

    }
    public void SpawnNextLevel()
    {
        current_level += 1;
        GameObject ob = Instantiate(level_prefab[current_level]);
        ob.transform.position = Vector3.zero;
    }

    public void SetSpeedGame(float speed)
    {
        speedGame = speed;
    }
    public void AddPolutionScore(float sco)
    {
        score_polution += sco;
        if (score_polution < 0)
        {
            frenzy_score = 0;
        }
        if (score_polution >= GameConstants.BAR_POLUTION_WARNING_LEVEL)
        {

        }
        if (score_polution >= GameConstants.BAR_POLUTION_MAX_LEVEL)
        {

        }
        float percent = (float)score_polution / GameConstants.BAR_POLUTION_MAX_LEVEL;
        polution_bar.transform.localScale = new Vector3(percent, polution_bar.transform.localScale.y, polution_bar.transform.localScale.z);
    }
    public void AddEnegryScore(float sco)
    {

        score_enegry += sco;
        if (score_enegry < 0)
        {
            score_enegry = 0;
        }
        else
        {
            if (score_enegry >= GameConstants.BAR_ENEGRY_MAX_LEVEL)
            {
                score_enegry = GameConstants.BAR_ENEGRY_MAX_LEVEL;


            }
        }
        float percent = (float)score_enegry / GameConstants.BAR_ENEGRY_MAX_LEVEL;
        enegry_bar.transform.localScale = new Vector3(percent, enegry_bar.transform.localScale.y, enegry_bar.transform.localScale.z);
    }

    public void Play()
    {
        SetState(GameConstants.GAME_STATE_PLAY);
    }
    public void NextLevel()
    {
        SetState(GameConstants.GAME_STATE_PLAY);
    }
    // Start is called before the first frame update
    void Start()
    {
      //  SpawnNextLevel();
        Play();
        //   SetState(GameConstants.GAME_MGR_STATE_PLAY);
    }
    private void AlignUI(Vector3 posOffset)
    {

    }
    public void EndGame()
    {

    }
    public void Init()
    {

    }


    private void PlaySoundWarning()
    {

    }
    void OnDisable()
    {

    }

    private void Spawn(int type)
    {

    }
    void Awake()
    {
       
    }
    // Update is called once per frame
    void Update()
    {


        switch (State)
        {

            case GameConstants.GAME_STATE_NONE:

                break;
            case GameConstants.GAME_STATE_PLAY:

                break;
            case GameConstants.GAME_STATE_CHANGE_FADEIN:
                if(GameDefine.Instance.IsDoneAnim(fade_animator,"fadein"))
                {
                    SetState(GameConstants.GAME_STATE_CHANGE_FADEOUT);
                    SpawnNextLevel();
                }
                break;
            case GameConstants.GAME_STATE_CHANGE_FADEOUT:
                if (GameDefine.Instance.IsDoneAnim(fade_animator, "fadeout"))
                {
                    SetState(GameConstants.GAME_STATE_PLAY);
                }
                break;
            case GameConstants.GAME_STATE_FINISH:

                break;


        }

    }
}

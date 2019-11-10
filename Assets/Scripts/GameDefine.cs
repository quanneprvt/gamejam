using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDefine : Singleton<GameDefine>
{
    // Start is called before the first frame update
   
    public Bounds bound;
    private Dictionary<string, Sprite> Sprites;
    private string nextScreen;
  //  public bool  isSpecialRatio = false;
    public Vector2 WorldUnitsInCamera;
    public Vector2 WorldToPixelAmount;
    private Vector2 resolution;
    void Awake()
    {
        //WorldUnitsInCamera.y = Camera.main.orthographicSize * 2;
        //WorldUnitsInCamera.x = WorldUnitsInCamera.y * Screen.width / Screen.height;

        //WorldToPixelAmount.x = Screen.width / WorldUnitsInCamera.x;
        //WorldToPixelAmount.y = Screen.height / WorldUnitsInCamera.y;
     
        DontDestroyOnLoad(transform.gameObject);

        //bound = new Bounds(new Vector3(0,0,0),new Vector3(17.8f,13.4f,0)) ;

        //// Debug.Log(bound.size);
        ////  Debug.Log(Screen.width / Screen.height);
        //resolution = new Vector2(Screen.width, Screen.height);
       
        
    }
    void Start()
    {
        //LoadDictionary("preload");
        //Camera.main.orthographicSize = GetRatioCamera();
        //string[] canvas = { "CanvasPreLoad" };
        //ResizeCanvas(canvas);

    }
    public bool isSpecialRatio()
    {
        return (Screen.height / Screen.width) >= 2f;
    }
    public Vector3 ToScreenPos(Vector3 pos)
    {
        return Camera.main.WorldToScreenPoint(pos);
    }
    
    public Vector3 ToViewPos(Vector3 pos)
    {
        return Camera.main.WorldToViewportPoint(pos);
    }
    public void ResizeCanvas(string[] canvasName)
    {
        Camera cam = Camera.main;
      
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        for(int i = 0; i< canvasName.Length;i++)
        {
            RectTransform rt = GameObject.Find(canvasName[i]).GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(width, height);
        }
       

    }
    public void SwitchScreen(string screen)
    {
        nextScreen = screen;
        //if(screen=="ChooseCharacter")
        //{
        //    SoundManager.Instance.PlaySound(13, false);
        //}
        //else
        //{
        //    if (screen == "Loading")
        //    {
        //        SoundManager.Instance.PlaySound(14, false);
        //    }
        //}
        FadeAnimMgr.Instance.FadeIn();
        Debug.Log(FadeAnimMgr.Instance.IsDoneFadeIn());

    }
    public bool  IsDoneAnim(Animator anim , string name)
    {
      //  Debug.Log(anim);
       
        return (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0)) && anim.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

    public float GetRatioCamera()
    {
        float sceenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = bound.size.x / bound.size.y;
     //   Debug.Log(bg.bounds.size.x + " " + (float)Screen.width);
        if (sceenRatio <= targetRatio)
        {
            return bound.size.y / 2;
          //  Camera.main.orthographicSize = bg.bounds.size.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / sceenRatio;
          
            return bound.size.y / 2 * differenceInSize;
          //  Camera.main.orthographicSize = bg.bounds.size.y / 2 * differenceInSize;
        }
    }
    public Sprite GetSpriteByName(string name)
    {
      
        if (Sprites.ContainsKey(name))
            return Sprites[name];
        else
            return null;
    }
    private void LoadDictionary(string name)
    {
       
        Sprite[] SpritesData = Resources.LoadAll<Sprite>("Sprites/"+name);
        Sprites = new Dictionary<string, Sprite>();

        for (int i = 0; i < SpritesData.Length; i++)
        {

            Sprites.Add(SpritesData[i].name, SpritesData[i]);
        }
    }
    public string[] SoundData1 = { "m_lic_kt_bgm"
            , "m_lic_kt_end"
            , "m_lic_kt_intro"
            , "sfx_collect_item_01"
            , "sfx_collect_item_02"
            , "sfx_collect_item_03"
            , "sfx_hit_obstacle_rock"
            , "sfx_hit_obstacle_rock_frenzy_01"
            , "sfx_hit_obstacle_rock_frenzy_02"
            , "sfx_hit_obstacle_rock_frenzy_03"
            , "sfx_hit_obstacle_slime"
            , "sfx_MC_speed_boost"
            , "sfx_ui_browse"
            , "sfx_ui_confirm"
            , "sfx_ui_select"
            , "sfx_ui_timer"
    };
    void Update()
    {

        // Debug.Log(FadeAnimMgr.Instance );
        //if (FadeAnimMgr.Instance.IsActive())
        //{


        //    if (FadeAnimMgr.Instance.IsDoneFadeIn())
        //    {
        //        SceneManager.LoadScene(nextScreen);
        //        FadeAnimMgr.Instance.FadeOut();
        //    }
        //}
        //if (resolution.x != Screen.width || resolution.y != Screen.height)
        //{
        //    // do stuff
        //    Camera.main.orthographicSize = GetRatioCamera();
        //    string[] canvas = { "CanvasPreload" };
        //    ResizeCanvas(canvas);

        //    resolution.x = Screen.width;
        //    resolution.y = Screen.height;
        //}
    }
   // public int a = 0;

    // Update is called once per frame
    
}

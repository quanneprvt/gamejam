using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Enemy parent;
    [SerializeField]
    private Transform chat;
    private Animator animator;
    private bool state;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }
    public void SetHappy()
    {
        animator.SetBool("isclean", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (parent.GetType() == GameConstants.ENEMY_TYPE2)
            return;
        if (parent.movingRight)
            transform.position = new Vector3(parent.transform.position.x + 1.2f, parent.transform.position.y + 0.6f);
        else
            transform.position = new Vector3(parent.transform.position.x - 1.2f, parent.transform.position.y + 0.5f);
        if(!parent.movingRight && chat.localScale.x==1)
        {
            chat.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            if (parent.movingRight && chat.localScale.x == -1)
            {
                chat.localScale = new Vector3(1, 1, 1);
            }
        }
       
    }
}

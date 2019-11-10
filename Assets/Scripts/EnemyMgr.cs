using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMgr : Singleton<EnemyMgr>
{
    // Start is called before the first frame update
    [SerializeField]
    private List<Enemy> enemy_list ;
    void Start()
    {
        
    }
    public void AddItem(Enemy enemy )
    {
        enemy_list.Add(enemy);
    }
    public void RemoveItem(Enemy enemy)
    {
        enemy_list.Remove(enemy);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

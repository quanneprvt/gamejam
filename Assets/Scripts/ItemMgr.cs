using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMgr : Singleton<ItemMgr>
{
    // Start is called before the first frame update
    private List<Item> item_list;
    void Awake()
    {
        item_list = new List<Item>();

        
    }
    void Start()
    {
        
    }
    public void AddItem(Item item)
    {
        item_list.Add(item);
    }
    public void RemoveItem(Item item)
    {
        item_list.Remove(item);
    }
    public int CountItem()
    {
        int count = 0;
        foreach(Item item in item_list)
        {
            if(item.Type == GameConstants.ITEM_TYPE_GOOD)
            {
                count++;
            }
        }

        return count;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

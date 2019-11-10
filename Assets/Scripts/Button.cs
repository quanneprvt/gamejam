using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update
 
    internal bool interactable;

    void Start()
    {
        
    }
    void OnMouseDown()
    {
        GameDefine.Instance.SwitchScreen("SampleScene");
        Debug.Log("p");

        // ;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}

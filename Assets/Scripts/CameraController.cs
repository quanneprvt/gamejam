using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject m_Target;
    private Vector3 offset;
    private Vector3 pos;
    float minX = 0f;
    float maxX = 4057f;
    float maxY = 0f;

    // Use this for initialization
    void Start()
    {
        // offset = transform.position - Player.transform.position;
        pos =  transform.position;
        offset = transform.position - m_Target.transform.position;
        //
        minX = 0.75f;
        maxX = 18f;
        maxY = transform.position.y - 0.5f;
    }

    //Update each frame
    void Update()
    {
        // offset.x = Player.transform.position.x - transform.position.x;
        //
        // pos = transform.position + 3* offset * Time.deltaTime;
        // pos.x = Math.Min(maxX, Math.Max(minX, transform.position.x + 3 * (m_Target.transform.position.x - transform.position.x) * Time.deltaTime));
        // pos.y = Math.Min(maxY , transform.position.y + 5 * (m_Target.transform.position.y - (transform.position.y - offset.y - 0.5f)) * Time.deltaTime);
        pos.x = transform.position.x + 3 * (m_Target.transform.position.x - transform.position.x) * Time.deltaTime;
        pos.y = transform.position.y + 5 * (m_Target.transform.position.y - transform.position.y) * Time.deltaTime;
        //
        transform.position = pos;
    }

    // LateUpdate is called after Update each frame
    // void LateUpdate()
    // {
    //     transform.position = transform.position + 3* offset * Time.fixedDeltaTime;
    // }
}

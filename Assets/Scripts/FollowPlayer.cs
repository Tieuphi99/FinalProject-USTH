using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (player.transform.position.x > 0)
        {
            transform.position = new Vector3(player.transform.position.x, 3, -10);
        }
    }
}
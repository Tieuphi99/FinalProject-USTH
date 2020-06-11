using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    
    // Camera follows player
    void LateUpdate()
    {
        if (player.transform.position.x > 4.5f)
        {
            transform.position = new Vector3(player.transform.position.x, 5, -10);
        }
    }
}
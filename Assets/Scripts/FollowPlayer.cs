using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > 0)
        {
            transform.position = player.transform.position + new Vector3(0, 0, -10);
        }
    }
}
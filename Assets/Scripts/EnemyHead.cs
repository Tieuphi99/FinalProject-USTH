using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    private GoombaController _goombaController;
    public GameObject goomba;

    public float pushForce = 400;

    private void Awake()
    {
        _goombaController = goomba.GetComponent<GoombaController>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.rigidbody.AddForce(new Vector2(0f, pushForce));
            _goombaController.speed = 0;
            _goombaController.Die();
        }
    }
}

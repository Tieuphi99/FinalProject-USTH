using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    private PlayerController _playerController;
    [SerializeField] private GameObject player;

    public float pushForce = 700;
    
    private void Awake()
    {
        _playerController = player.GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerController.isDead = true;
            other.rigidbody.AddForce(new Vector2(0f, pushForce));
        }
    }
}
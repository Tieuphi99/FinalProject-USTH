using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    private PlayerController _playerController;
    private GoombaController _goombaController;
    
    public GameObject goomba;

    private void Awake()
    {
        _goombaController = goomba.GetComponent<GoombaController>();
        _playerController = _goombaController.player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (_goombaController.isTouchByPlayer)
        {
            GetComponent<BoxCollider2D>().offset = Vector2.zero;
            GetComponent<BoxCollider2D>().size = new Vector2(1.04f, 0.32f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Die(other.gameObject));
            _playerController.isDead = true;
        }
    }

    IEnumerator Die(GameObject playerGameObject)
    {
        yield return new WaitForSeconds(1);
        playerGameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 4000));
        playerGameObject.GetComponent<BoxCollider2D>().enabled = false;
        playerGameObject.GetComponent<EdgeCollider2D>().enabled = false;
        playerGameObject.GetComponent<Rigidbody2D>().gravityScale = 25;
    }
}
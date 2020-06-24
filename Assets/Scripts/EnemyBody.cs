using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    private PlayerController _playerController;
    private GoombaController _goombaController;
    
    public GameObject player;
    public GameObject goomba;

    private Rigidbody2D _goombaRb;

    private void Update()
    {
        if (goomba.transform.position.x - player.transform.position.x < 12)
        {
            goomba.SetActive(true);
        }
    }

    private void Awake()
    {
        _goombaController = goomba.GetComponent<GoombaController>();
        _goombaRb = _goombaController.GetComponent<Rigidbody2D>();
        _playerController = player.GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log($"Touch {other.gameObject.name}");
            StartCoroutine(Die(other.gameObject));
            _playerController.isDead = true;
            _goombaController.speed = 0;
            _goombaRb.velocity = Vector2.zero;
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